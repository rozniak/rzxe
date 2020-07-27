﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Tools.BinPacker.Algorithm
{
    /// <summary>
    /// Represents a bin packer instance for generating an atlas bitmap.
    /// </summary>
    internal sealed class BitmapBinPacker : IDisposable
    {
        /// <summary>
        /// Gets the generated atlas.
        /// </summary>
        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Gets the value that indicates whether this <see cref="BitmapBinPacker"/>
        /// has been disposed or not.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Gets or sets the size of the atlas.
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Gets or sets the source files to use when generating the atlas.
        /// </summary>
        public IList<string> SourceFiles { get; set; }


        /// <summary>
        /// The root node for the bin packing algorithm.
        /// </summary>
        private BinPackerNode RootNode;


        /// <summary>
        /// Initializes a new instance of <see cref="BitmapBinPacker"/>.
        /// </summary>
        public BitmapBinPacker()
        {
            Bitmap = null;
            Disposed = false;
            Size = Size.Empty;
            SourceFiles = new List<string>().AsReadOnly();
            RootNode = null;
        }


        /// <summary>
        /// Releases all resources used by this <see cref="BitmapBinPacker"/>.
        /// </summary>
        public void Dispose()
        {
            AssertNotDisposed();
        }

        /// <summary>
        /// Refreshes the generated atlas <see cref="Bitmap"/>.
        /// </summary>
        public void Refresh()
        {
            AssertNotDisposed();

            // Get ourselves a clean Bitmap
            //
            var newBitmap = new Bitmap(Size.Width, Size.Height);

            // Prepare our root node
            //
            RootNode = new BinPackerNode(
                new Rectangle(0, 0, Size.Width, Size.Height),
                null,
                null,
                null
                );

            // Now do the actual bin packing
            //
            var result = BinPackerError.None;
            var ex = new Exception("Unknown error occurred.");

            using (var g = Graphics.FromImage(newBitmap))
            {
                foreach (string filePath in SourceFiles)
                {
                    Bitmap sprite;
                    string spriteName = Path.GetFileNameWithoutExtension(filePath);
                    BinPackerNode node;

                    if (!File.Exists(filePath))
                    {
                        result = BinPackerError.MissingFile;
                        ex = new FileNotFoundException(
                            "Failed to find a source file.",
                            filePath
                            );

                        break;
                    }

                    try
                    {
                        sprite = (Bitmap)Bitmap.FromFile(filePath);
                    }
                    catch (Exception anyEx)
                    {
                        result = BinPackerError.UnreadableFile;
                        ex = anyEx;

                        break;
                    }

                    node = RootNode.Insert(
                        RootNode,
                        new Rectangle(Point.Empty, sprite.Size)
                        );

                    if (node != null)
                    {
                        g.DrawImage(sprite, node.Bounds);
                        node.LeafName = spriteName;
                        sprite.Dispose();
                    }
                    else
                    {
                        result = BinPackerError.OutOfRoom;
                        ex = new InvalidOperationException(
                            "There is not enough room in the atlas to contain all of the sprites."
                            );
                        sprite.Dispose();

                        break;
                    }
                }
            }

            if (result != BinPackerError.None)
                throw ex;

            Bitmap?.Dispose();
            Bitmap = newBitmap;
        }

        /// <summary>
        /// Saves the atlas data to disk.
        /// </summary>
        /// <param name="fullFilePath">The full file path (minus extension) to save atlas information at.</param>
        public void Save(string fullFilePath)
        {
            AssertNotDisposed();

            string noExt = Path.GetFileNameWithoutExtension(fullFilePath);
            string path = Path.GetDirectoryName(fullFilePath);

            Bitmap.Save(path + "\\" + noExt + ".png");

            IList<SpriteInfo> atlasInfo = BuildAtlas();

            File.WriteAllText(
                String.Format(
                    @"{0}\\{1}.json",
                    path,
                    noExt
                    ),
                JsonConvert.SerializeObject(atlasInfo)
                );
        }


        /// <summary>
        /// Asserts that this <see cref="BitmapBinPacker"/> has not been disposed.
        /// </summary>
        private void AssertNotDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(
                    "This BitmapBinPacker instance has been disposed."
                    );
            }
        }

        /// <summary>
        /// Builds the atlas information for this <see cref="BitmapBinPacker"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="IList{SpriteInfo}"/> collection that contains all
        /// information about the atlas.
        /// </returns>
        private IList<SpriteInfo> BuildAtlas()
        {
            var list = new List<SpriteInfo>();

            DiscoverSprites(RootNode, ref list);

            return list.AsReadOnly();
        }

        /// <summary>
        /// Maps sprite information to the specified list recursively.
        /// </summary>
        /// <param name="currentNode">
        /// The current node to discover sprites from; if this is to be the first
        /// iteration, this should be the root node.
        /// </param>
        /// <param name="list">
        /// A reference to the <see cref="List{SpriteInfo}"/> collection into which all
        /// sprite information will be inserted.
        /// </param>
        private void DiscoverSprites(
            BinPackerNode currentNode,
            ref List<SpriteInfo> list
            )
        {
            if (currentNode.LeftChild != null)
                DiscoverSprites(currentNode.LeftChild, ref list);

            if (currentNode.RightChild != null)
                DiscoverSprites(currentNode.RightChild, ref list);

            if (currentNode.LeafName != null)
                list.Add(new SpriteInfo(currentNode.LeafName, currentNode.Bounds));
        }
    }
}
