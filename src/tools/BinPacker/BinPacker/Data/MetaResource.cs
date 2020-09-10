using Oddmatics.Util.Collections;
using Oddmatics.Util.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oddmatics.Tools.BinPacker.Data
{
    /// <summary>
    /// Represents a resource that provides meta information about objects that can be
    /// created using assets within a given atlas.
    /// </summary>
    internal sealed class MetaResource : ChangeTrackingEx
    {
        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "The resource name cannot be null or whitespace."
                    );
                }

                _Name     = value;
                IsChanged = true;
            }
        }
        private string _Name;

        /// <summary>
        /// Gets or sets the kind of resource.
        /// </summary>
        public BinPackerResourceKind ResourceKind { get; private set; }

        /// <summary>
        /// Gets or sets the sprite mappings.
        /// </summary>
        public ChangeTrackingDictionary<string, string> SpriteMappings { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MetaResource"/> class.
        /// </summary>
        /// <param name="resourceKind">
        /// The kind of resource.
        /// </param>
        /// <param name="name">
        /// The name of the resource.
        /// </param>
        public MetaResource(BinPackerResourceKind resourceKind, string name)
        {
            Name           = name;
            ResourceKind   = resourceKind;
            SpriteMappings = new ChangeTrackingDictionary<string, string>();

            SpriteMappings.ValidationPredicate = ValidateSpriteMapping;
        }


        /// <inheritdoc />
        public override void AcceptChanges()
        {
            IsChanged = false;
        }


        /// <summary>
        /// Validates a sprite mapping being added.
        /// </summary>
        /// <param name="item">
        /// The sprite mapping.
        /// </param>
        /// <param name="collection">
        /// The sprite map collection.
        /// </param>
        /// <param name="reason">
        /// If the sprite mapping was determined to be invalid, contains the reason for
        /// the failure.
        /// </param>
        /// <returns>
        /// True if the sprite mapping is valid.
        /// </returns>
        private bool ValidateSpriteMapping(KeyValuePair<string, string> item, IEnumerable<KeyValuePair<string, string>> collection, out string reason)
        {
            string part   = item.Key;
            string sprite = item.Value;

            if (string.IsNullOrWhiteSpace(part))
            {
                reason = "Part name cannot be empty or whitespace.";
                return false;
            }

            string[] staticParts = GetStaticParts(ResourceKind);

            switch (ResourceKind)
            {
                case BinPackerResourceKind.BorderBox:
                    if (!staticParts.Contains(part))
                    {
                        reason = "Part is not valid for the resource.";
                        return false;
                    }

                    break;

                case BinPackerResourceKind.Font:
                    if (part.Length != 1)
                    {
                        reason = "Font resource parts must be individual characters.";
                        return false;
                    }

                    break;
            }

            if (!File.Exists(sprite))
            {
                reason = "The sprite cannot be found on the filesystem.";
                return false;
            }

            reason = null;
            return true;
        }


        /// <summary>
        /// Gets static parts for the specified resource kind.
        /// </summary>
        /// <param name="resourceKind">
        /// The resource kind.
        /// </param>
        /// <returns>
        /// The static parts for the specified resource kind, null if no static parts
        /// exist.
        /// </returns>
        public static string[] GetStaticParts(BinPackerResourceKind resourceKind)
        {
            switch (resourceKind)
            {
                case BinPackerResourceKind.BorderBox:
                    return new string[]
                    {
                        "tl",
                        "tm",
                        "tr",
                        "ml",
                        "mm",
                        "mr",
                        "bl",
                        "bm",
                        "br"
                    };

                case BinPackerResourceKind.Font:
                    return null;

                default:
                    throw new NotImplementedException("Unknown resource kind.");
            }
        }
    }
}
