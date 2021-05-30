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
using System.Linq;

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
        /// The filename for the border box definitions JSON document.
        /// </summary>
        private const string BorderBoxDefinitionsFilename = "borderboxes.json";
        
        /// <summary>
        /// The filename for the font definitions JSON document.
        /// </summary>
        private const string FontDefinitionsFilename = "fonts.json";
        
        
        /// <summary>
        /// The bitmap atlas.
        /// </summary>
        private Bitmap Atlas { get; set; }
        
        /// <summary>
        /// The intended size of the atlas.
        /// </summary>
        private Size AtlasSize { get; set; }
        
        /// <summary>
        /// The defined border boxes in the atlas.
        /// </summary>
        private IList<BorderBoxModel> BorderBoxDefinitions { get; set; }

        /// <summary>
        /// The defined fonts in the atlas.
        /// </summary>
        private IList<FontModel> FontDefinitions { get; set; }

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

            BorderBoxDefinitions = GetBorderBoxDefinitions(spriteRoot);
            FontDefinitions      = GetFontDefinitions(spriteRoot);
            
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

                    // We interject here - the sprite MIGHT be something special such
                    // as a font character, in which case we may need special
                    // handling for the name
                    //
                    string fontNamePrefix =
                        FindIfSpriteBelongsToFont(
                            sprite,
                            FontDefinitions
                        );
                        
                    if (fontNamePrefix != null)
                    {
                        string charName = sprite.Name.Substring(fontNamePrefix.Length);
                        
                        switch (charName)
                        {
                            case "apostrophe": charName =  "'"; break;
                            case "backslash":  charName = @"\"; break;
                            case "backtick":   charName =  "`"; break;
                            case "bang":       charName =  "!"; break;
                            case "bracel":     charName =  "{"; break;
                            case "bracer":     charName =  "}"; break;
                            case "bracketl":   charName =  "("; break;
                            case "bracketr":   charName =  ")"; break;
                            case "caret":      charName =  "^"; break;
                            case "colon":      charName =  ":"; break;
                            case "comma":      charName =  ","; break;
                            case "fullstop":   charName =  "."; break;
                            case "pipe":       charName =  "|"; break;
                            case "question":   charName =  "?"; break;
                            case "semicolon":  charName =  ";"; break;
                            case "tilde":      charName =  "~"; break;
                        }

                        node.LeafName = fontNamePrefix + charName;
                    }
                    else
                    {
                        node.LeafName = sprite.Name;
                    }
                    
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

            atlasModel.BorderBoxes.AddRange(BorderBoxDefinitions);
            atlasModel.Fonts.AddRange(FontDefinitions);

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
        /// Finds if the specified sprite is part of a font's character set.
        /// </summary>
        /// <param name="sprite">
        /// The sprite.
        /// </param>
        /// <param name="fonts">
        /// The collection of font definitions.
        /// </param>
        /// <returns>
        /// The sprite name prefix used by the font, if determined to be part of a
        /// font, otherwise null.
        /// </returns>
        private string FindIfSpriteBelongsToFont(
            LoadedBitmap     sprite,
            IList<FontModel> fonts
        )
        {
            FontModel font =
                fonts.FirstOrDefault(
                    (f) =>
                        sprite.Name.StartsWith(
                            f.SpriteNameBase,
                            StringComparison.InvariantCultureIgnoreCase
                        )
                );
                
            if (font == null)
            {
                return null;
            }

            return font.SpriteNameBase;
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
        
        /// <summary>
        /// Gets font definitions from the specified directory.
        /// </summary>
        /// <param name="spriteRoot">
        /// The root directory containing the definition file.
        /// </param>
        /// <returns>
        /// The list of definitions.
        /// </returns>
        private IList<FontModel> GetFontDefinitions(
            string spriteRoot
        )
        {
            string path = Path.Combine(spriteRoot, FontDefinitionsFilename);
            
            if (!File.Exists(path))
            {
                return new List<FontModel>().AsReadOnly();
            }

            return JsonConvert.DeserializeObject<List<FontModel>>(
                File.ReadAllText(path)
            ).AsReadOnly();
        }
        
        /// <summary>
        /// Gets border box definitions from the specified directory.
        /// </summary>
        /// <param name="spriteRoot">
        /// The root directory containing the definition file.
        /// </param>
        /// <returns>
        /// The list of definitions.
        /// </returns>
        private IList<BorderBoxModel> GetBorderBoxDefinitions(
            string spriteRoot
        )
        {
            string path = Path.Combine(spriteRoot, BorderBoxDefinitionsFilename);
            
            if (!File.Exists(path))
            {
                return new List<BorderBoxModel>().AsReadOnly();
            }

            return JsonConvert.DeserializeObject<List<BorderBoxModel>>(
                File.ReadAllText(path)
            ).AsReadOnly();
        }
    }
}
