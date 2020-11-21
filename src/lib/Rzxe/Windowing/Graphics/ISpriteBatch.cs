using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    public interface ISpriteBatch
    {
        ISpriteAtlas Atlas { get; }
    
    
        void Draw(ISprite sprite, Point location);
    
        void Draw(Rectangle sourceRect, Point location);
    
        void Draw(ISprite sprite, Rectangle destRect, DrawMode drawMode);
        
        void Draw(Rectangle sourceRect, Rectangle destRect, DrawMode drawMode);
    
        void DrawBorderBox(IBorderBoxResource borderBox, Rectangle destRect);
        
        void DrawString(string text, string fontNameBase, Point location, int scale = 1);

        void Finish();
    }
}