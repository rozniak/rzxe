/**
 * Program.cs - Bin Packer Entry Point
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Tools.BinPacker.Algorithm;
using Oddmatics.Rzxe.Tools.BinPacker.CommandLine;
using System;
using System.IO;

namespace Oddmatics.Rzxe.Tools.BinPacker
{
    /// <summary>
    /// The main program class for the bin packer.
    /// </summary>
    internal sealed class MainClass
    {
        /// <summary>
        /// The main entry point for the bin packer tool.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        public static void Main(
            string[] args
        )
        {
            try
            {
                var arguments = new BinPackerArguments(args);
                var packer    = new BitmapBinPacker(arguments.AtlasSize);
                
                packer.PackSprites(
                    arguments.SpriteFolderTarget
                );
                
                packer.Save(
                    arguments.OutputFilename
                );

                Console.WriteLine(
                    $"Success! Bin packed output at: {arguments.OutputFilename}\n\n"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Bin packing failure: {ex.Message}\n\n{ex.StackTrace}\n\n"
                );
            }
        }
    }
}
