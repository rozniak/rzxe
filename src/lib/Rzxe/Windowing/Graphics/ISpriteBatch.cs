using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    public interface ISpriteBatch
    {
        void Draw(string spriteName, Point location);
    
        void Draw(Rectangle sourceRect, Point location);
    
        void Draw(string spriteName, Rectangle rect, DrawMode drawMode);
        
        void Draw(Rectangle sourceRect, Rectangle rect, DrawMode drawMode);

        void DrawBorderBox(string spriteName, Rectangle rect);
        
        void DrawString(string text, string fontNameBase, Point location, int scale = 1);

        void Finish();
    }
}
