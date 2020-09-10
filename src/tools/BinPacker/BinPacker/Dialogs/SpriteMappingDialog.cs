using Oddmatics.Tools.BinPacker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker.Dialogs
{
    /// <summary>
    /// Displays a dialog box that prompts the user to map sprites for a resource.
    /// </summary>
    internal partial class SpriteMappingDialog : Form
    {
        /// <summary>
        /// Gets or sets the mappings selected in the dialog.
        /// </summary>
        public Dictionary<string, string> Mappings
        {
            get { return new Dictionary<string, string>(_Mappings); }
        }
        private List<KeyValuePair<string, string>> _Mappings;

        /// <summary>
        /// Gets or sets the resource being edited.
        /// </summary>
        public MetaResource Resource { get; private set; }

        /// <summary>
        /// Gets or sets the available source files.
        /// </summary>
        public IList<string> SourceFiles
        {
            get { return _SourceFiles; }
            set
            {
                MappingListView.SelectedIndices.Clear();

                _SourceFiles = value;
            }
        }
        private IList<string> _SourceFiles;


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteMappingDialog"/> class.
        /// </summary>
        private SpriteMappingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteMappingDialog"/> class
        /// with an associated resource.
        /// </summary>
        /// <param name="resource">
        /// The resource.
        /// </param>
        public SpriteMappingDialog(MetaResource resource)
        {
            InitializeComponent();

            Resource  = resource;

            InitializePartInput();
            PopulateMappings();
            PopulateListView();
        }


        /// <summary>
        /// Initializes the input control(s) for selecting the part to map.
        /// </summary>
        private void InitializePartInput()
        {
            string[] staticParts =
                MetaResource.GetStaticParts(
                    Resource.ResourceKind
                );

            // No parts?
            //
            if (staticParts == null)
            {
                PartComboBox.Visible = false;
                PartTextBox.Visible  = true;
            }

            // Populate parts now
            //
            PartComboBox.Items.AddRange(staticParts);

            PartComboBox.Visible = true;
            PartTextBox.Visible  = false;
        }

        /// <summary>
        /// Populates the mapping list view.
        /// </summary>
        private void PopulateListView()
        {
            MappingListView.Items.Clear();

            foreach (var pair in _Mappings)
            {
                MappingListView.Items.Add(
                    new ListViewItem(
                        new string[]
                        {
                            pair.Key,
                            Path.GetFileNameWithoutExtension(pair.Value)
                        }
                    )
                );
            }
        }

        /// <summary>
        /// Populates the internal mappings dictionary.
        /// </summary>
        private void PopulateMappings()
        {
            _Mappings = new List<KeyValuePair<string, string>>();

            foreach (KeyValuePair<string, string> pair in Resource.SpriteMappings)
            {
                _Mappings.Add(pair);
            }
        }

        /// <summary>
        /// Updates the preview pane.
        /// </summary>
        private void UpdatePreviewPane()
        {
            if (MappingListView.SelectedIndices.Count == 0)
            {
                PartComboBox.Enabled        = false;
                PartComboBox.SelectedItem   = -1;
                PartTextBox.Enabled         = false;
                PartTextBox.Text            = "";
                RemoveButton.Enabled        = false;
                SpriteComboBox.Enabled      = false;
                SpriteComboBox.SelectedItem = -1;
            }
            else
            {
                KeyValuePair<string, string> mapping =
                    _Mappings[MappingListView.SelectedIndices[0]];

                // If the part combo box is active, then search for the part and select
                // it
                //
                if (PartComboBox.Visible)
                {
                    PartComboBox.SelectedIndex =
                        PartComboBox.Items.IndexOf(mapping.Key);
                }
                else if (PartTextBox.Visible)
                {
                    PartTextBox.Text = mapping.Key;
                }

                PartComboBox.Enabled = PartComboBox.Visible;
                PartTextBox.Enabled  = PartTextBox.Visible;
                RemoveButton.Enabled = true;
            }
        }


        /// <summary>
        /// (Event) Occurs when the selected index of the mapping list view has
        /// changed.
        /// </summary>
        private void MappingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewPane();
        }

        /// <summary>
        /// (Event) Occurs when the 'New' button is clicked.
        /// </summary>
        private void NewButton_Click(object sender, EventArgs e)
        {
            MappingListView.Items.Add(
                new ListViewItem(
                    new string[] { "", "" }
                )
            );

            _Mappings.Add(new KeyValuePair<string, string>("", ""));

            MappingListView.SelectedIndices.Clear();
            MappingListView.SelectedIndices.Add(MappingListView.Items.Count - 1);
        }
    }
}
