using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using GothicItemsLookup.parsers;
using GothicItemsLookup.Scanners;
using LogSys;
namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {
        private void btn_SelectItemsScripts_Click(object sender, EventArgs e)
        {
            selectFilesGeneric.Reset();
            selectFilesGeneric.Multiselect = true;
            if (Properties.Settings.Default.path_ItemsScript != null)
                selectFilesGeneric.InitialDirectory = Properties.Settings.Default.path_ItemsScript;
            selectFilesGeneric.Filter = "Gothic Script|*.d";
            selectFilesGeneric.ShowDialog(this);
            new LogMsg("Items-Scipt: Selecting Items files...", eDebugMsgLvl.INFO);
            foreach (string file in selectFilesGeneric.FileNames)
            {
                sItems_ScriptFiles.Items.Add(new FileEntry(file));
                new LogMsg("Items-Scipt: \tAdded: "+file, eDebugMsgLvl.INFO);
            }
            if(selectFilesGeneric.FileNames.Length>0) // wybrano coś:
                Properties.Settings.Default.path_ItemsScript = Utils.ExctractPath(selectFilesGeneric.FileNames[0]);
        }

        private void btn_ItemsScriptsClearAll_Click(object sender, EventArgs e)
        {
            new LogMsg("Items-Scipt: Clear all items files", eDebugMsgLvl.INFO);
            sItems_ScriptFiles.Items.Clear();
        }

        ICollection<searchedItem> _foundItems;
        private void sItems_btn_GetItems_Click(object sender, EventArgs e)
        {
            new LogMsg("Items-Scipt-Extract: Start runing...", eDebugMsgLvl.INFO);
            _resetStatusBars();
            BackgroundWorker bw = new BackgroundWorker();
            itemFileParser.worker = bw;
            itemFileParser.progressUpdate = genericProgressUpdateHandl;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_exctractItems_doWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            new LogMsg("Items-Scipt-Extract: Run compleated, exctracted items: "+_foundItems.Count, eDebugMsgLvl.INFO);
            foreach (searchedItem it in _foundItems)
                sItems_SelectedItems.Items.Add(it);
            toolStrip_Status.Text = "Gotowy";
            _testIfCanRunItemsSearch();
            _resetStatusBars();
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStrip_Status.Text = itemFileParser.curFile;
            toolStrip_ProgressWhole.Maximum = 100;
            toolStrip_ProgressWhole.Value = e.ProgressPercentage;
            new LogMsg("Items-Scipt-Extract: Progress changed: "+e.ProgressPercentage, eDebugMsgLvl.INFO);
        }
        private void bw_exctractItems_doWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            List<string> filepaths = new List<string>();
            foreach (FileEntry item in sItems_ScriptFiles.Items)
                filepaths.Add(item.fullpath);

            stringFilter sf = new stringFilter("",sItems_inp_FilterItemsInstance.Text,(sItems_CaseSensitive.Checked)? RegexOptions.None : RegexOptions.IgnoreCase);
            new LogMsg("Items-Scipt-Extract: Filter: ["+sf.wildCardPattern+"]", eDebugMsgLvl.INFO);
            _foundItems = itemFileParser.Parse(filepaths, sf);

        }
        private void sItems_btn_clearItems_Click(object sender, EventArgs e)
        {
            new LogMsg("Clear Exctracted items", eDebugMsgLvl.INFO);
            sItems_SelectedItems.Items.Clear();
            _testIfCanRunItemsSearch();
        }

        private void sItems_SelectedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sItems_SelectedItems.SelectedItem != null)
            {
                searchedItem it = (sItems_SelectedItems.SelectedItem as searchedItem);
                toolStrip_Status.Text = it.instance + " " + it.fields["name"] + " " + it.fields["value"];
            }
        }

        private void sItems_btn_removeSelectedItms_Click(object sender, EventArgs e)
        {
            sItems_SelectedItems.Visible = false;
            for (int x = sItems_SelectedItems.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = sItems_SelectedItems.SelectedIndices[x];
                sItems_SelectedItems.Items.RemoveAt(idx);
            }
            sItems_SelectedItems.Visible = true;
            _testIfCanRunItemsSearch();
        }
    }
}