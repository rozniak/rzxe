using Oddmatics.Tools.BinPacker.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker.Controls
{
    /// <summary>
    /// Represents a context menu for resource items in the tree.
    /// </summary>
    internal sealed class ResourceItemContextMenuStrip : ContextMenuStrip
    {
        /// <summary>
        /// Gets or sets the current context describing the resource being managed.
        /// </summary>
        public MetaResource Context { get; private set; }

        /// <summary>
        /// Gets or sets the 'Delete' menu item.
        /// </summary>
        public ToolStripMenuItem DeleteMenuItem { get; private set; }

        /// <summary>
        /// Gets or sets the 'Properties' menu item.
        /// </summary>
        public ToolStripMenuItem PropertiesMenuItem { get; private set; }

        /// <summary>
        /// Gets or sets the 'Rename' menu item.
        /// </summary>
        public ToolStripMenuItem RenameMenuItem { get; private set; }


        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ResourceItemContextMenuStrip"/> class.
        /// </summary>
        public ResourceItemContextMenuStrip()
        {
            Initialize();
        }


        /// <summary>
        /// Positions the <see cref="ResourceItemContextMenuStrip"/> relative to the
        /// specified screen location and provides a context on the resource kind.
        /// </summary>
        /// <param name="screenLocation">
        /// The horizontal and vertical location of the screen's upper-left corner, in
        /// pixels.
        /// </param>
        /// <param name="context">
        /// The <see cref="MetaResource"/> being managed.
        /// </param>
        public void Show(Point screenLocation, MetaResource context)
        {
            Context = context;
            this.Show(screenLocation);
        }


        /// <summary>
        /// Initializes the menu.
        /// </summary>
        private void Initialize()
        {
            DeleteMenuItem     =
                new ToolStripMenuItem()
                {
                    Name = "DeleteResourceContextMenuItem",
                    Text = "Delete"
                };
            PropertiesMenuItem =
                new ToolStripMenuItem()
                {
                    Name = "PropertiesResourceContextMenuItem",
                    Text = "Properties"
                };
            RenameMenuItem     =
                new ToolStripMenuItem()
                {
                    Name = "RenameResourceContextMenuItem",
                    Text = "Rename"
                };

            this.Items.Add(PropertiesMenuItem);
            this.Items.Add(new ToolStripSeparator());
            this.Items.Add(DeleteMenuItem);
            this.Items.Add(RenameMenuItem);
        }
    }
}
