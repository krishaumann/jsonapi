﻿namespace JSONAPI.Controls
{
	partial class JavaScriptEditorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.TextPanel = new System.Windows.Forms.Panel();
            this.FileName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.indentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outdentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.uppercaseSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowercaseSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.goToLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indentGuidesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenCharactersItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.BtnNextSearch = new System.Windows.Forms.Button();
            this.BtnPrevSearch = new System.Windows.Forms.Button();
            this.BtnCloseSearch = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.closeEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.PanelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextPanel
            // 
            this.TextPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPanel.Location = new System.Drawing.Point(7, 71);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(651, 389);
            this.TextPanel.TabIndex = 0;
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Font = new System.Drawing.Font("Segoe UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FileName.Location = new System.Drawing.Point(10, 34);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(52, 30);
            this.FileName.TabIndex = 1;
            this.FileName.Text = "Title";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(673, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.executeScriptToolStripMenuItem,
            this.closeEditorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // executeScriptToolStripMenuItem
            // 
            this.executeScriptToolStripMenuItem.Name = "executeScriptToolStripMenuItem";
            this.executeScriptToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.executeScriptToolStripMenuItem.Text = "Execute Script";
            this.executeScriptToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectLineToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.toolStripSeparator2,
            this.indentSelectionToolStripMenuItem,
            this.outdentSelectionToolStripMenuItem,
            this.toolStripSeparator3,
            this.uppercaseSelectionToolStripMenuItem,
            this.lowercaseSelectionToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // selectLineToolStripMenuItem
            // 
            this.selectLineToolStripMenuItem.Name = "selectLineToolStripMenuItem";
            this.selectLineToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.selectLineToolStripMenuItem.Text = "Select Line";
            this.selectLineToolStripMenuItem.Click += new System.EventHandler(this.selectLineToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // indentSelectionToolStripMenuItem
            // 
            this.indentSelectionToolStripMenuItem.Name = "indentSelectionToolStripMenuItem";
            this.indentSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Tab";
            this.indentSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.indentSelectionToolStripMenuItem.Text = "Indent";
            this.indentSelectionToolStripMenuItem.Click += new System.EventHandler(this.indentSelectionToolStripMenuItem_Click);
            // 
            // outdentSelectionToolStripMenuItem
            // 
            this.outdentSelectionToolStripMenuItem.Name = "outdentSelectionToolStripMenuItem";
            this.outdentSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Shift+Tab";
            this.outdentSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.outdentSelectionToolStripMenuItem.Text = "Outdent";
            this.outdentSelectionToolStripMenuItem.Click += new System.EventHandler(this.outdentSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // uppercaseSelectionToolStripMenuItem
            // 
            this.uppercaseSelectionToolStripMenuItem.Name = "uppercaseSelectionToolStripMenuItem";
            this.uppercaseSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+U";
            this.uppercaseSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.uppercaseSelectionToolStripMenuItem.Text = "Uppercase";
            this.uppercaseSelectionToolStripMenuItem.Click += new System.EventHandler(this.uppercaseSelectionToolStripMenuItem_Click);
            // 
            // lowercaseSelectionToolStripMenuItem
            // 
            this.lowercaseSelectionToolStripMenuItem.Name = "lowercaseSelectionToolStripMenuItem";
            this.lowercaseSelectionToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.lowercaseSelectionToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.lowercaseSelectionToolStripMenuItem.Text = "Lowercase";
            this.lowercaseSelectionToolStripMenuItem.Click += new System.EventHandler(this.lowercaseSelectionToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.findDialogToolStripMenuItem,
            this.findAndReplaceToolStripMenuItem,
            this.toolStripSeparator7,
            this.goToLineToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findToolStripMenuItem.Text = "Quick Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findDialogToolStripMenuItem
            // 
            this.findDialogToolStripMenuItem.Name = "findDialogToolStripMenuItem";
            this.findDialogToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+F";
            this.findDialogToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findDialogToolStripMenuItem.Text = "Find...";
            this.findDialogToolStripMenuItem.Click += new System.EventHandler(this.findDialogToolStripMenuItem_Click);
            // 
            // findAndReplaceToolStripMenuItem
            // 
            this.findAndReplaceToolStripMenuItem.Name = "findAndReplaceToolStripMenuItem";
            this.findAndReplaceToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+H";
            this.findAndReplaceToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.findAndReplaceToolStripMenuItem.Text = "Find and Replace...";
            this.findAndReplaceToolStripMenuItem.Click += new System.EventHandler(this.findAndReplaceToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(238, 6);
            // 
            // goToLineToolStripMenuItem
            // 
            this.goToLineToolStripMenuItem.Name = "goToLineToolStripMenuItem";
            this.goToLineToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+G";
            this.goToLineToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.goToLineToolStripMenuItem.Text = "Go To Line...";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordWrapItem,
            this.indentGuidesItem,
            this.hiddenCharactersItem,
            this.toolStripSeparator4,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.zoom100ToolStripMenuItem,
            this.toolStripSeparator5,
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // wordWrapItem
            // 
            this.wordWrapItem.Name = "wordWrapItem";
            this.wordWrapItem.Size = new System.Drawing.Size(221, 24);
            this.wordWrapItem.Text = "Word Wrap";
            this.wordWrapItem.Click += new System.EventHandler(this.wordWrapToolStripMenuItem1_Click);
            // 
            // indentGuidesItem
            // 
            this.indentGuidesItem.Checked = true;
            this.indentGuidesItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.indentGuidesItem.Name = "indentGuidesItem";
            this.indentGuidesItem.Size = new System.Drawing.Size(221, 24);
            this.indentGuidesItem.Text = "Show Indent Guides";
            this.indentGuidesItem.Click += new System.EventHandler(this.indentGuidesToolStripMenuItem_Click);
            // 
            // hiddenCharactersItem
            // 
            this.hiddenCharactersItem.Name = "hiddenCharactersItem";
            this.hiddenCharactersItem.Size = new System.Drawing.Size(221, 24);
            this.hiddenCharactersItem.Text = "Show Whitespace";
            this.hiddenCharactersItem.Click += new System.EventHandler(this.hiddenCharactersToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(218, 6);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Plus";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Minus";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // zoom100ToolStripMenuItem
            // 
            this.zoom100ToolStripMenuItem.Name = "zoom100ToolStripMenuItem";
            this.zoom100ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+0";
            this.zoom100ToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.zoom100ToolStripMenuItem.Text = "Zoom 100%";
            this.zoom100ToolStripMenuItem.Click += new System.EventHandler(this.zoom100ToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(218, 6);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // PanelSearch
            // 
            this.PanelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSearch.BackColor = System.Drawing.Color.White;
            this.PanelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSearch.Controls.Add(this.BtnNextSearch);
            this.PanelSearch.Controls.Add(this.BtnPrevSearch);
            this.PanelSearch.Controls.Add(this.BtnCloseSearch);
            this.PanelSearch.Controls.Add(this.TxtSearch);
            this.PanelSearch.Location = new System.Drawing.Point(404, 70);
            this.PanelSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Size = new System.Drawing.Size(256, 30);
            this.PanelSearch.TabIndex = 10;
            this.PanelSearch.Visible = false;
            // 
            // BtnNextSearch
            // 
            this.BtnNextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNextSearch.ForeColor = System.Drawing.Color.White;
            this.BtnNextSearch.Location = new System.Drawing.Point(204, 3);
            this.BtnNextSearch.Name = "BtnNextSearch";
            this.BtnNextSearch.Size = new System.Drawing.Size(22, 22);
            this.BtnNextSearch.TabIndex = 9;
            this.BtnNextSearch.Tag = "Find next (Enter)";
            this.BtnNextSearch.UseVisualStyleBackColor = true;
            this.BtnNextSearch.Click += new System.EventHandler(this.BtnNextSearch_Click);
            // 
            // BtnPrevSearch
            // 
            this.BtnPrevSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrevSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevSearch.ForeColor = System.Drawing.Color.White;
            this.BtnPrevSearch.Location = new System.Drawing.Point(179, 3);
            this.BtnPrevSearch.Name = "BtnPrevSearch";
            this.BtnPrevSearch.Size = new System.Drawing.Size(22, 22);
            this.BtnPrevSearch.TabIndex = 8;
            this.BtnPrevSearch.Tag = "Find previous (Shift+Enter)";
            this.BtnPrevSearch.UseVisualStyleBackColor = true;
            this.BtnPrevSearch.Click += new System.EventHandler(this.BtnPrevSearch_Click);
            // 
            // BtnCloseSearch
            // 
            this.BtnCloseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCloseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCloseSearch.ForeColor = System.Drawing.Color.White;
            this.BtnCloseSearch.Location = new System.Drawing.Point(228, 3);
            this.BtnCloseSearch.Name = "BtnCloseSearch";
            this.BtnCloseSearch.Size = new System.Drawing.Size(22, 22);
            this.BtnCloseSearch.TabIndex = 7;
            this.BtnCloseSearch.Tag = "Close (Esc)";
            this.BtnCloseSearch.UseVisualStyleBackColor = true;
            this.BtnCloseSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TxtSearch.Location = new System.Drawing.Point(9, 4);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(165, 25);
            this.TxtSearch.TabIndex = 6;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "js";
            this.openFileDialog.FileName = "New File";
            this.openFileDialog.Filter = "JavaScript files|*.js";
            // 
            // closeEditorToolStripMenuItem
            // 
            this.closeEditorToolStripMenuItem.Name = "closeEditorToolStripMenuItem";
            this.closeEditorToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.closeEditorToolStripMenuItem.Text = "Close Editor";
            this.closeEditorToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // JavaScriptEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(673, 471);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.TextPanel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "JavaScriptEditorForm";
            this.Text = "JavaScript Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closeToolStripMenuItem_Click);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel TextPanel;
		private System.Windows.Forms.Label FileName;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wordWrapItem;
		private System.Windows.Forms.ToolStripMenuItem hiddenCharactersItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoom100ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem indentSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem outdentSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem uppercaseSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lowercaseSelectionToolStripMenuItem;
		private System.Windows.Forms.Panel PanelSearch;
		private System.Windows.Forms.Button BtnNextSearch;
		private System.Windows.Forms.Button BtnPrevSearch;
		private System.Windows.Forms.Button BtnCloseSearch;
		private System.Windows.Forms.TextBox TxtSearch;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem selectLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem goToLineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findAndReplaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem findDialogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem indentGuidesItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeEditorToolStripMenuItem;
    }
}
