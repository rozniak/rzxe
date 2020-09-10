using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker.Dialogs
{
    /// <summary>
    /// Displays a dialog box that prompts the user to specify a name.
    /// </summary>
    public partial class NameEntryDialog : Form
    {
        /// <summary>
        /// Gets or sets the instruction text for the dialog.
        /// </summary>
        public string InstructionText { get; set; }

        /// <summary>
        /// Gets or sets the name entered in the dialog.
        /// </summary>
        public string NameEntered
        {
            get { return InputTextBox.Text; }
            set { InputTextBox.Text = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NameEntryDialog"/> class.
        /// </summary>
        public NameEntryDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// (Event) Occurs when the 'Cancel' button is clicked.
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// (Event) Occurs when the 'OK' button is clicked.
        /// </summary>
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
