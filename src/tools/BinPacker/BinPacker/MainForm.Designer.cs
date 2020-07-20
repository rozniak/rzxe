namespace Oddmatics.Tools.BinPacker
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Fonts");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Border Boxes", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RenderTarget = new System.Windows.Forms.PictureBox();
            this.UiSplitView = new System.Windows.Forms.SplitContainer();
            this.ResourceSplitView = new System.Windows.Forms.SplitContainer();
            this.ResourceBinTreeView = new System.Windows.Forms.TreeView();
            this.ResourcesImageList = new System.Windows.Forms.ImageList(this.components);
            this.TextureBinListBox = new System.Windows.Forms.ListBox();
            this.TextureBinManagementPanel = new System.Windows.Forms.Panel();
            this.TextureBinRefreshButton = new System.Windows.Forms.Button();
            this.TextureBinRemoveButton = new System.Windows.Forms.Button();
            this.TextureBinAddButton = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CanvasMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CanvasAtlasSizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextureBinTitlePanel = new System.Windows.Forms.Panel();
            this.TextureBinTitleLabel = new System.Windows.Forms.Label();
            this.ResourceBinTitlePanel = new System.Windows.Forms.Panel();
            this.ResourceBinTitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RenderTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UiSplitView)).BeginInit();
            this.UiSplitView.Panel1.SuspendLayout();
            this.UiSplitView.Panel2.SuspendLayout();
            this.UiSplitView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceSplitView)).BeginInit();
            this.ResourceSplitView.Panel1.SuspendLayout();
            this.ResourceSplitView.Panel2.SuspendLayout();
            this.ResourceSplitView.SuspendLayout();
            this.TextureBinManagementPanel.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.TextureBinTitlePanel.SuspendLayout();
            this.ResourceBinTitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RenderTarget
            // 
            this.RenderTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RenderTarget.Location = new System.Drawing.Point(0, 0);
            this.RenderTarget.Name = "RenderTarget";
            this.RenderTarget.Size = new System.Drawing.Size(128, 128);
            this.RenderTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.RenderTarget.TabIndex = 0;
            this.RenderTarget.TabStop = false;
            // 
            // UiSplitView
            // 
            this.UiSplitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UiSplitView.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.UiSplitView.IsSplitterFixed = true;
            this.UiSplitView.Location = new System.Drawing.Point(0, 24);
            this.UiSplitView.Name = "UiSplitView";
            // 
            // UiSplitView.Panel1
            // 
            this.UiSplitView.Panel1.AutoScroll = true;
            this.UiSplitView.Panel1.Controls.Add(this.RenderTarget);
            // 
            // UiSplitView.Panel2
            // 
            this.UiSplitView.Panel2.Controls.Add(this.ResourceSplitView);
            this.UiSplitView.Size = new System.Drawing.Size(774, 451);
            this.UiSplitView.SplitterDistance = 514;
            this.UiSplitView.TabIndex = 2;
            // 
            // ResourceSplitView
            // 
            this.ResourceSplitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResourceSplitView.Location = new System.Drawing.Point(0, 0);
            this.ResourceSplitView.Name = "ResourceSplitView";
            this.ResourceSplitView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ResourceSplitView.Panel1
            // 
            this.ResourceSplitView.Panel1.Controls.Add(this.ResourceBinTreeView);
            this.ResourceSplitView.Panel1.Controls.Add(this.ResourceBinTitlePanel);
            // 
            // ResourceSplitView.Panel2
            // 
            this.ResourceSplitView.Panel2.Controls.Add(this.TextureBinListBox);
            this.ResourceSplitView.Panel2.Controls.Add(this.TextureBinTitlePanel);
            this.ResourceSplitView.Panel2.Controls.Add(this.TextureBinManagementPanel);
            this.ResourceSplitView.Size = new System.Drawing.Size(256, 451);
            this.ResourceSplitView.SplitterDistance = 225;
            this.ResourceSplitView.TabIndex = 3;
            // 
            // ResourceBinTreeView
            // 
            this.ResourceBinTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResourceBinTreeView.ImageIndex = 0;
            this.ResourceBinTreeView.ImageList = this.ResourcesImageList;
            this.ResourceBinTreeView.Location = new System.Drawing.Point(0, 24);
            this.ResourceBinTreeView.Name = "ResourceBinTreeView";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "FontRootNode";
            treeNode1.Text = "Fonts";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "BorderBoxRoot";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "Border Boxes";
            this.ResourceBinTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.ResourceBinTreeView.SelectedImageIndex = 0;
            this.ResourceBinTreeView.Size = new System.Drawing.Size(256, 201);
            this.ResourceBinTreeView.TabIndex = 2;
            // 
            // ResourcesImageList
            // 
            this.ResourcesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ResourcesImageList.ImageStream")));
            this.ResourcesImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ResourcesImageList.Images.SetKeyName(0, "Font_16x16.png");
            this.ResourcesImageList.Images.SetKeyName(1, "BorderBox_16x16.png");
            // 
            // TextureBinListBox
            // 
            this.TextureBinListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextureBinListBox.FormattingEnabled = true;
            this.TextureBinListBox.Location = new System.Drawing.Point(0, 24);
            this.TextureBinListBox.Name = "TextureBinListBox";
            this.TextureBinListBox.Size = new System.Drawing.Size(256, 161);
            this.TextureBinListBox.TabIndex = 1;
            this.TextureBinListBox.SelectedIndexChanged += new System.EventHandler(this.NodeListBox_SelectedIndexChanged);
            // 
            // TextureBinManagementPanel
            // 
            this.TextureBinManagementPanel.Controls.Add(this.TextureBinRefreshButton);
            this.TextureBinManagementPanel.Controls.Add(this.TextureBinRemoveButton);
            this.TextureBinManagementPanel.Controls.Add(this.TextureBinAddButton);
            this.TextureBinManagementPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextureBinManagementPanel.Location = new System.Drawing.Point(0, 185);
            this.TextureBinManagementPanel.Name = "TextureBinManagementPanel";
            this.TextureBinManagementPanel.Size = new System.Drawing.Size(256, 37);
            this.TextureBinManagementPanel.TabIndex = 0;
            // 
            // TextureBinRefreshButton
            // 
            this.TextureBinRefreshButton.Location = new System.Drawing.Point(7, 6);
            this.TextureBinRefreshButton.Name = "TextureBinRefreshButton";
            this.TextureBinRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.TextureBinRefreshButton.TabIndex = 2;
            this.TextureBinRefreshButton.Text = "Refresh";
            this.TextureBinRefreshButton.UseVisualStyleBackColor = true;
            this.TextureBinRefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // TextureBinRemoveButton
            // 
            this.TextureBinRemoveButton.Enabled = false;
            this.TextureBinRemoveButton.Location = new System.Drawing.Point(88, 6);
            this.TextureBinRemoveButton.Name = "TextureBinRemoveButton";
            this.TextureBinRemoveButton.Size = new System.Drawing.Size(75, 23);
            this.TextureBinRemoveButton.TabIndex = 1;
            this.TextureBinRemoveButton.Text = "Remove";
            this.TextureBinRemoveButton.UseVisualStyleBackColor = true;
            this.TextureBinRemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // TextureBinAddButton
            // 
            this.TextureBinAddButton.Location = new System.Drawing.Point(169, 6);
            this.TextureBinAddButton.Name = "TextureBinAddButton";
            this.TextureBinAddButton.Size = new System.Drawing.Size(75, 23);
            this.TextureBinAddButton.TabIndex = 0;
            this.TextureBinAddButton.Text = "Add...";
            this.TextureBinAddButton.UseVisualStyleBackColor = true;
            this.TextureBinAddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.CanvasMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(774, 24);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewMenuItem,
            this.FileOpenMenuItem,
            this.FileSaveMenuItem,
            this.FileSaveAsMenuItem,
            this.FileMenuSeparator1,
            this.FileExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(35, 20);
            this.FileMenu.Text = "&File";
            // 
            // FileNewMenuItem
            // 
            this.FileNewMenuItem.Name = "FileNewMenuItem";
            this.FileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNewMenuItem.Size = new System.Drawing.Size(148, 22);
            this.FileNewMenuItem.Text = "&New";
            this.FileNewMenuItem.Click += new System.EventHandler(this.FileNewMenuItem_Click);
            // 
            // FileOpenMenuItem
            // 
            this.FileOpenMenuItem.Enabled = false;
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpenMenuItem.Size = new System.Drawing.Size(148, 22);
            this.FileOpenMenuItem.Text = "&Open..";
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSaveMenuItem.Size = new System.Drawing.Size(148, 22);
            this.FileSaveMenuItem.Text = "&Save";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
            // 
            // FileSaveAsMenuItem
            // 
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Size = new System.Drawing.Size(148, 22);
            this.FileSaveAsMenuItem.Text = "Save &As...";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // FileMenuSeparator1
            // 
            this.FileMenuSeparator1.Name = "FileMenuSeparator1";
            this.FileMenuSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.Size = new System.Drawing.Size(148, 22);
            this.FileExitMenuItem.Text = "E&xit";
            this.FileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
            // 
            // CanvasMenu
            // 
            this.CanvasMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CanvasAtlasSizeMenuItem});
            this.CanvasMenu.Name = "CanvasMenu";
            this.CanvasMenu.Size = new System.Drawing.Size(55, 20);
            this.CanvasMenu.Text = "&Canvas";
            // 
            // CanvasAtlasSizeMenuItem
            // 
            this.CanvasAtlasSizeMenuItem.Name = "CanvasAtlasSizeMenuItem";
            this.CanvasAtlasSizeMenuItem.Size = new System.Drawing.Size(132, 22);
            this.CanvasAtlasSizeMenuItem.Text = "Atlas Si&ze...";
            this.CanvasAtlasSizeMenuItem.Click += new System.EventHandler(this.CanvasAtlasSizeMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAboutMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(40, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // HelpAboutMenuItem
            // 
            this.HelpAboutMenuItem.Name = "HelpAboutMenuItem";
            this.HelpAboutMenuItem.Size = new System.Drawing.Size(190, 22);
            this.HelpAboutMenuItem.Text = "&About Bin Packer Tool...";
            this.HelpAboutMenuItem.Click += new System.EventHandler(this.HelpAboutMenuItem_Click);
            // 
            // TextureBinTitlePanel
            // 
            this.TextureBinTitlePanel.Controls.Add(this.TextureBinTitleLabel);
            this.TextureBinTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TextureBinTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TextureBinTitlePanel.Name = "TextureBinTitlePanel";
            this.TextureBinTitlePanel.Size = new System.Drawing.Size(256, 24);
            this.TextureBinTitlePanel.TabIndex = 2;
            // 
            // TextureBinTitleLabel
            // 
            this.TextureBinTitleLabel.AutoSize = true;
            this.TextureBinTitleLabel.Location = new System.Drawing.Point(4, 5);
            this.TextureBinTitleLabel.Name = "TextureBinTitleLabel";
            this.TextureBinTitleLabel.Size = new System.Drawing.Size(61, 13);
            this.TextureBinTitleLabel.TabIndex = 0;
            this.TextureBinTitleLabel.Text = "Texture Bin";
            // 
            // ResourceBinTitlePanel
            // 
            this.ResourceBinTitlePanel.Controls.Add(this.ResourceBinTitleLabel);
            this.ResourceBinTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ResourceBinTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.ResourceBinTitlePanel.Name = "ResourceBinTitlePanel";
            this.ResourceBinTitlePanel.Size = new System.Drawing.Size(256, 24);
            this.ResourceBinTitlePanel.TabIndex = 3;
            // 
            // ResourceBinTitleLabel
            // 
            this.ResourceBinTitleLabel.AutoSize = true;
            this.ResourceBinTitleLabel.Location = new System.Drawing.Point(4, 5);
            this.ResourceBinTitleLabel.Name = "ResourceBinTitleLabel";
            this.ResourceBinTitleLabel.Size = new System.Drawing.Size(58, 13);
            this.ResourceBinTitleLabel.TabIndex = 1;
            this.ResourceBinTitleLabel.Text = "Resources";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 475);
            this.Controls.Add(this.UiSplitView);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(780, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bin Packer Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.RenderTarget)).EndInit();
            this.UiSplitView.Panel1.ResumeLayout(false);
            this.UiSplitView.Panel1.PerformLayout();
            this.UiSplitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UiSplitView)).EndInit();
            this.UiSplitView.ResumeLayout(false);
            this.ResourceSplitView.Panel1.ResumeLayout(false);
            this.ResourceSplitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResourceSplitView)).EndInit();
            this.ResourceSplitView.ResumeLayout(false);
            this.TextureBinManagementPanel.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.TextureBinTitlePanel.ResumeLayout(false);
            this.TextureBinTitlePanel.PerformLayout();
            this.ResourceBinTitlePanel.ResumeLayout(false);
            this.ResourceBinTitlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderTarget;
        private System.Windows.Forms.SplitContainer UiSplitView;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator FileMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpAboutMenuItem;
        private System.Windows.Forms.ListBox TextureBinListBox;
        private System.Windows.Forms.Panel TextureBinManagementPanel;
        private System.Windows.Forms.Button TextureBinRemoveButton;
        private System.Windows.Forms.Button TextureBinAddButton;
        private System.Windows.Forms.Button TextureBinRefreshButton;
        private System.Windows.Forms.ToolStripMenuItem CanvasMenu;
        private System.Windows.Forms.ToolStripMenuItem CanvasAtlasSizeMenuItem;
        private System.Windows.Forms.TreeView ResourceBinTreeView;
        private System.Windows.Forms.ImageList ResourcesImageList;
        private System.Windows.Forms.SplitContainer ResourceSplitView;
        private System.Windows.Forms.Panel TextureBinTitlePanel;
        private System.Windows.Forms.Label TextureBinTitleLabel;
        private System.Windows.Forms.Panel ResourceBinTitlePanel;
        private System.Windows.Forms.Label ResourceBinTitleLabel;
    }
}

