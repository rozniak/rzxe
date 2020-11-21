using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    public class SpriteMappingModel
    {
        public Rectangle Bounds { get; set; }
    
        public string Name { get; set; }
    
    
        public SpriteMappingModel()
        {
            Bounds = Rectangle.Empty;
            Name   = string.Empty;
        }
    }
}
