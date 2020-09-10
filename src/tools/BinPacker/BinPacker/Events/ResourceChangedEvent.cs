using Oddmatics.Tools.BinPacker.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oddmatics.Tools.BinPacker.Events
{
    /// <summary>
    /// Represents the method that will handle events referencing
    /// <see cref="MetaResource"/> objects.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="ResourceChangedEventArgs"/> object that contains event data.
    /// </param>
    internal delegate void ResourceChangedEventHandler(
        object sender,
        ResourceChangedEventArgs e
    );


    /// <summary>
    /// Provides data for events referencing <see cref="MetaResource"/> objects.
    /// </summary>
    internal sealed class ResourceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the <see cref="MetaResource"/> that was changed during the
        /// event.
        /// </summary>
        public MetaResource Resource { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceChangedEventArgs"/>
        /// class with a reference to the <see cref="MetaResource" /> instance.
        /// </summary>
        /// <param name="resource">
        /// The resource that was changed.
        /// </param>
        public ResourceChangedEventArgs(MetaResource resource)
        {
            Resource = resource;
        }
    }
}
