namespace Oddmatics.Tools.BinPacker.Dialogs
{
    partial class SpriteMappingDialog
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
            this.UiSplitView = new System.Windows.Forms.SplitContainer();
            this.MappingListView = new System.Windows.Forms.ListView();
            this.PartColumn = new System.Windows.Forms.ColumnHeader();
            this.SpriteColumn = new System.Windows.Forms.ColumnHeader();
            this.PartComboBox = new System.Windows.Forms.ComboBox();
            this.PartTextBox = new System.Windows.Forms.TextBox();
            this.SpriteComboBox = new System.Windows.Forms.ComboBox();
            this.SpriteLabel = new System.Windows.Forms.Label();
            this.PartLabel = new System.Windows.Forms.Label();
            this.NewButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.HorizontalBreak = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UiSplitView)).BeginInit();
            this.UiSplitView.Panel1.SuspendLayout();
            this.UiSplitView.Panel2.SuspendLayout();
            this.UiSplitView.SuspendLayout();
            this.SuspendLayout();
            // 
            // UiSplitView
            // 
            this.UiSplitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UiSplitView.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.UiSplitView.IsSplitterFixed = true;
            this.UiSplitView.Location = new System.Drawing.Point(0, 0);
            this.UiSplitView.Name = "UiSplitView";
            this.UiSplitView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // UiSplitView.Panel1
            // 
            this.UiSplitView.Panel1.Controls.Add(this.MappingListView);
            // 
            // UiSplitView.Panel2
            // 
            this.UiSplitView.Panel2.Controls.Add(this.PartComboBox);
            this.UiSplitView.Panel2.Controls.Add(this.PartTextBox);
            this.UiSplitView.Panel2.Controls.Add(this.SpriteComboBox);
            this.UiSplitView.Panel2.Controls.Add(this.SpriteLabel);
            this.UiSplitView.Panel2.Controls.Add(this.PartLabel);
            this.UiSplitView.Panel2.Controls.Add(this.NewButton);
            this.UiSplitView.Panel2.Controls.Add(this.RemoveButton);
            this.UiSplitView.Panel2.Controls.Add(this.HorizontalBreak);
            this.UiSplitView.Panel2.Controls.Add(this.OKButton);
            this.UiSplitView.Panel2.Controls.Add(this.CancelButton);
            this.UiSplitView.Size = new System.Drawing.Size(307, 371);
            this.UiSplitView.SplitterDistance = 220;
            this.UiSplitView.TabIndex = 0;
            this.UiSplitView.Text = "splitContainer1";
            // 
            // MappingListView
            // 
            this.MappingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PartColumn,
            this.SpriteColumn});
            this.MappingListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MappingListView.HideSelection = false;
            this.MappingListView.Location = new System.Drawing.Point(0, 0);
            this.MappingListView.Name = "MappingListView";
            this.MappingListView.Size = new System.Drawing.Size(307, 220);
            this.MappingListView.TabIndex = 0;
            this.MappingListView.UseCompatibleStateImageBehavior = false;
            this.MappingListView.View = System.Windows.Forms.View.Details;
            this.MappingListView.SelectedIndexChanged += new System.EventHandler(this.MappingListView_SelectedIndexChanged);
            // 
            // PartColumn
            // 
            this.PartColumn.Text = "Part";
            this.PartColumn.Width = 100;
            // 
            // SpriteColumn
            // 
            this.SpriteColumn.Text = "Sprite";
            this.SpriteColumn.Width = 200;
            // 
            // PartComboBox
            // 
            this.PartComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PartComboBox.Enabled = false;
            this.PartComboBox.FormattingEnabled = true;
            this.PartComboBox.Location = new System.Drawing.Point(12, 34);
            this.PartComboBox.Name = "PartComboBox";
            this.PartComboBox.Size = new System.Drawing.Size(128, 21);
            this.PartComboBox.TabIndex = 6;
            // 
            // PartTextBox
            // 
            this.PartTextBox.Enabled = false;
            this.PartTextBox.Location = new System.Drawing.Point(12, 34);
            this.PartTextBox.Name = "PartTextBox";
            this.PartTextBox.Size = new System.Drawing.Size(128, 21);
            this.PartTextBox.TabIndex = 7;
            this.PartTextBox.Visible = false;
            // 
            // SpriteComboBox
            // 
            this.SpriteComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SpriteComboBox.Enabled = false;
            this.SpriteComboBox.FormattingEnabled = true;
            this.SpriteComboBox.Location = new System.Drawing.Point(167, 34);
            this.SpriteComboBox.Name = "SpriteComboBox";
            this.SpriteComboBox.Size = new System.Drawing.Size(128, 21);
            this.SpriteComboBox.TabIndex = 6;
            // 
            // SpriteLabel
            // 
            this.SpriteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SpriteLabel.AutoSize = true;
            this.SpriteLabel.Location = new System.Drawing.Point(167, 18);
            this.SpriteLabel.Name = "SpriteLabel";
            this.SpriteLabel.Size = new System.Drawing.Size(39, 13);
            this.SpriteLabel.TabIndex = 5;
            this.SpriteLabel.Text = "Sprite:";
            // 
            // PartLabel
            // 
            this.PartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PartLabel.AutoSize = true;
            this.PartLabel.Location = new System.Drawing.Point(12, 18);
            this.PartLabel.Name = "PartLabel";
            this.PartLabel.Size = new System.Drawing.Size(31, 13);
            this.PartLabel.TabIndex = 4;
            this.PartLabel.Text = "Part:";
            // 
            // NewButton
            // 
            this.NewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewButton.Location = new System.Drawing.Point(139, 70);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 3;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveButton.Enabled = false;
            this.RemoveButton.Location = new System.Drawing.Point(220, 70);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            // 
            // HorizontalBreak
            // 
            this.HorizontalBreak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HorizontalBreak.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HorizontalBreak.Location = new System.Drawing.Point(12, 102);
            this.HorizontalBreak.Name = "HorizontalBreak";
            this.HorizontalBreak.Size = new System.Drawing.Size(283, 1);
            this.HorizontalBreak.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(139, 112);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(220, 112);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // SpriteMappingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 371);
            this.Controls.Add(this.UiSplitView);
            this.Name = "SpriteMappingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sprite Mapping";
            this.UiSplitView.Panel1.ResumeLayout(false);
            this.UiSplitView.Panel2.ResumeLayout(false);
            this.UiSplitView.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UiSplitView)).EndInit();
            this.UiSplitView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer UiSplitView;
        private System.Windows.Forms.ListView MappingListView;
        private System.Windows.Forms.ColumnHeader PartColumn;
        private System.Windows.Forms.ColumnHeader SpriteColumn;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox SpriteComboBox;
        private System.Windows.Forms.Label SpriteLabel;
        private System.Windows.Forms.Label PartLabel;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Label HorizontalBreak;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ComboBox PartComboBox;
        private System.Windows.Forms.TextBox PartTextBox;
    }
}