namespace GothicItemsLookup
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed` otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip_ProgressWhole = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip_ProgressBarPartial = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab_srchItems = new System.Windows.Forms.TabPage();
            this.sItems_btn_removeSelectedItms = new System.Windows.Forms.Button();
            this.sItems_btn_clearItems = new System.Windows.Forms.Button();
            this.sItems_SelectedItems = new System.Windows.Forms.ListBox();
            this.sItems_btn_GetItems = new System.Windows.Forms.Button();
            this.sItems_FilterGroup = new System.Windows.Forms.GroupBox();
            this.sItems_CaseSensitive = new System.Windows.Forms.CheckBox();
            this.sItems_inp_FilterItemsInstance = new System.Windows.Forms.TextBox();
            this.sItems_btn_ItemsScriptsClearAll = new System.Windows.Forms.Button();
            this.sItems_btn_ItemsGetFiles = new System.Windows.Forms.Button();
            this.sItems_ScriptFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_srchScriptsAndWorlds = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sWorld_worldsList = new System.Windows.Forms.ListBox();
            this.sWorld_btn_ClearWorlds = new System.Windows.Forms.Button();
            this.sWorld_label1 = new System.Windows.Forms.Label();
            this.sWorld_Choose = new System.Windows.Forms.Button();
            this.sScripts_btn_Default = new System.Windows.Forms.Button();
            this.sScripts_getGothicSRC = new System.Windows.Forms.Button();
            this.sScripts_list_itemGenerFuncs = new System.Windows.Forms.ListBox();
            this.sScripts_btn_Clear = new System.Windows.Forms.Button();
            this.sScripts_btn_addFunc = new System.Windows.Forms.Button();
            this.sScripts_label2 = new System.Windows.Forms.Label();
            this.sScripts_GothicSRC = new System.Windows.Forms.TextBox();
            this.sScripts_label1 = new System.Windows.Forms.Label();
            this.tab_srchResults = new System.Windows.Forms.TabPage();
            this.sResults_Combo_DetailsFilter = new System.Windows.Forms.ComboBox();
            this.btn_showOnMap = new System.Windows.Forms.Button();
            this.results_Details = new System.Windows.Forms.ListBox();
            this.singleResultsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.singleResCont_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.singleResCont_MoreInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.results_Summary = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orcwarriorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFilesGeneric = new System.Windows.Forms.OpenFileDialog();
            this.btn_SearchForItems = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tab_srchItems.SuspendLayout();
            this.sItems_FilterGroup.SuspendLayout();
            this.tab_srchScriptsAndWorlds.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tab_srchResults.SuspendLayout();
            this.singleResultsContext.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_ProgressWhole,
            this.toolStrip_ProgressBarPartial,
            this.toolStrip_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(887, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip_ProgressWhole
            // 
            this.toolStrip_ProgressWhole.Name = "toolStrip_ProgressWhole";
            this.toolStrip_ProgressWhole.Size = new System.Drawing.Size(100, 16);
            this.toolStrip_ProgressWhole.Visible = false;
            // 
            // toolStrip_ProgressBarPartial
            // 
            this.toolStrip_ProgressBarPartial.Name = "toolStrip_ProgressBarPartial";
            this.toolStrip_ProgressBarPartial.Size = new System.Drawing.Size(100, 16);
            this.toolStrip_ProgressBarPartial.Visible = false;
            // 
            // toolStrip_Status
            // 
            this.toolStrip_Status.Name = "toolStrip_Status";
            this.toolStrip_Status.Size = new System.Drawing.Size(48, 17);
            this.toolStrip_Status.Text = "Gotowy";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tab_srchItems);
            this.tabControl.Controls.Add(this.tab_srchScriptsAndWorlds);
            this.tabControl.Controls.Add(this.tab_srchResults);
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(887, 382);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 1;
            // 
            // tab_srchItems
            // 
            this.tab_srchItems.BackColor = System.Drawing.Color.Transparent;
            this.tab_srchItems.Controls.Add(this.sItems_btn_removeSelectedItms);
            this.tab_srchItems.Controls.Add(this.sItems_btn_clearItems);
            this.tab_srchItems.Controls.Add(this.sItems_SelectedItems);
            this.tab_srchItems.Controls.Add(this.sItems_btn_GetItems);
            this.tab_srchItems.Controls.Add(this.sItems_FilterGroup);
            this.tab_srchItems.Controls.Add(this.sItems_btn_ItemsScriptsClearAll);
            this.tab_srchItems.Controls.Add(this.sItems_btn_ItemsGetFiles);
            this.tab_srchItems.Controls.Add(this.sItems_ScriptFiles);
            this.tab_srchItems.Controls.Add(this.label1);
            this.tab_srchItems.Location = new System.Drawing.Point(4, 22);
            this.tab_srchItems.Name = "tab_srchItems";
            this.tab_srchItems.Padding = new System.Windows.Forms.Padding(3);
            this.tab_srchItems.Size = new System.Drawing.Size(879, 356);
            this.tab_srchItems.TabIndex = 0;
            this.tab_srchItems.Text = "Szukane Itemy";
            // 
            // sItems_btn_removeSelectedItms
            // 
            this.sItems_btn_removeSelectedItms.Location = new System.Drawing.Point(273, 208);
            this.sItems_btn_removeSelectedItms.Name = "sItems_btn_removeSelectedItms";
            this.sItems_btn_removeSelectedItms.Size = new System.Drawing.Size(104, 23);
            this.sItems_btn_removeSelectedItms.TabIndex = 9;
            this.sItems_btn_removeSelectedItms.Text = "Usuń zaznaczone";
            this.sItems_btn_removeSelectedItms.UseVisualStyleBackColor = true;
            this.sItems_btn_removeSelectedItms.Click += new System.EventHandler(this.sItems_btn_removeSelectedItms_Click);
            // 
            // sItems_btn_clearItems
            // 
            this.sItems_btn_clearItems.Location = new System.Drawing.Point(273, 250);
            this.sItems_btn_clearItems.Name = "sItems_btn_clearItems";
            this.sItems_btn_clearItems.Size = new System.Drawing.Size(104, 23);
            this.sItems_btn_clearItems.TabIndex = 8;
            this.sItems_btn_clearItems.Text = "Usuń wszystkie";
            this.sItems_btn_clearItems.UseVisualStyleBackColor = true;
            this.sItems_btn_clearItems.Click += new System.EventHandler(this.sItems_btn_clearItems_Click);
            // 
            // sItems_SelectedItems
            // 
            this.sItems_SelectedItems.ColumnWidth = 110;
            this.sItems_SelectedItems.FormattingEnabled = true;
            this.sItems_SelectedItems.Location = new System.Drawing.Point(444, 9);
            this.sItems_SelectedItems.MultiColumn = true;
            this.sItems_SelectedItems.Name = "sItems_SelectedItems";
            this.sItems_SelectedItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.sItems_SelectedItems.Size = new System.Drawing.Size(427, 303);
            this.sItems_SelectedItems.TabIndex = 7;
            this.sItems_SelectedItems.SelectedIndexChanged += new System.EventHandler(this.sItems_SelectedItems_SelectedIndexChanged);
            // 
            // sItems_btn_GetItems
            // 
            this.sItems_btn_GetItems.Location = new System.Drawing.Point(273, 166);
            this.sItems_btn_GetItems.Name = "sItems_btn_GetItems";
            this.sItems_btn_GetItems.Size = new System.Drawing.Size(104, 23);
            this.sItems_btn_GetItems.TabIndex = 6;
            this.sItems_btn_GetItems.Text = "Wyłuskaj itemy";
            this.sItems_btn_GetItems.UseVisualStyleBackColor = true;
            this.sItems_btn_GetItems.Click += new System.EventHandler(this.sItems_btn_GetItems_Click);
            // 
            // sItems_FilterGroup
            // 
            this.sItems_FilterGroup.Controls.Add(this.sItems_CaseSensitive);
            this.sItems_FilterGroup.Controls.Add(this.sItems_inp_FilterItemsInstance);
            this.sItems_FilterGroup.Location = new System.Drawing.Point(231, 35);
            this.sItems_FilterGroup.Name = "sItems_FilterGroup";
            this.sItems_FilterGroup.Size = new System.Drawing.Size(177, 100);
            this.sItems_FilterGroup.TabIndex = 5;
            this.sItems_FilterGroup.TabStop = false;
            this.sItems_FilterGroup.Text = "Filtrowanie instance(Wildcards):";
            // 
            // sItems_CaseSensitive
            // 
            this.sItems_CaseSensitive.AutoSize = true;
            this.sItems_CaseSensitive.Checked = true;
            this.sItems_CaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sItems_CaseSensitive.Location = new System.Drawing.Point(6, 65);
            this.sItems_CaseSensitive.Name = "sItems_CaseSensitive";
            this.sItems_CaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.sItems_CaseSensitive.TabIndex = 1;
            this.sItems_CaseSensitive.Text = "Case Sensitive";
            this.sItems_CaseSensitive.UseVisualStyleBackColor = true;
            // 
            // sItems_inp_FilterItemsInstance
            // 
            this.sItems_inp_FilterItemsInstance.Location = new System.Drawing.Point(6, 28);
            this.sItems_inp_FilterItemsInstance.Name = "sItems_inp_FilterItemsInstance";
            this.sItems_inp_FilterItemsInstance.Size = new System.Drawing.Size(165, 20);
            this.sItems_inp_FilterItemsInstance.TabIndex = 0;
            // 
            // sItems_btn_ItemsScriptsClearAll
            // 
            this.sItems_btn_ItemsScriptsClearAll.Location = new System.Drawing.Point(22, 279);
            this.sItems_btn_ItemsScriptsClearAll.Name = "sItems_btn_ItemsScriptsClearAll";
            this.sItems_btn_ItemsScriptsClearAll.Size = new System.Drawing.Size(75, 22);
            this.sItems_btn_ItemsScriptsClearAll.TabIndex = 3;
            this.sItems_btn_ItemsScriptsClearAll.Text = "Wyczyść";
            this.sItems_btn_ItemsScriptsClearAll.UseVisualStyleBackColor = true;
            this.sItems_btn_ItemsScriptsClearAll.Click += new System.EventHandler(this.btn_ItemsScriptsClearAll_Click);
            // 
            // sItems_btn_ItemsGetFiles
            // 
            this.sItems_btn_ItemsGetFiles.Location = new System.Drawing.Point(131, 279);
            this.sItems_btn_ItemsGetFiles.Name = "sItems_btn_ItemsGetFiles";
            this.sItems_btn_ItemsGetFiles.Size = new System.Drawing.Size(75, 23);
            this.sItems_btn_ItemsGetFiles.TabIndex = 2;
            this.sItems_btn_ItemsGetFiles.Text = "...";
            this.sItems_btn_ItemsGetFiles.UseVisualStyleBackColor = true;
            this.sItems_btn_ItemsGetFiles.Click += new System.EventHandler(this.btn_SelectItemsScripts_Click);
            // 
            // sItems_ScriptFiles
            // 
            this.sItems_ScriptFiles.FormattingEnabled = true;
            this.sItems_ScriptFiles.Location = new System.Drawing.Point(22, 35);
            this.sItems_ScriptFiles.Name = "sItems_ScriptFiles";
            this.sItems_ScriptFiles.Size = new System.Drawing.Size(184, 238);
            this.sItems_ScriptFiles.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pliki Itemów:";
            // 
            // tab_srchScriptsAndWorlds
            // 
            this.tab_srchScriptsAndWorlds.BackColor = System.Drawing.Color.Transparent;
            this.tab_srchScriptsAndWorlds.Controls.Add(this.splitContainer1);
            this.tab_srchScriptsAndWorlds.Location = new System.Drawing.Point(4, 22);
            this.tab_srchScriptsAndWorlds.Name = "tab_srchScriptsAndWorlds";
            this.tab_srchScriptsAndWorlds.Size = new System.Drawing.Size(879, 356);
            this.tab_srchScriptsAndWorlds.TabIndex = 2;
            this.tab_srchScriptsAndWorlds.Text = "Pliki do Przeszukania";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sWorld_worldsList);
            this.splitContainer1.Panel1.Controls.Add(this.sWorld_btn_ClearWorlds);
            this.splitContainer1.Panel1.Controls.Add(this.sWorld_label1);
            this.splitContainer1.Panel1.Controls.Add(this.sWorld_Choose);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_btn_Default);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_getGothicSRC);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_list_itemGenerFuncs);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_btn_Clear);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_btn_addFunc);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_label2);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_GothicSRC);
            this.splitContainer1.Panel2.Controls.Add(this.sScripts_label1);
            this.splitContainer1.Size = new System.Drawing.Size(879, 322);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.TabIndex = 8;
            // 
            // sWorld_worldsList
            // 
            this.sWorld_worldsList.FormattingEnabled = true;
            this.sWorld_worldsList.Location = new System.Drawing.Point(22, 38);
            this.sWorld_worldsList.Name = "sWorld_worldsList";
            this.sWorld_worldsList.Size = new System.Drawing.Size(184, 238);
            this.sWorld_worldsList.TabIndex = 9;
            // 
            // sWorld_btn_ClearWorlds
            // 
            this.sWorld_btn_ClearWorlds.Location = new System.Drawing.Point(22, 282);
            this.sWorld_btn_ClearWorlds.Name = "sWorld_btn_ClearWorlds";
            this.sWorld_btn_ClearWorlds.Size = new System.Drawing.Size(75, 23);
            this.sWorld_btn_ClearWorlds.TabIndex = 11;
            this.sWorld_btn_ClearWorlds.Text = "Wyczyść";
            this.sWorld_btn_ClearWorlds.UseVisualStyleBackColor = true;
            this.sWorld_btn_ClearWorlds.Click += new System.EventHandler(this.sWorld_btn_ClearWorlds_Click);
            // 
            // sWorld_label1
            // 
            this.sWorld_label1.AutoSize = true;
            this.sWorld_label1.Location = new System.Drawing.Point(22, 17);
            this.sWorld_label1.Name = "sWorld_label1";
            this.sWorld_label1.Size = new System.Drawing.Size(72, 13);
            this.sWorld_label1.TabIndex = 8;
            this.sWorld_label1.Text = "Pliki Światów:";
            // 
            // sWorld_Choose
            // 
            this.sWorld_Choose.Location = new System.Drawing.Point(128, 282);
            this.sWorld_Choose.Name = "sWorld_Choose";
            this.sWorld_Choose.Size = new System.Drawing.Size(75, 23);
            this.sWorld_Choose.TabIndex = 10;
            this.sWorld_Choose.Text = "...";
            this.sWorld_Choose.UseVisualStyleBackColor = true;
            this.sWorld_Choose.Click += new System.EventHandler(this.sWorld_Choose_Click);
            // 
            // sScripts_btn_Default
            // 
            this.sScripts_btn_Default.Location = new System.Drawing.Point(118, 282);
            this.sScripts_btn_Default.Name = "sScripts_btn_Default";
            this.sScripts_btn_Default.Size = new System.Drawing.Size(75, 23);
            this.sScripts_btn_Default.TabIndex = 16;
            this.sScripts_btn_Default.Text = "Przywróc";
            this.sScripts_btn_Default.UseVisualStyleBackColor = true;
            this.sScripts_btn_Default.Click += new System.EventHandler(this.sScripts_btn_itemAddFuncsDefault_Click);
            // 
            // sScripts_getGothicSRC
            // 
            this.sScripts_getGothicSRC.Location = new System.Drawing.Point(240, 37);
            this.sScripts_getGothicSRC.Name = "sScripts_getGothicSRC";
            this.sScripts_getGothicSRC.Size = new System.Drawing.Size(47, 22);
            this.sScripts_getGothicSRC.TabIndex = 15;
            this.sScripts_getGothicSRC.Text = "...";
            this.sScripts_getGothicSRC.UseVisualStyleBackColor = true;
            this.sScripts_getGothicSRC.Click += new System.EventHandler(this.sScripts_getGothicSRC_Click);
            // 
            // sScripts_list_itemGenerFuncs
            // 
            this.sScripts_list_itemGenerFuncs.FormattingEnabled = true;
            this.sScripts_list_itemGenerFuncs.Location = new System.Drawing.Point(21, 116);
            this.sScripts_list_itemGenerFuncs.Name = "sScripts_list_itemGenerFuncs";
            this.sScripts_list_itemGenerFuncs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.sScripts_list_itemGenerFuncs.Size = new System.Drawing.Size(266, 160);
            this.sScripts_list_itemGenerFuncs.TabIndex = 12;
            // 
            // sScripts_btn_Clear
            // 
            this.sScripts_btn_Clear.Location = new System.Drawing.Point(21, 282);
            this.sScripts_btn_Clear.Name = "sScripts_btn_Clear";
            this.sScripts_btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.sScripts_btn_Clear.TabIndex = 14;
            this.sScripts_btn_Clear.Text = "Usuń";
            this.sScripts_btn_Clear.UseVisualStyleBackColor = true;
            this.sScripts_btn_Clear.Click += new System.EventHandler(this.sScripts_btn_Clear_Click);
            // 
            // sScripts_btn_addFunc
            // 
            this.sScripts_btn_addFunc.Enabled = false;
            this.sScripts_btn_addFunc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sScripts_btn_addFunc.Location = new System.Drawing.Point(212, 282);
            this.sScripts_btn_addFunc.Name = "sScripts_btn_addFunc";
            this.sScripts_btn_addFunc.Size = new System.Drawing.Size(75, 23);
            this.sScripts_btn_addFunc.TabIndex = 13;
            this.sScripts_btn_addFunc.Text = "Dodaj";
            this.sScripts_btn_addFunc.UseVisualStyleBackColor = true;
            // 
            // sScripts_label2
            // 
            this.sScripts_label2.AutoSize = true;
            this.sScripts_label2.Location = new System.Drawing.Point(18, 82);
            this.sScripts_label2.Name = "sScripts_label2";
            this.sScripts_label2.Size = new System.Drawing.Size(134, 13);
            this.sScripts_label2.TabIndex = 11;
            this.sScripts_label2.Text = "Funkcje Generujące Itemy:";
            // 
            // sScripts_GothicSRC
            // 
            this.sScripts_GothicSRC.Location = new System.Drawing.Point(21, 38);
            this.sScripts_GothicSRC.Name = "sScripts_GothicSRC";
            this.sScripts_GothicSRC.Size = new System.Drawing.Size(222, 20);
            this.sScripts_GothicSRC.TabIndex = 10;
            // 
            // sScripts_label1
            // 
            this.sScripts_label1.AutoSize = true;
            this.sScripts_label1.Location = new System.Drawing.Point(18, 17);
            this.sScripts_label1.Name = "sScripts_label1";
            this.sScripts_label1.Size = new System.Drawing.Size(76, 13);
            this.sScripts_label1.TabIndex = 9;
            this.sScripts_label1.Text = "Plik gothic.src:";
            // 
            // tab_srchResults
            // 
            this.tab_srchResults.BackColor = System.Drawing.Color.Transparent;
            this.tab_srchResults.Controls.Add(this.sResults_Combo_DetailsFilter);
            this.tab_srchResults.Controls.Add(this.btn_showOnMap);
            this.tab_srchResults.Controls.Add(this.results_Details);
            this.tab_srchResults.Controls.Add(this.results_Summary);
            this.tab_srchResults.Location = new System.Drawing.Point(4, 22);
            this.tab_srchResults.Name = "tab_srchResults";
            this.tab_srchResults.Size = new System.Drawing.Size(879, 356);
            this.tab_srchResults.TabIndex = 3;
            this.tab_srchResults.Text = "Wyniki";
            // 
            // sResults_Combo_DetailsFilter
            // 
            this.sResults_Combo_DetailsFilter.FormattingEnabled = true;
            this.sResults_Combo_DetailsFilter.Location = new System.Drawing.Point(142, 329);
            this.sResults_Combo_DetailsFilter.Name = "sResults_Combo_DetailsFilter";
            this.sResults_Combo_DetailsFilter.Size = new System.Drawing.Size(150, 21);
            this.sResults_Combo_DetailsFilter.TabIndex = 11;
            this.sResults_Combo_DetailsFilter.SelectedIndexChanged += new System.EventHandler(this.sResults_Combo_DetailsFilter_SelectedIndexChanged);
            // 
            // btn_showOnMap
            // 
            this.btn_showOnMap.Location = new System.Drawing.Point(8, 328);
            this.btn_showOnMap.Name = "btn_showOnMap";
            this.btn_showOnMap.Size = new System.Drawing.Size(117, 23);
            this.btn_showOnMap.TabIndex = 10;
            this.btn_showOnMap.Text = "Pokaż na mapie";
            this.btn_showOnMap.UseVisualStyleBackColor = true;
            this.btn_showOnMap.Click += new System.EventHandler(this.btn_showOnMap_Click);
            // 
            // results_Details
            // 
            this.results_Details.ColumnWidth = 300;
            this.results_Details.ContextMenuStrip = this.singleResultsContext;
            this.results_Details.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.results_Details.FormattingEnabled = true;
            this.results_Details.Location = new System.Drawing.Point(8, 201);
            this.results_Details.MultiColumn = true;
            this.results_Details.Name = "results_Details";
            this.results_Details.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.results_Details.Size = new System.Drawing.Size(863, 121);
            this.results_Details.TabIndex = 9;
            this.results_Details.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.result_Details_DrawItem);
            // 
            // singleResultsContext
            // 
            this.singleResultsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleResCont_Run,
            this.singleResCont_MoreInfos});
            this.singleResultsContext.Name = "singleResultsContext";
            this.singleResultsContext.Size = new System.Drawing.Size(240, 48);
            // 
            // singleResCont_Run
            // 
            this.singleResCont_Run.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.singleResCont_Run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.singleResCont_Run.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.singleResCont_Run.Name = "singleResCont_Run";
            this.singleResCont_Run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.singleResCont_Run.Size = new System.Drawing.Size(239, 22);
            this.singleResCont_Run.Text = "Uruchom plik zródłowy";
            this.singleResCont_Run.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.singleResCont_Run.Click += new System.EventHandler(this.singleResCont_Run_Click);
            // 
            // singleResCont_MoreInfos
            // 
            this.singleResCont_MoreInfos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.singleResCont_MoreInfos.Name = "singleResCont_MoreInfos";
            this.singleResCont_MoreInfos.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.singleResCont_MoreInfos.Size = new System.Drawing.Size(239, 22);
            this.singleResCont_MoreInfos.Text = "Więcej info...";
            this.singleResCont_MoreInfos.Click += new System.EventHandler(this.singleResCont_MoreInfos_Click);
            // 
            // results_Summary
            // 
            this.results_Summary.ColumnWidth = 180;
            this.results_Summary.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.results_Summary.FormattingEnabled = true;
            this.results_Summary.Location = new System.Drawing.Point(8, 6);
            this.results_Summary.MultiColumn = true;
            this.results_Summary.Name = "results_Summary";
            this.results_Summary.Size = new System.Drawing.Size(863, 186);
            this.results_Summary.TabIndex = 8;
            this.results_Summary.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.results_Summary_DrawItem);
            this.results_Summary.SelectedIndexChanged += new System.EventHandler(this.results_Summary_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.orcwarriorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wczytajToolStripMenuItem,
            this.zapiszToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.plikToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // wczytajToolStripMenuItem
            // 
            this.wczytajToolStripMenuItem.Enabled = false;
            this.wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            this.wczytajToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.wczytajToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.wczytajToolStripMenuItem.Text = "Wczytaj ...";
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Enabled = false;
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.zapiszToolStripMenuItem.Text = "Zapisz...";
            // 
            // orcwarriorToolStripMenuItem
            // 
            this.orcwarriorToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.orcwarriorToolStripMenuItem.Enabled = false;
            this.orcwarriorToolStripMenuItem.Name = "orcwarriorToolStripMenuItem";
            this.orcwarriorToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.orcwarriorToolStripMenuItem.Text = "orcwarrior 2013";
            // 
            // selectFilesGeneric
            // 
            this.selectFilesGeneric.Filter = "Gothic Script|*.d";
            this.selectFilesGeneric.Multiselect = true;
            this.selectFilesGeneric.Title = "Wybierz pliki";
            // 
            // btn_SearchForItems
            // 
            this.btn_SearchForItems.Location = new System.Drawing.Point(766, 406);
            this.btn_SearchForItems.Name = "btn_SearchForItems";
            this.btn_SearchForItems.Size = new System.Drawing.Size(117, 23);
            this.btn_SearchForItems.TabIndex = 3;
            this.btn_SearchForItems.Text = "Szukaj Itemów!";
            this.btn_SearchForItems.UseVisualStyleBackColor = true;
            this.btn_SearchForItems.Click += new System.EventHandler(this.btn_SearchForItems_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 454);
            this.Controls.Add(this.btn_SearchForItems);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Main";
            this.ShowIcon = false;
            this.Text = "Gothic Items Lookup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tab_srchItems.ResumeLayout(false);
            this.tab_srchItems.PerformLayout();
            this.sItems_FilterGroup.ResumeLayout(false);
            this.sItems_FilterGroup.PerformLayout();
            this.tab_srchScriptsAndWorlds.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tab_srchResults.ResumeLayout(false);
            this.singleResultsContext.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tab_srchItems;
        private System.Windows.Forms.TabPage tab_srchScriptsAndWorlds;
        private System.Windows.Forms.TabPage tab_srchResults;
        private System.Windows.Forms.ListBox sItems_ScriptFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.Button sItems_btn_ItemsGetFiles;
        private System.Windows.Forms.OpenFileDialog selectFilesGeneric;
        private System.Windows.Forms.Button sItems_btn_ItemsScriptsClearAll;
        private System.Windows.Forms.GroupBox sItems_FilterGroup;
        private System.Windows.Forms.CheckBox sItems_CaseSensitive;
        private System.Windows.Forms.TextBox sItems_inp_FilterItemsInstance;
        private System.Windows.Forms.Button sItems_btn_GetItems;
        private System.Windows.Forms.ListBox sItems_SelectedItems;
        private System.Windows.Forms.ToolStripStatusLabel toolStrip_Status;
        private System.Windows.Forms.Button sItems_btn_clearItems;
        private System.Windows.Forms.Button sItems_btn_removeSelectedItms;
        private System.Windows.Forms.ToolStripProgressBar toolStrip_ProgressWhole;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox sWorld_worldsList;
        private System.Windows.Forms.Button sWorld_btn_ClearWorlds;
        private System.Windows.Forms.Label sWorld_label1;
        private System.Windows.Forms.Button sWorld_Choose;
        private System.Windows.Forms.Button sScripts_getGothicSRC;
        private System.Windows.Forms.ListBox sScripts_list_itemGenerFuncs;
        private System.Windows.Forms.Button sScripts_btn_Clear;
        private System.Windows.Forms.Button sScripts_btn_addFunc;
        private System.Windows.Forms.Label sScripts_label2;
        private System.Windows.Forms.TextBox sScripts_GothicSRC;
        private System.Windows.Forms.Label sScripts_label1;
        private System.Windows.Forms.Button sScripts_btn_Default;
        private System.Windows.Forms.Button btn_SearchForItems;
        private System.Windows.Forms.ToolStripProgressBar toolStrip_ProgressBarPartial;
        private System.Windows.Forms.ListBox results_Summary;
        private System.Windows.Forms.ListBox results_Details;
        private System.Windows.Forms.Button btn_showOnMap;
        private System.Windows.Forms.ToolStripMenuItem orcwarriorToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip singleResultsContext;
        private System.Windows.Forms.ToolStripMenuItem singleResCont_Run;
        private System.Windows.Forms.ToolStripMenuItem singleResCont_MoreInfos;
        private System.Windows.Forms.ComboBox sResults_Combo_DetailsFilter;

    }
}

