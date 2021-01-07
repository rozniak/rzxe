/**
 * LoadedBitmap.cs - Loaded Bitmap File
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Drawing;
using System.IO;

namespace Oddmatics.Rzxe.Tools.BinPacker.Algorithm
{
    /// <summary>
    /// Represents a loaded bitmap file.
    /// </summary>
    internal sealed class LoadedBitmap : IDisposable
    {
        /// <summary>
        /// Gets the loaded bitmap.
        /// </summary>
        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Gets the name of the bitmap.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// The value that indicates whether the <see cref="LoadedBitmap"/> is
        /// disposed or disposing.
        /// </summary>
        private bool Disposing { get; set; }


        /// <summary>
        /// Initializes a new instance <see cref="LoadedBitmap"/> class using a file
        /// path to load from.
        /// </summary>
        /// <param name="path">
        /// The path to the image file to load.
        /// </param>
        public LoadedBitmap(
            string path
        )
        {
            Bitmap = (Bitmap)Image.FromFile(path);
            Name = Path.GetFileNameWithoutExtension(path);
        }
        
        
        /// <inheritdoc />
        public void Dispose()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(
                    "Bitmap already disposed."
                );
            }

            Bitmap.Dispose();
            Name = string.Empty;
        }
    }
}
