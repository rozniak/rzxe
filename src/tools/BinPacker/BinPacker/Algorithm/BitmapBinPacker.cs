/**
 * BitmapBinPacker.cs - Bin Packer Algorithm
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Oddmatics.Rzxe.Tools.BinPacker.Algorithm
{
    /// <summary>
    /// Represents the bin packer algorithm for packing a bitmap.
    /// </summary>
    internal sealed class BitmapBinPacker
    {
        /// <summary>
        /// The acceptable image formats for the bin packer.
        /// </summary>
        private static string[] AcceptedImageFormats = { "*.bmp", "*.png" };
        
        
        /// <summary>
        /// The bitmap atlas.
        /// </summary>
        private Bitmap Atlas { get; set; }
        
        /// <summary>
        /// The intended size of the atlas.
        /// </summary>
        private Size AtlasSize { get; set; }

        /// <summary>
        /// The root node of bin packing.
        /// </summary>
        private BinPackerNode RootNode { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBinPacker"/> class.
        /// </summary>
        /// <param name="size">
        /// The size of the atlas.
        /// </param>
        public BitmapBinPacker(
            Size size
        )
        {
            AtlasSize = size;
        }
        
        
        /// <summary>
        /// Packs sprites into the atlas.
        /// </summary>
        /// <param name="spriteRoot">
        /// The root directory containing sprites.
        /// </param>
        public void PackSprites(
            string spriteRoot
        )
        {
            IList<LoadedBitmap> sprites = GetSprites(spriteRoot);
            
            // Bin off existing state, if any
            //
            if (Atlas != null)
            {
                Atlas.Dispose();
            }

            Atlas    = new Bitmap(AtlasSize.Width, AtlasSize.Height);
            RootNode = new BinPackerNode(
                           new Rectangle(0, 0, AtlasSize.Width, AtlasSize.Height),
                           null,
                           null,
                           null
                       );
            
            using (var g = Graphics.FromImage(Atlas))
            {
                foreach (LoadedBitmap sprite in sprites)
                {
                    BinPackerNode node =
                        RootNode.Insert(
                            RootNode,
                            new Rectangle(Point.Empty, sprite.Bitmap.Size)
                        );
                    
                    // Check we successfully inserted the node
                    //
                    if (node == null)
                    {
                        throw new InvalidOperationException(
                            "Unable to add any more sprites, out of room!"
                        );
                    }
                    
                    g.DrawImage(sprite.Bitmap, node.Bounds);
                    node.LeafName = sprite.Name;
                    
                    sprite.Dispose();
                }
            }
        }
        
        /// <summary>
        /// Saves the atlas data to the specified path.
        /// </summary>
        /// <param name="path">
        /// The target file path, the extension will be ignored.
        /// </param>
        public void Save(
            string path
        )
        {
            string baseFilename = Path.GetFileNameWithoutExtension(path);
            string directory    = Path.GetDirectoryName(path);
            string finalNoExt   = Path.Combine(directory, baseFilename);

            // Construct atlas metadata
            //
            var atlasModel = new AtlasModel();

            BuildAtlasModel(RootNode, ref atlasModel);

            // Now save
            //
            Atlas.Save(
                $"{finalNoExt}.png"
            );
            File.WriteAllText(
                $"{finalNoExt}.json",
                JsonConvert.SerializeObject(atlasModel)
            );
        }
        
        
        /// <summary>
        /// Recursively builds the atlas model metadata from the bin packed data.
        /// </summary>
        /// <param name="currentNode">
        /// The current node to read; if this is to be the first iteration, this should
        /// be the root node.
        /// </param>
        /// <param name="atlasModel">
        /// A reference to the <see cref="AtlasModel"/> being built.
        /// </param>
        private void BuildAtlasModel(
            BinPackerNode  currentNode,
            ref AtlasModel atlasModel
        )
        {
            if (currentNode.LeftChild != null)
            {
                BuildAtlasModel(
                    currentNode.LeftChild,
                    ref atlasModel
                );
            }
            
            if (currentNode.RightChild != null)
            {
                BuildAtlasModel(
                    currentNode.RightChild,
                    ref atlasModel
                );
            }
            
            if (!string.IsNullOrWhiteSpace(currentNode.LeafName))
            {
                atlasModel.SpriteMappings.Add(
                    new SpriteMappingModel()
                    {
                        Bounds = currentNode.Bounds,
                        Name   = currentNode.LeafName
                    }
                );
            }
        }

        /// <summary>
        /// Gets sprites from the specified directory.
        /// </summary>
        /// <param name="spriteRoot">
        /// The root directory containing sprites.
        /// </param>
        /// <returns>
        /// The list of sprites.
        /// </returns>
        private IList<LoadedBitmap> GetSprites(
            string spriteRoot
        )
        {
            var sprites = new List<LoadedBitmap>();
            
            foreach (string ext in AcceptedImageFormats)
            {
                var files = Directory.GetFiles(spriteRoot, ext);
                
                foreach (string file in files)
                {
                    sprites.Add(new LoadedBitmap(file));
                }
            }

            return sprites.AsReadOnly();
        }
    }
}
