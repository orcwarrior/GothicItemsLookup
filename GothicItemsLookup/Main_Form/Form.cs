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

namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
            _testIfCanRunItemsSearch();
            _initialize_itemAddingFunctions();
            _initialize_resultDetailsFilter();
        }

        private void _resetStatusBars()
        {
            toolStrip_ProgressBarPartial.Value = toolStrip_ProgressWhole.Value = 0;
        }


        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
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
                return; // <- break b4 disaster o,o
            }

            if(statusMSG!=null)
            toolStrip_Status.Text = statusMSG;
            if (progrWhole < 0 || progrWhole > 1.0) toolStrip_ProgressWhole.Visible = false;
            else
            {
                toolStrip_ProgressWhole.Visible = true;
                toolStrip_ProgressWhole.Maximum = 1000;
                toolStrip_ProgressWhole.Value = (int)(1000 * progrWhole);
            }
            if (progrPartial < 0 || progrPartial > 1.0) toolStrip_ProgressBarPartial.Visible = false;
            else
            {
                toolStrip_ProgressBarPartial.Visible = true;
                toolStrip_ProgressBarPartial.Maximum = 1000;
                toolStrip_ProgressBarPartial.Value = (int)(1000 * progrPartial);
            }
        }

        // Button: Lookup for items (checks for items in Scripts and Worlds)
        private void btn_SearchForItems_Click(object sender, EventArgs e)
        {
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
            tabControl.SelectedTab = tab_srchResults;
        }

        private void bw_itemsLookup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (searchedItem it in _foundItems)
                sItems_SelectedItems.Items.Add(it);
            genericProgressUpdateHandl(-1, -1, "Gotowy");
            results_Summary.Items.Clear();
            foreach (searchResultSummary sum in lookupResults.self)
                results_Summary.Items.Add(sum);
            _resetStatusBars();
            btn_SearchForItems.Enabled = true;
        }
        private void bw_itemsLookup_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            lookupResults.Create(_foundItems);
            // Scripts Lookup:
            if (!sScripts_GothicSRC.Text.Equals("")
            && System.IO.File.Exists(sScripts_GothicSRC.Text))
            {
                scriptFilesParser.Parse(sScripts_GothicSRC.Text, currentFunctions);
                while (!scriptFilesParser.jobDone) System.Threading.Thread.Sleep(250);
            }
            // Worlds Lookup:
            if (sWorld_worldsList.Items.Count > 0)
            {
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
        }



    }
}
