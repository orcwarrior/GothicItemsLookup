using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using GothicItemsLookup.parsers;
using GothicItemsLookup.Scanners;
using GothicItemsLookup.Scanners.Utils;
namespace GothicItemsLookup
{
    public partial class Form_Main : Form
    {
        List<itemAddingFunc> currentFunctions = new List<itemAddingFunc>();
        // World Part:
        // ***************************
        private void sWorld_btn_ClearWorlds_Click(object sender, EventArgs e)
        {
            sWorld_worldsList.Items.Clear();
            _testIfCanRunItemsSearch();
        }
        private void sWorld_Choose_Click(object sender, EventArgs e)
        {
            selectFilesGeneric.Reset();
            selectFilesGeneric.Multiselect = true;
            if (Properties.Settings.Default.path_Worlds != null)
                selectFilesGeneric.InitialDirectory = Properties.Settings.Default.path_Worlds;
            selectFilesGeneric.Filter = "ZEN (NOT BINARY!)|*.ZEN";
            selectFilesGeneric.ShowDialog(this);
            foreach (string file in selectFilesGeneric.FileNames)
                sWorld_worldsList.Items.Add(new FileEntry(file));
            if (selectFilesGeneric.FileNames.Length > 0) // wybrano coś:
                Properties.Settings.Default.path_Worlds = Utils.ExctractPath(selectFilesGeneric.FileNames[0]);
            _testIfCanRunItemsSearch();
        }

        // Scripts Part:
        // ***************************
        private void sScripts_getGothicSRC_Click(object sender, EventArgs e)
        {
            selectFilesGeneric.Reset();
            selectFilesGeneric.Multiselect = false;
            if (Properties.Settings.Default.path_GothicSRC != null)
                selectFilesGeneric.InitialDirectory = Properties.Settings.Default.path_GothicSRC;
            selectFilesGeneric.Filter = "Plik Gothic.src|Gothic.src";
            selectFilesGeneric.ShowDialog(this);
            foreach (string file in selectFilesGeneric.FileNames)
                sScripts_GothicSRC.Text = file;
            if (selectFilesGeneric.FileNames.Length > 0) // wybrano coś:
                Properties.Settings.Default.path_GothicSRC = Utils.ExctractPath(selectFilesGeneric.FileNames[0]);
            _testIfCanRunItemsSearch();
        }
        private void _initialize_itemAddingFunctions()
        {
            List<itemAddingFunc> defaultItems = _createDefaultItemsAddingFuncs();
            currentFunctions.AddRange(defaultItems);
            sScripts_list_itemGenerFuncs.Items.AddRange(defaultItems.ToArray());
        }

        private List<itemAddingFunc> _createDefaultItemsAddingFuncs()
        {
            List<itemAddingFunc> list = new List<itemAddingFunc>();
            list.Add(new itemAddingFunc("CreateInvItems", //NOTE: MUSI BYC PRZED CreateInvItem
                     new eItemFunc_Param[]{eItemFunc_Param.srcNpc,
                                           eItemFunc_Param.itemID,
                                           eItemFunc_Param.count}));
            list.Add(new itemAddingFunc("CreateInvItem",
                     new eItemFunc_Param[]{eItemFunc_Param.srcNpc,
                                           eItemFunc_Param.itemID}));
            list.Add(new itemAddingFunc("Wld_InsertItem",
                     new eItemFunc_Param[]{eItemFunc_Param.itemID,
                                           eItemFunc_Param.wp}));
            list.Add(new itemAddingFunc("EquipItem",
                     new eItemFunc_Param[]{eItemFunc_Param.srcNpc,
                                           eItemFunc_Param.itemID}));
            list.Add(new itemAddingFunc("B_GiveInvItems",
                     new eItemFunc_Param[]{eItemFunc_Param.srcNpc,
                                           eItemFunc_Param.dstNpc,
                                           eItemFunc_Param.itemID,
                                           eItemFunc_Param.count}));

            return list;

        }
        // Back to default settings:
        private void sScripts_btn_itemAddFuncsDefault_Click(object sender, EventArgs e)
        {
            sScripts_list_itemGenerFuncs.Items.Clear();
            currentFunctions.Clear();
            _initialize_itemAddingFunctions();
        }
        // Remove selected items:
        private void sScripts_btn_Clear_Click(object sender, EventArgs e)
        {
            while (sScripts_list_itemGenerFuncs.SelectedItems.Count > 0)
            {
                itemAddingFunc func = sScripts_list_itemGenerFuncs.SelectedItems[0] as itemAddingFunc;
                sScripts_list_itemGenerFuncs.Items.Remove(func);
                currentFunctions.Remove(func);
            }
        }
    }

}
