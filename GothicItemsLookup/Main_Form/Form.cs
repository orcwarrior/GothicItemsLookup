using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GothicItemsLookup.parsers;
using GothicItemsLookup.Scanners;
using GothicItemsLookup.Results;
using LogSys;

namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {

        public Form_Main()
        {
            System.IO.File.Delete("log.txt"); // <-remove old log
            Logger.Initialize(new System.IO.StreamWriter("log.txt"), true, eDebugMsgLvl.INFO);
            InitializeComponent();
            _testIfCanRunItemsSearch();
            _initialize_itemAddingFunctions();
            _initialize_resultsFilter();
            new LogMsg("Initializing Done", eDebugMsgLvl.INFO);
        }

        private void _resetStatusBars()
        {
            toolStrip_ProgressBarPartial.Value = toolStrip_ProgressWhole.Value = 0;
            new LogMsg("Progress bars reset", eDebugMsgLvl.INFO);
        }


        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            new LogMsg("Closing Main Form", eDebugMsgLvl.INFO);
            Logger.instance.Dispose();
        }
        // Generic Hanlder of progress update:
        // (progress in range of: 0.0-1.0, otherwise it will hide progressBar)
        // if statusMSG is null, it will not affect current status text
        private void genericProgressUpdateHandl(double progrPartial, double progrWhole, string statusMSG)
        {
            // FIX: Nie możemy zmienić wartosci z innego wątku (a w ten sposob najczesciej probojemy)
            // w tym wypadku skorzystamy z metody Invoke.
            if (this.InvokeRequired)
            {
                updateProgress del = genericProgressUpdateHandl;
                this.Invoke(del, new object[] { progrPartial, progrWhole, statusMSG });
                new LogMsg("Generic Progress Update -> need invoke", eDebugMsgLvl.INFO);
                return; // <- break b4 disaster o,o
            }

            if (statusMSG != null)
            {
                toolStrip_Status.Text = statusMSG;
                new LogMsg("Generic Progress Update -> status: " + statusMSG, eDebugMsgLvl.INFO);
            }
            if (progrWhole < 0 || progrWhole > 1.0) toolStrip_ProgressWhole.Visible = false;
            else
            {
                toolStrip_ProgressWhole.Visible = true;
                toolStrip_ProgressWhole.Maximum = 1000;
                toolStrip_ProgressWhole.Value = (int)(1000 * progrWhole);
                new LogMsg("Generic Progress Update -> ProgressWhole:" + progrWhole, eDebugMsgLvl.INFO);
            }
            if (progrPartial < 0 || progrPartial > 1.0) toolStrip_ProgressBarPartial.Visible = false;
            else
            {
                toolStrip_ProgressBarPartial.Visible = true;
                toolStrip_ProgressBarPartial.Maximum = 1000;
                toolStrip_ProgressBarPartial.Value = (int)(1000 * progrPartial);
                new LogMsg("Generic Progress Update -> ProgressPartial:" + progrPartial, eDebugMsgLvl.INFO);
            }
        }

        // Button: Lookup for items (checks for items in Scripts and Worlds)
        private void btn_SearchForItems_Click(object sender, EventArgs e)
        {
            new LogMsg("Start Items Lookup", eDebugMsgLvl.INFO);
            _resetStatusBars();
            btn_SearchForItems.Enabled = false;
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            itemFileParser.worker = bw;
            scriptFilesParser.progressUpdate = genericProgressUpdateHandl;
            itemFileParser.progressUpdate = genericProgressUpdateHandl;
            bw.DoWork += new DoWorkEventHandler(bw_itemsLookup_doWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_itemsLookup_RunWorkerCompleted);
            bw.RunWorkerAsync();
            new LogMsg("Items Lookup: Worker thread started!", eDebugMsgLvl.INFO);
            tabControl.SelectedTab = tab_srchResults;
        }

        private void bw_itemsLookup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            new LogMsg("Items Lookup: Worker thread finished! Founded Items: "+_foundItems.Count, eDebugMsgLvl.INFO);
            foreach (searchedItem it in _foundItems)
                sItems_SelectedItems.Items.Add(it);
            genericProgressUpdateHandl(-1, -1, "Gotowy"); 
            _init_resultsSummary_ListBox();
            _resetStatusBars();
            btn_SearchForItems.Enabled = true;
        }

        private void _init_resultsSummary_ListBox()
        {
            new LogMsg("Initing result summary", eDebugMsgLvl.INFO);
            results_Summary.Items.Clear();
            foreach (searchResultSummary sum in lookupResults.self)
                results_Summary.Items.Add(sum);
        }
        private void bw_itemsLookup_doWork(object sender, DoWorkEventArgs e)
        {
            new LogMsg("Items Lookup: Worker thread event there", eDebugMsgLvl.INFO);
            BackgroundWorker worker = sender as BackgroundWorker;
            lookupResults.Create(_foundItems);
            // Scripts Lookup:
            if (!sScripts_GothicSRC.Text.Equals("")
            && System.IO.File.Exists(sScripts_GothicSRC.Text))
            {
                new LogMsg("Items Lookup: Starting gothic.src parsing!", eDebugMsgLvl.INFO);
                scriptFilesParser.Parse(sScripts_GothicSRC.Text, currentFunctions);
                while (!scriptFilesParser.jobDone) System.Threading.Thread.Sleep(250);
            }
            // Worlds Lookup:
            if (sWorld_worldsList.Items.Count > 0)
            {
                new LogMsg("Items Lookup: Starting lookup in world files", eDebugMsgLvl.INFO);
                List<string> filepaths = new List<string>();
                foreach (FileEntry item in sWorld_worldsList.Items)
                    filepaths.Add(item.fullpath);
                zenFileParser.worker = worker;
                zenFileParser.Parse(filepaths);
                while (!zenFileParser.jobDone || !scriptFilesParser.jobDone) System.Threading.Thread.Sleep(250);
            }
        }
        // Called when Loaded Items Scripts, Exctracted Items, Worlds or gothic.src changed:
        private void _testIfCanRunItemsSearch()
        {
            btn_SearchForItems.Enabled = (sItems_SelectedItems.Items.Count > 0 &&
                (sWorld_worldsList.Items.Count + sScripts_GothicSRC.Text.Length > 0));
            new LogMsg("Testing if can run items lookup, result: " + btn_SearchForItems.Enabled, eDebugMsgLvl.INFO);
        }



    }
}
