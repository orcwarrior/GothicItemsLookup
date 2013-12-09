using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using GothicItemsLookup.parsers;
using GothicItemsLookup.Scanners;
using System.Drawing;
using GothicItemsLookup.Results;
namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {
        findResultType? sResultsDetailsFilter;
        private void results_Summary_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (0 > e.Index || e.Index > (sender as ListBox).Items.Count) return;
            searchResultSummary cur = results_Summary.Items[e.Index] as searchResultSummary;
            Color BG = e.BackColor;
            if (cur.amount == 0)
                BG = ColorsConst.resultSummary_NotFound;
            else
                BG = System.Drawing.Color.FromArgb(255, Math.Max(255 - cur.amount / 3, 140), Math.Max(255 - cur.amount * 3, 80));
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                BG = System.Drawing.Color.FromArgb(BG.R - 100, BG.G - 100, BG.B - 80);
            e.Graphics.FillRectangle(new SolidBrush(BG), e.Bounds);
            e.Graphics.DrawString(cur.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Location);
        }

        private void results_Summary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                searchResultSummary sum = (sender as ListBox).SelectedItem as searchResultSummary;
                results_Details.Items.Clear();
                foreach (searchResult res in sum)
                    results_Details.Items.Add(res);
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
            e.Graphics.DrawString(cur.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Location);
        }


        private void singleResCont_MoreInfos_Click(object sender, EventArgs e)
        {

            searchResult res = results_Details.SelectedItem as searchResult;
            if (res != null)
                res.MoreInfos().Show(this);
        }

        private void singleResCont_Run_Click(object sender, EventArgs e)
        {
            //
            string nppPath = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\notepad++.exe", "", null);

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

        private void _initialize_resultDetailsFilter()
        {
            for (int i = 0; i < (int)findResultType.MAX; i++)
            {
                findResultType hlp = findResultType.NPC;
                sResults_Combo_DetailsFilter.Items.Add(hlp.getAtIndex(i).getName());
            }
            sResults_Combo_DetailsFilter.Items.Add("Brak");
        }
        private void sResults_Combo_DetailsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sResults_Combo_DetailsFilter.SelectedItem.Equals("Brak"))
                sResultsDetailsFilter = null;
            else
                sResultsDetailsFilter = findResultType.NPC.getAtIndex(sResults_Combo_DetailsFilter.SelectedIndex);

        }

        private void btn_showOnMap_Click(object sender, EventArgs e)
        {

            if (results_Summary.SelectedItems != null)
            {
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