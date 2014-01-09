using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using GothicItemsLookup.parsers;
using GothicItemsLookup.Scanners;
using System.Drawing;
using GothicItemsLookup.Results;
using LogSys;
namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {
        findResultType? sResultsFilter;
        private void results_Summary_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (0 > e.Index || e.Index > (sender as ListBox).Items.Count) return;
            searchResultSummary cur = results_Summary.Items[e.Index] as searchResultSummary;
            Color BG = e.BackColor;
            int amount = cur.getFilteredAmount(sResultsFilter);
            if (amount == 0)
                BG = ColorsConst.resultSummary_NotFound;
            else
                BG = System.Drawing.Color.FromArgb(255, Math.Max(255 - amount / 3, 140), Math.Max(255 - amount * 3, 80));
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                BG = System.Drawing.Color.FromArgb( BG.R + ColorsConst.resultSelected[0],
                                                    BG.G + ColorsConst.resultSelected[1],
                                                    BG.B + ColorsConst.resultSelected[2]);
            e.Graphics.FillRectangle(new SolidBrush(BG), e.Bounds);
            e.Graphics.DrawString(cur.ToString(sResultsFilter), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Location);
        }

        private void results_Summary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                searchResultSummary sum = (sender as ListBox).SelectedItem as searchResultSummary;
                results_Details.Items.Clear();
                foreach (searchResult res in sum)
                    if (sResultsFilter == null)
                        results_Details.Items.Add(res);
                    else if (res.type == sResultsFilter)
                        results_Details.Items.Add(res);
                new LogMsg("Result Summary Evt: Selected IDX changed, results cnt: "+results_Details.Items.Count+", filter: "+sResultsFilter, eDebugMsgLvl.INFO);
            }
        }

        private void result_Details_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (0 > e.Index || e.Index > (sender as ListBox).Items.Count) return;

            searchResult cur = results_Details.Items[e.Index] as searchResult;
            Color BG = cur.myColor;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                BG = System.Drawing.Color.FromArgb(BG.R - 40, BG.G - 40, BG.B - 40);
            e.Graphics.FillRectangle(new SolidBrush(BG), e.Bounds);
            if(cur.instanceUpdated) // Pogrub czcionke jeśli istancja była zmieniona
                e.Graphics.DrawString(cur.ToString(), new Font(e.Font.FontFamily,e.Font.Size,FontStyle.Bold), new SolidBrush(e.ForeColor), e.Bounds.Location);
            else
                e.Graphics.DrawString(cur.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Location);
        }


        private void singleResCont_MoreInfos_Click(object sender, EventArgs e)
        {
            new LogMsg("Detail Menu-Context: Opening More Infos form...", eDebugMsgLvl.INFO);
            searchResult res = results_Details.SelectedItem as searchResult;
            if (res != null)
                res.MoreInfos().Show(this);
        }

        private void singleResCont_Run_Click(object sender, EventArgs e)
        {
            new LogMsg("Detail Menu-Context: Opening source file(s): "+results_Details.SelectedItems.Count, eDebugMsgLvl.INFO);
            string nppPath = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\notepad++.exe", "", null);

            new LogMsg("Detail Menu-Context: Opening sources, n++ found: "+(nppPath!=null), eDebugMsgLvl.INFO);
            for (int i = 0; i < results_Details.SelectedItems.Count; i++)
            {
                searchResult res = results_Details.SelectedItems[i] as searchResult;
                string line = ((res.src.line > 0) ? res.src.line : 0).ToString();
                if (nppPath == null)
                    System.Diagnostics.Process.Start(res.src.file);
                else
                    try
                    {
                        System.Diagnostics.Process.Start(nppPath, "-n" + line + " " + res.src.file);
                    }
                    finally { }
            }
        }
        // Context: Usuwanie plików dla znaznaczonych wystąpień przedmiotów
        private void singleResCont_Delete_Click(object sender, EventArgs e)
        {
            if (results_Details.SelectedItems == null) return;
            new LogMsg("Detail Menu-Context: Opening Delete sources form...", eDebugMsgLvl.INFO);
            if (MessageBox.Show("Czy napewno usunąć zaznaczone pliki?", "Potwierdzenie usunięcia",MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                return;

            resultsManager.deleteResultsFiles(results_Details.SelectedItems);
        }
        // Context: Zmiana instancji w zaznaczonych wynikach
        private void singleResCont_ChangeInstance_Click(object sender, EventArgs e)
        {
            new LogMsg("Detail Menu-Context: Opening Change instances form...", eDebugMsgLvl.INFO);
            if (ChangeInstanceMsgBox.Show() == System.Windows.Forms.DialogResult.Cancel)
                return;
            new LogMsg("Detail Menu-Context: Change instances, new instance: "+ChangeInstanceMsgBox.choosenID, eDebugMsgLvl.INFO);
            resultsManager.changeResultInstances(results_Details.SelectedItems, ChangeInstanceMsgBox.choosenID);
        }
        private void _initialize_resultsFilter()
        {
            new LogMsg("Initializing results filter", eDebugMsgLvl.INFO);
            for (int i = 0; i < (int)findResultType.MAX; i++)
            {
                findResultType hlp = findResultType.NPC;
                sResults_Combo_resFilter.Items.Add(hlp.getAtIndex(i).getName());
            }
            sResults_Combo_resFilter.Items.Add("Brak");
        }
        private void sResults_Combo_resFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sResults_Combo_resFilter.SelectedItem.Equals("Brak"))
                sResultsFilter = null;
            else
                sResultsFilter = findResultType.NPC.getAtIndex(sResults_Combo_resFilter.SelectedIndex);
            new LogMsg("Detail-Filter: New Filter: "+sResultsFilter, eDebugMsgLvl.INFO);
            // Redraw detailed results
            results_Summary_SelectedIndexChanged(results_Summary, new EventArgs());
            results_Summary.Refresh();
        }

        private void btn_showOnMap_Click(object sender, EventArgs e)
        {
            if (results_Summary.SelectedItems != null)
            {
                new LogMsg("Show-Map: Opening form...", eDebugMsgLvl.INFO);
                List<searchResult> resList = new List<searchResult>();
                foreach (searchResultSummary sum in results_Summary.SelectedItems)
                {
                    foreach (searchResult res in sum)
                    {
                        resList.Add(res);
                    }
                }
                new mapPreview(resList).Show(this);
            }
            else if (results_Details.SelectedItems != null)
            {
                new mapPreview(results_Details.SelectedItems as ICollection<searchResult>).Show(this);
            }
            else
                new mapPreview(new List<searchResult>()).Show(this);
        }
    }
}