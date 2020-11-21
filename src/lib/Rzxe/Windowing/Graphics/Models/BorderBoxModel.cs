using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    public class BorderBoxModel
    {
        public string Name { get; set; }
        
        public int[] Slices { get; set; }
        
        public string SpriteName { get; set; }
    
    
        public BorderBoxModel()
        {
            Name       = string.Empty;
            Slices     = new int[] { };
            SpriteName = string.Empty;
        }
    }
}
