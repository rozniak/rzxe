/**
 * BinPackerArguments.cs - Bin Packer Command Line Arguments
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Tools.BinPacker.CommandLine
{
    /// <summary>
    /// Represents command line arguments for the bin packer program.
    /// </summary>
    internal class BinPackerArguments
    {
        /// <summary>
        /// Gets the target size of the atlas that sprites will be bin packed into.
        /// </summary>
        public Size AtlasSize { get; private set; }
        
        /// <summary>
        /// Gets the output filename.
        /// </summary>
        public string OutputFilename { get; private set; }
        
        /// <summary>
        /// Gets the folder target for the sprite resources to pack.
        /// </summary>
        public string SpriteFolderTarget { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BinPackerArguments"/> class
        /// with the command line arguments.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        public BinPackerArguments(
            string[] args
        )
        {
            if (args.Length != 4)
            {
                throw new ArgumentException(
                    "Incorrect number of arguments provided."
                );
            }

            AtlasSize          = new Size(
                                     Convert.ToInt32(args[0]),
                                     Convert.ToInt32(args[1])
                                 );
            SpriteFolderTarget = args[2];
            OutputFilename     = args[3];

            AssertValidAtlasSize();
        }
        
        
        /// <summary>
        /// Asserts that the atlas size is valid.
        /// </summary>
        private void AssertValidAtlasSize()
        {
            // Cannot be 0 size in either direction
            //
            if (
                AtlasSize.Height == 0 ||
                AtlasSize.Width  == 0
            )
            {
                throw new NotSupportedException(
                    "Atlas cannot be 0 size."
                );
            }
            
            // Must be a power of 2
            //
            if (
                !IsPowerOfTwo(AtlasSize.Height) ||
                !IsPowerOfTwo(AtlasSize.Width)
            )
            {
                throw new NotSupportedException(
                    "Atlas dimensions must be powers of 2."
                );
            }
        }
        
        /// <summary>
        /// Determines whether the specified number is a power of two.
        /// </summary>
        /// <param name="num">
        /// The number.
        /// </param>
        /// <returns>
        /// True if the number is a power of two.
        /// </returns>
        private bool IsPowerOfTwo(
            int num
        )
        {
            return (num & (num - 1)) == 0;
        }
    }
}
