using Newtonsoft.Json;
using Oddmatics.Tools.BinPacker.Algorithm;
using Oddmatics.Tools.BinPacker.Controls;
using Oddmatics.Tools.BinPacker.Data;
using Oddmatics.Tools.BinPacker.Dialogs;
using Oddmatics.Util.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker
{
    /// <summary>
    /// Represents the main Bin Packer Tool window.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The placeholder-oriented format for the window title.
        /// </summary>
        private const string WindowTitleFormat = "{0} - {1} {2}";


        /// <summary>
        /// The root tree node for border box resources.
        /// </summary>
        private ResourceTreeNode BorderBoxRootNode { get; set; }

        /// <summary>
        /// The working file.
        /// </summary>
        private WorkingFile File { get; set; }

        /// <summary>
        /// The root tree node for font resources.
        /// </summary>
        private ResourceTreeNode FontRootNode { get; set; }

        /// <summary>
        /// The context menu for the root node of the resource tree.
        /// </summary>
        private ResourceContextMenuStrip ResourceContextMenuStrip { get; set; }

        /// <summary>
        /// The context menu for item nodes of the resource tree.
        /// </summary>
        private ResourceItemContextMenuStrip ResourceItemContextMenuStrip { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeResourceContextMenus();
        }


        /// <summary>
        /// Creates a new atlas.
        /// </summary>
        public void CreateNew()
        {
            DialogResult shouldSaveFirst = CheckDiscard();

            switch (shouldSaveFirst)
            {
                case DialogResult.Yes:
                    bool saveOccurred = Save(File.LastFileName);

                    if (!saveOccurred)
                        return;

                    break;

                case DialogResult.Cancel:
                    return;
            }

            // User should select a new atlas size
            //
            var dialog = new ChangeAtlasSizeDialog();
            Size newAtlasSize = Size.Empty;

            if (dialog.ShowDialog() == DialogResult.OK)
                newAtlasSize = dialog.ChosenSize;
            else if (File == null)
                newAtlasSize = new Size(2048, 2048); // FIXME: Replace with constants
            else
                return;

            // Store a reference to the old file
            //
            WorkingFile oldFile = File;

            // Create a new file in its place
            //
            File = new WorkingFile();

            File.SetAtlasSize(newAtlasSize);

            File.ChangesAccepted                += File_ChangesAccepted;
            File.Invalidated                    += File_Invalidated;

            File.BorderBoxResources.Cleared     += File_BorderBoxesCleared;
            File.BorderBoxResources.ItemAdded   += File_BorderBoxAdded;
            File.BorderBoxResources.ItemRemoved += File_BorderBoxRemoved;

            File.FontResources.Cleared          += File_FontsCleared;
            File.FontResources.ItemAdded        += File_FontAdded;
            File.FontResources.ItemRemoved      += File_FontRemoved;

            // Update the form
            //
            TextureBinListBox.Items.Clear();
            RefreshRenderTarget();
            UpdateTitle();

            // Dispose the old file if needed
            //
            if (oldFile != null)
            {
                DisposeWorkingFile(ref oldFile);
            }

            // Initialize UI
            //
            InitializeResourceTreeView();
        }

        /// <summary>
        /// Saves the working file.
        /// </summary>
        /// <param name="fullFilePath">
        /// The full file path to save to, if not specified, the user will be prompted
        /// for a save location.
        /// </param>
        public bool Save(string fullFilePath = "")
        {
            string targetPath;

            if (String.IsNullOrWhiteSpace(fullFilePath))
            {
                var saveDialog = new SaveFileDialog();

                saveDialog.Filter = "Atlas and UV Files (*.json;*.png)|*.json;*.png";
                saveDialog.Title = "Save Atlas As";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                    targetPath = saveDialog.FileName;
                else
                    return false;
            }
            else
                targetPath = fullFilePath;

            try
            {
                File.Save(targetPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred whilst saving the atlas.{ex.Message}\n\n" +
                    $"{ex.StackTrace}",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }

            return true;
        }


        /// <summary>
        /// Prompt the user if they would like to discard unsaved changes, if the file
        /// contains any.
        /// </summary>
        /// <returns>
        /// <see cref="DialogResult.Yes"/> if the user would like to save,
        /// <see cref="DialogResult.No"/> if the discard operation should continue,
        /// <see cref="DialogResult.Cancel"/> if the user would like to go back to the 
        /// program.
        /// </returns>
        private DialogResult CheckDiscard()
        {
            // No need to prompt if the file has been saved or no file exists
            //
            if (File == null || !File.IsChanged)
            {
                return DialogResult.No;
            }

            return MessageBox.Show(
                "Do you want to save changes to the atlas?",
                Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );
        }

        /// <summary>
        /// Disposes a <see cref="WorkingFile"/> completely.
        /// </summary>
        /// <param name="file">
        /// A reference to the <see cref="WorkingFile"/> to dispose.
        /// </param>
        private void DisposeWorkingFile(ref WorkingFile file)
        {
            file.ChangesAccepted                -= File_ChangesAccepted;
            file.Invalidated                    -= File_Invalidated;

            file.BorderBoxResources.Cleared     -= File_BorderBoxesCleared;
            file.BorderBoxResources.ItemAdded   -= File_BorderBoxAdded;
            file.BorderBoxResources.ItemRemoved -= File_BorderBoxRemoved;

            file.FontResources.Cleared          -= File_FontsCleared;
            file.FontResources.ItemAdded        -= File_FontAdded;
            file.FontResources.ItemRemoved      -= File_FontRemoved;

            file.Dispose();
        }

        /// <summary>
        /// Initializes the context menus for the resource tree.
        /// </summary>
        private void InitializeResourceContextMenus()
        {
            ResourceContextMenuStrip     = new ResourceContextMenuStrip();
            ResourceItemContextMenuStrip = new ResourceItemContextMenuStrip();

            ResourceContextMenuStrip.AddMenuItem.Click            +=
                AddResourceContextMenuItem_Click;
            ResourceItemContextMenuStrip.DeleteMenuItem.Click     +=
                DeleteResourceContextMenuItem_Click;
            ResourceItemContextMenuStrip.PropertiesMenuItem.Click +=
                PropertiesResourceContextMenuItem_Click;
            ResourceItemContextMenuStrip.RenameMenuItem.Click     +=
                RenameResourceContextMenuItem_Click;
        }

        /// <summary>
        /// Initializes the tree view.
        /// </summary>
        private void InitializeResourceTreeView()
        {
            BorderBoxRootNode =
                new ResourceTreeNode(BinPackerResourceKind.BorderBox)
                {
                    ImageIndex         = 0,
                    SelectedImageIndex = 0
                };
            FontRootNode      =
                new ResourceTreeNode(BinPackerResourceKind.Font)
                {
                    ImageIndex         = 1,
                    SelectedImageIndex = 1
                };

            ResourceBinTreeView.Nodes.Clear();

            ResourceBinTreeView.Nodes.Add(BorderBoxRootNode);
            ResourceBinTreeView.Nodes.Add(FontRootNode);
        }

        /// <summary>
        /// Opens a <see cref="SpriteMappingDialog"/> for editing the specified
        /// resource.
        /// </summary>
        /// <param name="resource">
        /// The resource.
        /// </param>
        private void OpenResourceSpriteMapping(MetaResource resource)
        {
            using (var dialog = new SpriteMappingDialog(resource))
            {
                dialog.SourceFiles = File.SourceFiles;


            }
        }

        /// <summary>
        /// Refreshes the <see cref="PictureBox"/> render target displayed in this
        /// form.
        /// </summary>
        private void RefreshRenderTarget()
        {
            try
            {
                RenderTarget.Image = File.GrabAtlasBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to generate the atlas.{ex.Message}\n\n" +
                    $"{ex.StackTrace}",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates the window title.
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = string.Format(
                WindowTitleFormat,
                Application.ProductName,
                (string.IsNullOrEmpty(File.LastFileName) ? "Untitled" : File.LastFileName),
                (File.IsChanged ? "*" : string.Empty)
            );
        }


        #region Context Menu Events

        /// <summary>
        /// (Event) Occurs when the 'Add' button on the resource context menu is
        /// clicked.
        /// </summary>
        private void AddResourceContextMenuItem_Click(object sender, EventArgs e)
        {
            var uiItem = (ToolStripMenuItem) sender;
            var uiMenu = (ResourceContextMenuStrip) uiItem.Owner;

            // Handle name entry dialog
            //
            MetaResource resource = null;

            using (var dialog = new NameEntryDialog())
            {
                bool exitLoop = false;

                while (!exitLoop)
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var exceptionDr = DialogResult.Retry;

                        try
                        {
                            resource = new MetaResource(uiMenu.Context, dialog.NameEntered);

                            switch (uiMenu.Context)
                            {
                                case BinPackerResourceKind.BorderBox:
                                    File.BorderBoxResources.Add(resource);
                                    break;

                                case BinPackerResourceKind.Font:
                                    File.FontResources.Add(resource);
                                    break;
                            }

                            exitLoop = true;
                        }
                        catch (ArgumentException argEx)
                        {
                            exceptionDr =
                                MessageBox.Show(
                                    $"Failed to create resource: {argEx.Message}",
                                    Application.ProductName,
                                    MessageBoxButtons.RetryCancel,
                                    MessageBoxIcon.Warning
                                );
                        }
                        catch (ValidationFailureException validationEx)
                        {
                            exceptionDr =
                                MessageBox.Show(
                                    $"The resource name was invalid: {validationEx.Reason}",
                                    Application.ProductName,
                                    MessageBoxButtons.RetryCancel,
                                    MessageBoxIcon.Warning
                                );
                        }

                        if (exceptionDr == DialogResult.Cancel)
                        {
                            exitLoop = true;
                        }
                    }
                    else
                    {
                        exitLoop = true;
                    }
                }
            }

            // Made a resource? Edit it now
            //
            if (resource != null)
            {

            }
        }

        /// <summary>
        /// (Event) Occurs when the 'Delete' button on the resource item context menu
        /// is clicked.
        /// </summary>
        private void DeleteResourceContextMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (Event) Occurs when the 'Properties' button on the resource item context
        /// menu is clicked.
        /// </summary>
        private void PropertiesResourceContextMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (Event) Occurs when the 'Rename' button on the resource item context menu
        /// is clicked.
        /// </summary>
        private void RenameResourceContextMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region WinForms Events

        /// <summary>
        /// (Event) Occurs when the "Add..." button is clicked.
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();

            openDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png";
            openDialog.Title = "Add Image";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                File.SourceFiles.Add(openDialog.FileName);
                TextureBinListBox.Items.Add(
                    Path.GetFileNameWithoutExtension(openDialog.FileName)
                );
            }
        }

        /// <summary>
        /// (Event) Occurs when the "Canvas > Atlas Size..." menu item is clicked.
        /// </summary>
        private void CanvasAtlasSizeMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new ChangeAtlasSizeDialog();

            dialog.ChosenSize = RenderTarget.Image.Size;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.SetAtlasSize(dialog.ChosenSize);
                RefreshRenderTarget();
            }
        }

        /// <summary>
        /// (Event) Occurs when the "Help > About Bin Packer Tool..." menu item is clicked.
        /// </summary>
        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        {
            var aboutDialog = new AboutDialog();

            aboutDialog.ShowDialog();
        }

        /// <summary>
        /// (Event) Occurs when the "File > Exit" menu item is clicked.
        /// </summary>
        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// (Event) Occurs when the "File > New..." menu item is clicked.
        /// </summary>
        private void FileNewMenuItem_Click(object sender, EventArgs e)
        {
            CreateNew();
        }

        /// <summary>
        /// (Event) Occurs when the "File > Save" menu item is clicked.
        /// </summary>
        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            Save(File.LastFileName);
        }

        /// <summary>
        /// (Event) Occurs when the "File > Save As" menu item is clicked.
        /// </summary>
        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// (Event) Occurs when this form is closing.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult shouldSaveFirst = CheckDiscard();

            switch (shouldSaveFirst)
            {
                case DialogResult.Yes:
                    bool saveOccurred = Save(File.LastFileName);

                    if (!saveOccurred)
                        e.Cancel = true;

                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// (Event) Occurs when this form has been displayed.
        /// </summary>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            CreateNew();
            UpdateTitle();
        }

        /// <summary>
        /// (Event) Occurs when the selected index in the node <see cref="ListBox"/> is
        /// changed.
        /// </summary>
        private void NodeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextureBinRemoveButton.Enabled = TextureBinListBox.SelectedItem != null;
        }

        /// <summary>
        /// (Event) Occurs when the "Refresh" button is clicked.
        /// </summary>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshRenderTarget();
        }

        /// <summary>
        /// (Event) Occurs when the "Remove" button is clicked.
        /// </summary>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            File.SourceFiles.RemoveAt(TextureBinListBox.SelectedIndex);
            TextureBinListBox.Items.RemoveAt(TextureBinListBox.SelectedIndex);
        }

        /// <summary>
        /// (Event) Occurs when the resource bin is clicked.
        /// </summary>
        private void ResourceBinTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            var treeView = (TreeView) sender;

            // Handle hit test and selection
            //
            TreeViewHitTestInfo hitTest  = treeView.HitTest(e.X, e.Y);
            var                 treeNode = (ResourceTreeNode) hitTest.Node;

            if (treeNode == null)
            {
                return;
            }

            if (treeView.SelectedNode != treeNode)
            {
                treeView.SelectedNode = treeNode;
            }

            // Handle context menu
            //
            if (e.Button == MouseButtons.Right)
            {
                // Is this a root node or an item?
                //
                if (treeNode.Resource == null)
                {
                    ResourceContextMenuStrip.Show(
                        Cursor.Position,
                        treeNode.ResourceKind
                    );
                }
                else
                {
                    ResourceItemContextMenuStrip.Show(
                        Cursor.Position,
                        treeNode.Resource
                    );
                }
            }
        }

        #endregion


        #region WorkingFile Events

        /// <summary>
        /// (Event) Occurs when a border box resource is added to the working file.
        /// </summary>
        private void File_BorderBoxAdded(object sender, ItemChangedEventArgs<MetaResource> e)
        {
            BorderBoxRootNode.Nodes.Insert(
                e.ItemIndex,
                new ResourceTreeNode(e.ItemChanged)
                {
                    ImageIndex         = 2,
                    SelectedImageIndex = 2
                }
            );
        }

        /// <summary>
        /// (Event) Occurs when all border box resources are cleared from the working
        /// file.
        /// </summary>
        private void File_BorderBoxesCleared(object sender, EventArgs e)
        {
            BorderBoxRootNode.Nodes.Clear();
        }

        /// <summary>
        /// (Event) Occurs when a border box resource is removed from the working file.
        /// </summary>
        private void File_BorderBoxRemoved(object sender, ItemChangedEventArgs<MetaResource> e)
        {
            BorderBoxRootNode.Nodes.RemoveAt(e.ItemIndex);
        }

        /// <summary>
        /// (Event) Occurs when the current <see cref="WorkingFile"/> instance is
        /// saved.
        /// </summary>
        private void File_ChangesAccepted(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        /// <summary>
        /// (Event) Occurs when a font resource is added to the working file.
        /// </summary>
        private void File_FontAdded(object sender, ItemChangedEventArgs<MetaResource> e)
        {
            FontRootNode.Nodes.Insert(
                e.ItemIndex,
                new ResourceTreeNode(e.ItemChanged)
                {
                    ImageIndex         = 2,
                    SelectedImageIndex = 2
                }
            );
        }

        /// <summary>
        /// (Event) Occurs when a font resource is removed from the working file.
        /// </summary>
        private void File_FontRemoved(object sender, ItemChangedEventArgs<MetaResource> e)
        {
            FontRootNode.Nodes.RemoveAt(e.ItemIndex);
        }

        /// <summary>
        /// (Event) Occurs when all font resources are cleared from the working file.
        /// </summary>
        private void File_FontsCleared(object sender, EventArgs e)
        {
            FontRootNode.Nodes.Clear();
        }

        /// <summary>
        /// (Event) Occurs when the current <see cref="WorkingFile"/> has its
        /// previously saved copy invalidated.
        /// </summary>
        private void File_Invalidated(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        #endregion
    }
}
