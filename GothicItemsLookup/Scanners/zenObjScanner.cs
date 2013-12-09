using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.streamReaders;
using System.Text.RegularExpressions;

namespace GothicItemsLookup.Scanners
{
    class zenObjScanner : IItemScaner
    {
        public enum ZENOBJ_ACTION{ADD,SKIP,BREAK_LOOP}
        public enum ZENOBJ_Class { oCItem, oCMobContainer,WayNet, OTHER }

        public ZENOBJ_Class objClass {get; private set;}
        public ZENOBJ_ACTION action { get; private set; }
        public string objPosStr { get; private set; }
        public string srcFilePath { get; private set; }
        public string definitionLine { get; private set; } // for update'ing status

        private List<scannedItem> extractedItems;
        private zenStreamReader zenStream;
        public zenObjScanner(ref zenStreamReader stream)
        {
            zenStream = stream;
            srcFilePath = stream.filePath;
            _seekToObjDefinition(ref stream);
            definitionLine = stream.ReadLine();
            _extractObjClass(definitionLine);
            string lline = stream.lastLine;
            if(objClass == ZENOBJ_Class.WayNet) action = ZENOBJ_ACTION.BREAK_LOOP;
            else if(objClass == ZENOBJ_Class.OTHER) _skipToEndOfZenObj(ref stream,true);
            else//Item/Container
            {
                List<string> instances = new List<string>(); // if oCItem => 1 element
                objPosStr = _extractObjPos(ref stream);
                if(objClass == ZENOBJ_Class.oCItem) 
                    instances.Add(_extractItemInstance(ref stream));
                else
                    instances = _extractContainerItems(ref stream);
                // Trzeba odp. wyciagnąć instancje itemów z "listy"
                extractedItems = new List<scannedItem>();
                foreach (string item in instances)
                {
                    string[] tmp = item.Split(':');
                    try
                    {
                        _addItemToExtractedItems(tmp);
                    }
                    catch (Exception e)
                    {
                        System.Threading.Thread clipboardThread = new System.Threading.Thread(() => System.Windows.Forms.Clipboard.SetText(item));
                        clipboardThread.SetApartmentState(System.Threading.ApartmentState.STA);
                        clipboardThread.IsBackground = false;
                        clipboardThread.Start();
                        System.Windows.Forms.MessageBox.Show("Pojawil sie blad przy parsowaniu ciagu: \n" + item + "\n w pliku: " + srcFilePath + "\nBledny ciag skopiowano do showka\nNapraw blad by sprasowac ZEN!");
                        System.Windows.Forms.Application.Exit();
                    }
                }
                // [TODO] sprawdzenie czy któryś z itemów jest poszukiwany
                // jesli tak dodaj go gdzieśtam(?) w odp. ilości.
                lookupResults.self.testWithLookupItems(this);
                _skipToEndOfZenObj(ref stream, false);
            }
        }
        private void _addItemToExtractedItems(string[] srcString)
        {
            // Na wejsciu fromat: {ITMINUGGET} / {ITMINUGGET,666}
            scannedItem slf = extractedItems.SingleOrDefault(e => e.instance == srcString[0]);
            int itmCnt = 1;
            if (srcString.Length == 2)
            {
                itmCnt = int.Parse(srcString[1]);
            }
            if (slf != null) slf.increaseCount(itmCnt);
            else extractedItems.Add(
                new scannedItem(srcString[0], itmCnt, (objClass == ZENOBJ_Class.oCItem) ? findResultType.ITEM : findResultType.CHEST,
                                new Results.SearchResultSource(zenStream.filePath, -1, zenStream.lastLine)));
        }

        // Na wejsciu, ta funkcja jest na poczatku lini po pozycji Voba (czyli vobName)
        private string _extractItemInstance(ref zenStreamReader stream)
        {
            const int instanceOffset = 23;
            //stream.BaseStream.Seek(197, System.IO.SeekOrigin.Current);
            while (!stream.ReadLine().StartsWith("\t\t\titemInst")) ;
                return stream.lastLine.Substring(instanceOffset);
        }
        // Na wejsciu, ta funkcja jest na poczatku lini po pozycji Voba (czyli vobName)
        private List<string> _extractContainerItems(ref zenStreamReader stream)
        {
            List<string> res;
            const int constainsOffset = 19;
            string packedItems;
            // stream.BaseStream.Seek(625, System.IO.SeekOrigin.Current);
            while (!stream.ReadLine().StartsWith("\t\t\tcontains")) ;
            packedItems = stream.lastLine.Substring(constainsOffset);
            res = new List<string>(packedItems.Split(','));
            return res;
        }

        // Na wejsciu strumień jest ustawiony na poczatku linijki po definicji voba (na pack)
        private string _extractObjPos(ref zenStreamReader stream)
        {
            // Jako że mogą sie zdarzyć rózne ciekawe odstępstwa w il. bajtów do trafoOSToWSPos
            // czytam linia-po-lini:
            const int objPosOffset = 23;
            while (!stream.ReadLine().StartsWith("\t\t\ttrafoOSToWSPos")) { }
            return stream.lastLine.Substring(objPosOffset);
        }

        private void _skipToEndOfZenObj(ref zenStreamReader stream,bool skippingFromBegining)
        {
            // if(skippingFromBegining) // Jeżeli jesteśmy na początku def. obiektu to możemy spokojnie Seek'nąć
            //     stream.BaseStream.Seek(300, System.IO.SeekOrigin.Current);
            while (!stream.ReadLine().StartsWith("\t\t[]")) ;
        }

        private void _extractObjClass(string p)
        {
            if (p == null) return;

            if (Regex.IsMatch(p, ".*?\\[% oCMobContainer")) objClass = ZENOBJ_Class.oCMobContainer;
            else if (Regex.IsMatch(p, ".*?\\[% oCItem.*")) objClass = ZENOBJ_Class.oCItem;
            else if (Regex.IsMatch(p, ".*?\\[WayNet")) objClass = ZENOBJ_Class.WayNet;
            else objClass = ZENOBJ_Class.OTHER;
        }
        private void _seekToObjDefinition(ref zenStreamReader stream)
        {
            while (!stream.EndOfStream && !Regex.IsMatch(stream.ReadLine(), "\t.*?childs(\\d+)=")) ;
        }


        public IEnumerable<scannedItem> getFoundedItems()
        {
            return extractedItems;
        }
    }
}
