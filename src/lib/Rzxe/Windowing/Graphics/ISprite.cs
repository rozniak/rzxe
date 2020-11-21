using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    public interface ISprite
    {
        string Name { get; }
        
        Size Size { get; }
    }
}
