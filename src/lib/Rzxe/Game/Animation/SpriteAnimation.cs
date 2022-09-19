/**
 * SpriteAnimation.cs - Sprite Animation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Extensions;
using Oddmatics.Rzxe.Game.Animation.Models;
using System;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Game.Animation
{
    /// <summary>
    /// Represents a sprite-based animation.
    /// </summary>
    public class SpriteAnimation
    {
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public SpriteAnimationFrame CurrentFrame
        {
            get { return Frameset[CurrentFrameIndex]; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the animation is playing.
        /// </summary>
        public bool IsPlaying { get; private set; }
        
        /// <summary>
        /// Gets the name of the animation.
        /// </summary>
        public string Name { get; private set; }
        
        
        /// <summary>
        /// The index of the current frame.
        /// </summary>
        private int CurrentFrameIndex { get; set; }
        
        /// <summary>
        /// The current ticks progressed in the active frame.
        /// </summary>
        private int CurrentFrameTickCount { get; set; }
        
        /// <summary>
        /// The current ticks progressed in the animation.
        /// </summary>
        private int CurrentTicks
        {
            get { return CurrentTime.Div(TickSize); }
        }
        
        /// <summary>
        /// The current time progressed in the animation.
        /// </summary>
        private TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// The animation's frameset.
        /// </summary>
        private IList<SpriteAnimationFrame> Frameset { get; set; }

        /// <summary>
        /// The length of a single tick when stepping through the animation.
        /// </summary>
        private TimeSpan TickSize { get; set; }
        
        /// <summary>
        /// The total duration of the animation.
        /// </summary>
        private TimeSpan TotalDuration { get; set; }


        /// <summary>
        /// Occurs when animation has finished playback.
        /// </summary>
        public event EventHandler FinishedPlayback;
        
        /// <summary>
        /// Occurs when an event-triggering frame has been entered.
        /// </summary>
        public event EventHandler SpecialFrameEntered;


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimation"/> class.
        /// </summary>
        /// <param name="animModel">
        /// The data model that contains information about the animation.
        /// </param>
        internal SpriteAnimation(
            SpriteAnimationModel animModel
        )
        {
            CurrentFrameIndex = 0;
            CurrentTime       = TimeSpan.Zero;
            IsPlaying         = false;
            Name              = animModel.Name;
            TickSize          = TimeSpan.FromMilliseconds(animModel.TickSize);

            // Construct frameset and calculate total duration
            //
            var frameset = new List<SpriteAnimationFrame>();
            
            foreach (SpriteAnimationFrameModel frame in animModel.Frames)
            {
                frameset.Add(
                    new SpriteAnimationFrame(frame)
                );
                
                TotalDuration =
                    TotalDuration.Add(
                        TickSize.Multiply(frame.Ticks)
                    );
            }

            Frameset = frameset.AsReadOnly();
        }
        
        
        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            IsPlaying = false;
        }
        
        /// <summary>
        /// Plays or resumes the animation.
        /// </summary>
        public void Play()
        {
            IsPlaying = true;
        }
        
        /// <summary>
        /// Progresses the animation by the specified time span.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time that has passed.
        /// </param>
        public void Progress(
            TimeSpan deltaTime
        )
        {
            if (!IsPlaying)
            {
                return;
            }
            
            TimeSpan next  = CurrentTime.Add(deltaTime);
            int      ticks = next.Div(TickSize) - CurrentTicks;
            
            for (int i = 0; i < ticks; i++)
            {
                CurrentFrameTickCount++;
                
                if (CurrentFrameTickCount >= CurrentFrame.Ticks)
                {
                    CurrentFrameIndex++;
                    CurrentFrameTickCount = 0;
                    
                    if (CurrentFrameIndex >= Frameset.Count)
                    {
                        FinishedPlayback?.Invoke(this, EventArgs.Empty);
                        CurrentFrameIndex = 0;
                    }
                    
                    // Check if the next frame should emit an event
                    //
                    if (CurrentFrame.ShouldEmitEvent)
                    {
                        SpecialFrameEntered?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            CurrentTime = next.Mod(TotalDuration);
        }

        /// <summary>
        /// Restarts and plays the animation.
        /// </summary>
        public void Restart()
        {
            CurrentFrameIndex = 0;
            CurrentTime       = TimeSpan.Zero;
            IsPlaying         = true;
        }
        
        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            CurrentFrameIndex = 0;
            CurrentTime       = TimeSpan.Zero;
            IsPlaying         = false;
        }
    }
}
