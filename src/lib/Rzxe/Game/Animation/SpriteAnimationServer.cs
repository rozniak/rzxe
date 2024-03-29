﻿/**
 * SpriteAnimationServer.cs - Sprite Animation Server
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Game.Animation
{
    /// <summary>
    /// Represents a system for playing sprite-based animations.
    /// </summary>
    public class SpriteAnimationServer
    {
        /// <summary>
        /// The active animation.
        /// </summary>
        private SpriteAnimation ActiveAnimation { get; set; }
        
        /// <summary>
        /// The animation store.
        /// </summary>
        private SpriteAnimationStore AnimationStore { get; set; }
        
        
        /// <summary>
        /// Occurs when the active animation has finished playback.
        /// </summary>
        public event EventHandler FinishedPlayback
        {
            add
            {
                if (ActiveAnimation == null)
                {
                    return;
                }

                ActiveAnimation.FinishedPlayback += value;
            }
            
            remove
            {
                if (ActiveAnimation == null)
                {
                    return;
                }

                ActiveAnimation.FinishedPlayback -= value;
            }
        }

        /// <summary>
        /// Occurs when a frame that emits an event has been entered in the active
        /// animation.
        /// </summary>
        public event EventHandler SpecialFrameEntered
        {
            add
            {
                if (ActiveAnimation == null)
                {
                    return;
                }

                ActiveAnimation.SpecialFrameEntered += value;
            }

            remove
            {
                if (ActiveAnimation == null)
                {
                    return;
                }

                ActiveAnimation.SpecialFrameEntered -= value;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationServer"/>
        /// class.
        /// </summary>
        /// <param name="store">
        /// The animation store.
        /// </param>
        public SpriteAnimationServer(
            SpriteAnimationStore store
        )
        {
            AnimationStore = store;
        }
        
        
        /// <summary>
        /// Gets the current frame of the animation.
        /// </summary>
        /// <returns>
        /// The current frame of the animation.
        /// </returns>
        public SpriteAnimationFrame GetCurrentFrame()
        {
            return ActiveAnimation?.CurrentFrame;
        }
        
        /// <summary>
        /// Selects an animation and begins playback immediately.
        /// </summary>
        /// <param name="animName">
        /// The name of the animation.
        /// </param>
        public void GoToAndPlay(
            string animName
        )
        {
            if (animName == ActiveAnimation?.Name)
            {
                ActiveAnimation.Restart();
            }
            else
            {
                ActiveAnimation = AnimationStore.GetAnimation(animName);
                ActiveAnimation.Play();
            }
        }
        
        /// <summary>
        /// Selects an animation and pauses on the first frame.
        /// </summary>
        /// <param name="animName">
        /// The name of the animation.
        /// </param>
        public void GoToAndStop(
            string animName
        )
        {
            if (animName == ActiveAnimation?.Name)
            {
                ActiveAnimation.Stop();
            }
            else
            {
                ActiveAnimation = AnimationStore.GetAnimation(animName);
            }
        }
        
        /// <summary>
        /// Progresses the active animation.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time that has passed.
        /// </param>
        public void Progress(
            TimeSpan deltaTime
        )
        {
            ActiveAnimation?.Progress(deltaTime);
        }
    }
}
