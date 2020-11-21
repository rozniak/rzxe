using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    public class AtlasModel
    {
        public List<BorderBoxModel> BorderBoxes { get; set; }
        
        public List<SpriteMappingModel> SpriteMappings { get; set; }
    
    
        public AtlasModel()
        {
            BorderBoxes    = new List<BorderBoxModel>();
            SpriteMappings = new List<SpriteMappingModel>();
        }
    }
}
