using Oddmatics.Tools.BinPacker.Algorithm;
using Oddmatics.Tools.BinPacker.Events;
using Oddmatics.Util.Collections;
using Oddmatics.Util.System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Tools.BinPacker.Data
{
    /// <summary>
    /// Represents the working file in this application.
    /// </summary>
    internal sealed class WorkingFile : ChangeTrackingEx, IDisposable
    {
        /// <summary>
        /// Gets or sets the border box resources in the file.
        /// </summary>
        public ChangeTrackingList<MetaResource> BorderBoxResources { get; private set; }

        /// <summary>
        /// Gets or sets the font resources in the file.
        /// </summary>
        public ChangeTrackingList<MetaResource> FontResources { get; private set; }

        /// <summary>
        /// Gets or sets the full file path last used when saving the file.
        /// </summary>
        public string LastFileName { get; private set; }

        /// <summary>
        /// Gets or sets the source files referenced in the file.
        /// </summary>
        public ChangeTrackingList<string> SourceFiles { get; private set; }


        /// <summary>
        /// The bin packed atlas.
        /// </summary>
        private BitmapBinPacker Atlas;


        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingFile"/> class.
        /// </summary>
        public WorkingFile()
        {
            Atlas              = new BitmapBinPacker();
            BorderBoxResources = new ChangeTrackingList<MetaResource>();
            FontResources      = new ChangeTrackingList<MetaResource>();
            LastFileName       = string.Empty;
            SourceFiles        = new ChangeTrackingList<string>();

            BorderBoxResources.Invalidated += Collection_Invalidated;
            FontResources.Invalidated      += Collection_Invalidated;
            SourceFiles.Invalidated        += Collection_Invalidated;

            BorderBoxResources.ValidationPredicate = ValidateNewResourceItem;
            FontResources.ValidationPredicate      = ValidateNewResourceItem;
            SourceFiles.ValidationPredicate        = ValidateNewSourceFile;
        }


        /// <summary>
        /// Resets the object's state to unchanged by accepting the modifications.
        /// </summary>
        public override void AcceptChanges()
        {
            Save(LastFileName);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="WorkingFile"/>.
        /// </summary>
        public void Dispose()
        {
            Atlas.Dispose();
        }

        /// <summary>
        /// Updates the atlas and retrieves the generated <see cref="Bitmap"/>.
        /// </summary>
        /// <returns>The generated <see cref="Bitmap"/> of the atlas.</returns>
        public Bitmap GrabAtlasBitmap()
        {
            Atlas.SourceFiles = SourceFiles;
            Atlas.Refresh();

            return Atlas.Bitmap;
        }

        /// <summary>
        /// Saves this file to disk.
        /// </summary>
        /// <param name="fullFilePath">The full file path to save as.</param>
        public void Save(string fullFilePath)
        {
            Atlas.Save(fullFilePath);

            LastFileName = fullFilePath;
            IsChanged = false;
        }

        /// <summary>
        /// Sets the size of the atlas.
        /// </summary>
        /// <param name="newSize">The new size of the atlas.</param>
        public void SetAtlasSize(Size newSize)
        {
            Atlas.Size = newSize;
            IsChanged = true;
        }


        /// <summary>
        /// Validates a <see cref="MetaResource"/> being added to one of the resource
        /// bins.
        /// </summary>
        /// <param name="item">
        /// The resource.
        /// </param>
        /// <param name="collection">
        /// The resource bin collection.
        /// </param>
        /// <param name="reason">
        /// If the resource was determined to be invalid, contains the reason for the
        /// failure.
        /// </param>
        /// <returns>
        /// True if the resource is valid.
        /// </returns>
        private bool ValidateNewResourceItem(MetaResource item, IEnumerable<MetaResource> collection, out string reason)
        {
            foreach (MetaResource resource in collection)
            {
                if (item.Name == resource.Name)
                {
                    reason = "A resource with the same name already exists.";
                    return false;
                }
            }

            reason = null;
            return true;
        }

        /// <summary>
        /// Validates the file path of a source file being added to the bin.
        /// </summary>
        /// <param name="item">
        /// The file path to the source file.
        /// </param>
        /// <param name="collection">
        /// The source file collection.
        /// </param>
        /// <param name="reason">
        /// If the file path was determined to be invalid, contains the reason for the
        /// failure.
        /// </param>
        /// <returns>
        /// True if the file path is valid.
        /// </returns>
        private bool ValidateNewSourceFile(string item, IEnumerable<string> collection, out string reason)
        {
            if (collection.Contains(item))
            {
                reason = "The source file has already been added.";
                return false;
            }

            if (!File.Exists(item))
            {
                reason = "The source file does not exist or is not readable.";
                return false;
            }

            reason = null;
            return true;
        }


        /// <summary>
        /// (Event) Occurs when a member collection is invalidated.
        /// </summary>
        private void Collection_Invalidated(object sender, EventArgs e)
        {
            IsChanged = true;
        }
    }
}
