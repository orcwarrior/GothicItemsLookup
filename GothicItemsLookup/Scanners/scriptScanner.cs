using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.streamReaders;
using System.Threading;
using System.Text.RegularExpressions;
using GothicItemsLookup.Scanners.Utils;
using GothicItemsLookup.Results;

namespace GothicItemsLookup.Scanners
{
    // TODO: Implementuj interfejs wspolny dla scriptFile i zenObj
    // (do zwracania wyników przeszukiwania pliku/obiektu/etc)
    class scriptScanner : IItemScaner
    {
        public int curLineOfCode = 0;
        public string self { get; private set; } // self instance
        public string other { get; private set; } // other instance

        private Stack<scriptBlock> currentScriptBlocks;
        private List<string> currentConditions;// array of current conditions (grabbed cond-blocks on Stack)
        private List<scannedItem> extractedItems;
        private findResultType currentScaningItemsType;
        private string funcsRegexLookupStr; // <- skl. regex dla wyszukiwania funkcji dod. itemy
        private IEnumerable<itemAddingFunc> lookupFuncs; // przekazane w konstruktorze
        private string srcFilePath;
        public scriptScanner(ref scriptStreamReader stream, IEnumerable<itemAddingFunc> _lookupFuncs)
        {
            currentScriptBlocks = new Stack<scriptBlock>();
            currentConditions = new List<string>();
            currentScaningItemsType = findResultType.OTHER;
            extractedItems = new List<scannedItem>();
            lookupFuncs = _lookupFuncs;
            funcsRegexLookupStr = _genItmsFuncsRegexPattern(); // store it in varible and dont recreate it each loop
            srcFilePath = stream.filePath;
            _performScan(stream);
            lookupResults.self.testWithLookupItems(this);
        }

        private void _performScan(scriptStreamReader stream)
        {
            string curLine;
            while (!stream.EndOfStream)
            {
                curLine = stream.ReadLine(); curLineOfCode++;
                _scanDetectCodeBlocks(curLine);
                _scanDetectBlockClosure(curLine);
                // if we not already in some script block, then somethings is wrong
                if (currentScriptBlocks.Count > 0)
                {
                    scriptBlock curBlock = currentScriptBlocks.Peek();
                    if (curBlock.blockType == eBlockType.C_INFO)
                    {
                        _tryToFindNpcDefinition(curLine);
                    }
                    else
                    {
                        _scanTryToFindItemAddingFunc(curLine);
                    }
                }
            }
        }

        private void _scanTryToFindItemAddingFunc(string curLine)
        {
            Match m = Regex.Match(curLine, funcsRegexLookupStr + ".*?\\((.*)\\);", RegexOptions.IgnoreCase);
            if (m.Success)
            {   // Regex match:
                // Group 1: Function name
                // Group 2: Function parameters (without brackets)

                itemAddingFunc func = lookupFuncs.Single(f => string.Equals(f.name, m.Groups[1].Value, StringComparison.OrdinalIgnoreCase));
                if (func != null)
                {
                    string pSlf, pOth;
                    int count;
                    object[] parameters = func.exctractParameters(m.Groups[2].Value);
                    pSlf = parameters[(int)eItemFunc_Param.srcNpc] as string;
                    pSlf = (pSlf == null) ? null : (pSlf.Equals("self") ? self : pSlf);
                    pOth = parameters[(int)eItemFunc_Param.dstNpc] as string;
                    pOth = (pOth == null) ? null : (pOth.Equals("other") ? other : pOth);
                    //int? nullable int zabezpieczy nas w pewien sposob przed niespodziankami :)
                    int? hlp = parameters[(int)eItemFunc_Param.count] as int?;
                    count = hlp.GetValueOrDefault(0);
                    // Touching this piece od code is bad idea!
                    extractedItems.Add(new scriptScannedItem(
                        /*instance*/parameters[(int)eItemFunc_Param.itemID] as string,
                        /*  count */count,
                        /*findType*/currentScaningItemsType, 
                        /*findSrc */new SearchResultSource(srcFilePath, curLineOfCode, curLine),
                        /*  conds */string.Join(" && ", currentConditions.ToArray()),
                        /*  self  */pSlf,
                        /*  other */pOth
                    ));// DAAAAMN XD
                }
            }
        }

        private void _scanDetectBlockClosure(string curLine)
        {
            int blocks2Close = curLine.CountOccurencesOf("}");
            while (blocks2Close > 0 && currentScriptBlocks.Count > 0)
            {
                scriptBlock removed = currentScriptBlocks.Pop();
                // If we poped condition block, update current Conditions
                if (removed.blockType == eBlockType.COND)
                    if (currentConditions.Count > 0) currentConditions.RemoveAt(currentConditions.Count - 1);
                blocks2Close--; //<- popujemy block ze stosu otwartych bloków
            }
        }

        /// <summary>
        /// Check some block was opened in this line:
        /// (do only if-elseif-else test if some block is already opened)
        /// if COND block found, it will be added to currentConditioons list.
        /// </summary>
        private void _scanDetectCodeBlocks(string curLine)
        {
            scriptBlock block = scriptBlock.tryToCreate(curLine, currentScriptBlocks.Count > 0);
            if (block != null)
            {
                currentScriptBlocks.Push(block); //<- add block to stack:
                if (block.blockType == eBlockType.COND)
                    currentConditions.Add(block.additionalInfo);
                if (currentScriptBlocks.Count == 1)
                {
                    if (block.blockType == eBlockType.INSTANCE) self = block.additionalInfo;
                    currentScaningItemsType = _determineCurrentItemsType();
                }
            }
        }
        /// <summary>
        /// Called everytime when first level of deep block was opened
        /// to decide which kind of Items it's create
        /// </summary>
        /// <returns></returns>
        private findResultType _determineCurrentItemsType()
        {
            switch (currentScriptBlocks.Peek().blockType)
            {
                case eBlockType.C_INFO: return findResultType.MISSION;
                case eBlockType.FUNC: if (self != "self") return findResultType.MISSION; else return findResultType.OTHER;
                case eBlockType.INSTANCE: if (GothicItemsLookup.Utils.isTraderInstance(currentScriptBlocks.Peek().additionalInfo)) 
                                               return findResultType.TRADER;
                                          else return findResultType.NPC;
                default: return findResultType.OTHER;
            }
        }
        private string _genItmsFuncsRegexPattern()
        {
            string pattern = "(";
            foreach (itemAddingFunc func in lookupFuncs)
                pattern += func.name + "|";
            if (pattern.Length >= 1) // nothing added, Quit
                pattern = pattern.Substring(0, pattern.Length - 1);
            pattern += ")";
            return pattern;
        }
        // This is quite primitive, but should work in most cases:
        private void _tryToFindNpcDefinition(string curLine)
        {
            Match m = Regex.Match(curLine, "npc[\\s]*?= ([\\w]*);", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                self = m.Groups[1].Value;
                other = "PC_HERO";
            }
        }
        public IEnumerable<scannedItem> getFoundedItems()
        {
            return extractedItems;
        }
    }
}
