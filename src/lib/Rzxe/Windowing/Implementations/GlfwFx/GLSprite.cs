using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    internal sealed class GLSprite : ISprite
    {
        public Rectangle Bounds { get; private set; }
    
        public string Name { get; private set; }
        
        public Size Size
        {
            get { return Bounds.Size; }
        }


        public GLSprite(
            SpriteMappingModel model
        )
        {
            Bounds = model.Bounds;
            Name   = model.Name;
        }
    }
}
