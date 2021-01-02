/**
 * AnimationStore.cs - Actor Animation Store
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using Oddmatics.Rzxe.Game.Actors.Animation.Models;
using System.Collections.Generic;
using System.IO;

namespace Oddmatics.Rzxe.Game.Actors.Animation
{
    /// <summary>
    /// Represents a store for actor animations.
    /// </summary>
    public class AnimationStore
    {
        /// <summary>
        /// The internal store for animations by their names.
        /// </summary>
        private Dictionary<string, AnimationModel> Animations { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationStore"/> class.
        /// </summary>
        /// <param name="engineParameters">
        /// The game engine parameters.
        /// </param>
        public AnimationStore(
            IGameEngineParameters engineParameters
        )
        {
            Animations = new Dictionary<string, AnimationModel>();
        
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
        public ActorAnimation GetAnimation(
            string animName
        )
        {
            return new ActorAnimation(Animations[animName]);
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
            var    models  = JsonConvert.DeserializeObject<AnimationModel[]>(fileSrc);
            
            foreach (AnimationModel model in models)
            {
                Animations.Add(model.Name, model);
            }
        }
    }
}
