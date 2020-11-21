using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    public interface ISpriteAtlas
    {
        IReadOnlyDictionary<string, IBorderBoxResource> BorderBoxes { get; }
        
        string Name { get; }
        
        Size Size { get; }
        
        IReadOnlyDictionary<string, ISprite> Sprites { get; }
    }
}
