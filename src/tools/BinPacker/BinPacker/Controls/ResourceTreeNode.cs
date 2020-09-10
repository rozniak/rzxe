using Oddmatics.Tools.BinPacker.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Oddmatics.Tools.BinPacker.Controls
{
    /// <summary>
    /// Represents a node for the resource <see cref="TreeView"/>.
    /// </summary>
    internal sealed class ResourceTreeNode : TreeNode
    {
        /// <summary>
        /// Gets or sets the <see cref="MetaResource"/> associated with the node.
        /// </summary>
        public MetaResource Resource { get; private set; }

        /// <summary>
        /// Gets or sets the resource kind referenced by the node.
        /// </summary>
        public BinPackerResourceKind ResourceKind { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTreeNode"/> class
        /// using a reference to a <see cref="MetaResource"/>.
        /// </summary>
        /// <param name="resource">
        /// The referenced <see cref="MetaResource"/> object.
        /// </param>
        public ResourceTreeNode(
            MetaResource resource
        )
        {
            Resource     = resource;
            ResourceKind = resource.ResourceKind;

            this.Text = resource.Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTreeNode"/> class
        /// as a root of of the specified <see cref="BinPackerResourceKind"/>.
        /// </summary>
        /// <param name="resourceKind">
        /// The kind of resource to associate the node with.
        /// </param>
        public ResourceTreeNode(
            BinPackerResourceKind resourceKind
        )
        {
            ResourceKind = resourceKind;

            switch (ResourceKind)
            {
                case BinPackerResourceKind.BorderBox:
                    this.Text = "Border Boxes";
                    break;

                case BinPackerResourceKind.Font:
                    this.Text = "Fonts";
                    break;
            }
        }
    }
}
