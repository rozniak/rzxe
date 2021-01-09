/**
 * SpriteAnimationStore.cs - Sprite Animation Store
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using Oddmatics.Rzxe.Game.Animation.Models;
using System.Collections.Generic;
using System.IO;

namespace Oddmatics.Rzxe.Game.Animation
{
    /// <summary>
    /// Represents a store for sprite-based animations.
    /// </summary>
    public class SpriteAnimationStore
    {
        /// <summary>
        /// The internal store for animations by their names.
        /// </summary>
        private Dictionary<string, SpriteAnimationModel> Animations { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationStore"/> class.
        /// </summary>
        /// <param name="engineParameters">
        /// The game engine parameters.
        /// </param>
        public SpriteAnimationStore(
            IGameEngineParameters engineParameters
        )
        {
            Animations = new Dictionary<string, SpriteAnimationModel>();
        
            // Collect all animations from the content directory and load them
            //
            string[] animFiles =
                Directory.GetFiles(
                    Path.Combine(
                        engineParameters.GameContentRoot,
                        "Animations"
                    ),
                    "*.json"
                );

            foreach (string file in animFiles)
            {
                LoadAnimationDefinitions(file);
            }
        }
        
        
        /// <summary>
        /// Gets the animation.
        /// </summary>
        /// <param name="animName">
        /// The name of the animation.
        /// </param>
        /// <returns>
        /// The animation of the specified name from the store.
        /// </returns>
        public SpriteAnimation GetAnimation(
            string animName
        )
        {
            return new SpriteAnimation(Animations[animName]);
        }


        /// <summary>
        /// Loads animation definitions from the specified file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        private void LoadAnimationDefinitions(
            string filename
        )
        {
            string fileSrc = File.ReadAllText(filename);
            var    models  = JsonConvert.DeserializeObject<SpriteAnimationModel[]>(
                                 fileSrc
                             );
            
            foreach (SpriteAnimationModel model in models)
            {
                Animations.Add(model.Name, model);
            }
        }
    }
}
