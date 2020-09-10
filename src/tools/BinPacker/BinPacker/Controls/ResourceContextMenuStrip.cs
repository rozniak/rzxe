using Oddmatics.Tools.BinPacker.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker.Controls
{
    /// <summary>
    /// Represents a context menu for a resource tree root node.
    /// </summary>
    internal sealed class ResourceContextMenuStrip : ContextMenuStrip
    {
        /// <summary>
        /// Gets or sets the 'Add...' menu item.
        /// </summary>
        public ToolStripMenuItem AddMenuItem { get; private set; }

        /// <summary>
        /// Gets or sets the current context describing the resource kind being
        /// managed.
        /// </summary>
        public BinPackerResourceKind Context { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContextMenuStrip"/>
        /// class.
        /// </summary>
        public ResourceContextMenuStrip()
        {
            Initialize();
        }


        /// <summary>
        /// Positions the <see cref="ResourceContextMenuStrip"/> relative to the
        /// specified screen location and provides a context on the resource kind.
        /// </summary>
        /// <param name="screenLocation">
        /// The horizontal and vertical location of the screen's upper-left corner, in
        /// pixels.
        /// </param>
        /// <param name="context">
        /// The <see cref="BinPackerResourceKind"/> of the resource being managed.
        /// </param>
        public void Show(Point screenLocation, BinPackerResourceKind context)
        {
            Context = context;
            this.Show(screenLocation);
        }


        /// <summary>
        /// Initializes the menu.
        /// </summary>
        private void Initialize()
        {
            AddMenuItem =
                new ToolStripMenuItem()
                {
                    Name = "AddResourceContextMenuItem",
                    Text = "Add..."
                };

            this.Items.Add(AddMenuItem);
        }
    }
}
