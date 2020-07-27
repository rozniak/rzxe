using Oddmatics.Tools.BinPacker.Algorithm;
using Oddmatics.Util.Collections;
using Oddmatics.Util.System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
        /// Gets the full file path last used when saving this file.
        /// </summary>
        public string LastFileName { get; private set; }


        /// <summary>
        /// The bin packed atlas.
        /// </summary>
        private BitmapBinPacker Atlas;

        /// <summary>
        /// The source files referenced in the file.
        /// </summary>
        private List<string> SourceFiles;


        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingFile"/> class.
        /// </summary>
        public WorkingFile()
        {
            Atlas = new BitmapBinPacker();
            LastFileName = String.Empty;
            SourceFiles = new List<string>();
        }


        /// <summary>
        /// Resets the object's state to unchanged by accepting the modifications.
        /// </summary>
        public override void AcceptChanges()
        {
            Save(LastFileName);
        }

        /// <summary>
        /// Adds a file to the working source files.
        /// </summary>
        /// <param name="fullFilePath">The full path to the source file.</param>
        public void AddFile(string fullFilePath)
        {
            SourceFiles.Add(fullFilePath);
            IsChanged = true;
        }

        /// <summary>
        /// Releases all resources used by this <see cref="WorkingFile"/>.
        /// </summary>
        public void Dispose()
        {
            Atlas.Dispose();
        }

        /// <summary>
        /// Retrieves a read-only copy of the collection of source files.
        /// </summary>
        /// <returns>
        /// A <see cref="ReadOnlyCollection{string}"/> that contains all of the source
        /// files.
        /// </returns>
        public ReadOnlyCollection<string> GetSourceFiles()
        {
            return new List<string>(SourceFiles).AsReadOnly();
        }

        /// <summary>
        /// Updates the atlas and retrieves the generated <see cref="Bitmap"/>.
        /// </summary>
        /// <returns>The generated <see cref="Bitmap"/> of the atlas.</returns>
        public Bitmap GrabAtlasBitmap()
        {
            Atlas.SourceFiles = SourceFiles.AsReadOnly();
            Atlas.Refresh();

            return Atlas.Bitmap;
        }

        /// <summary>
        /// Removes a file by its index in the source files collection.
        /// </summary>
        /// <param name="index">The index of the file.</param>
        public void RemoveFileByIndex(int index)
        {
            SourceFiles.RemoveAt(index);
            IsChanged = true;
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
    }
}
