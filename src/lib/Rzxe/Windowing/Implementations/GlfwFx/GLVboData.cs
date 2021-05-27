/**
 * GLVboData.cs - OpenGL VBO Data Wrapper
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.MathUtils;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Represents OpenGL VBO data during sprite batching.
    /// </summary>
    internal sealed class GLVboData
    {
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// alpha component data for scaling alpha on sampled textures.
        /// </summary>
        public IList<float> AlphaScalesData
        {
            get { return _AlphaScalesData.AsReadOnly(); }
        }
        private List<float> _AlphaScalesData;
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// target vertex data.
        /// </summary>
        public IList<float> DrawContentsData
        {
            get { return _DrawContentsData; }
        }
        private List<float> _DrawContentsData;
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// draw mode data.
        /// </summary>
        public IList<float> DrawModesData
        {
            get { return _DrawModesData; }
        }
        private List<float> _DrawModesData;
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// origin vertex data.
        /// </summary>
        /// <remarks>
        /// This is used by the shader program in order to do tiling of sprites when
        /// drawing to a region larger than the sprite itself. The origin vertex is
        /// repeated in the VBO so that it can be modulo'd to calculate the UV
        /// co-ordinate to sample from.
        /// </remarks>
        public IList<float> OriginsData
        {
            get { return _OriginsData.AsReadOnly(); }
        }
        private List<float> _OriginsData;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// UV source rectangles.
        /// </summary>
        /// <remarks>
        /// This is also used by the shader program for sampling calculations, alongside
        /// <see cref="OriginsData"/>.
        /// </remarks>
        public IList<float> SourceRectsData
        {
            get { return _SourceRectsData.AsReadOnly(); }
        }
        private List<float> _SourceRectsData;
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// colour tinting data.
        /// </summary>
        public IList<float> TintsData
        {
            get { return _TintsData; }
        }
        private List<float> _TintsData;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// UV vertex data.
        /// </summary>
        public IList<float> UvContentsData
        {
            get { return _UvContentsData.AsReadOnly(); }
        }
        private List<float> _UvContentsData;
        
        /// <summary>
        /// Gets the number of vertexes to render in the buffer.
        /// </summary>
        public int VertexCount { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="GLVboData"/> class.
        /// </summary>
        /// <param name="atlas">
        /// The atlas to associate with.
        /// </param>
        public GLVboData(
            GLSpriteAtlas atlas
        )
        {
            _AlphaScalesData  = new List<float>();
            _DrawContentsData = new List<float>();
            _DrawModesData    = new List<float>();
            _OriginsData      = new List<float>();
            _SourceRectsData  = new List<float>();
            _TintsData        = new List<float>();
            _UvContentsData   = new List<float>();

            VertexCount = 0;
        }


        /// <summary>
        /// Expands a draw call into buffer data.
        /// </summary>
        /// <param name="sourceRect">
        /// The source rectangle on the atlas.
        /// </param>
        /// <param name="destRect">
        /// The region to draw into.
        /// </param>
        /// <param name="drawMode">
        /// The mode that defines how the region should be drawn.
        /// </param>
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw.
        /// </param>
        public void Draw(
            Rectanglei sourceRect,
            Rectanglei destRect,
            DrawMode   drawMode,
            Color      tint,
            float      alpha = 1.0f
        )
        {
            _DrawContentsData.AddRange(MakeVboData(destRect));
            _UvContentsData.AddRange(MakeVboData(sourceRect));

            _SourceRectsData.AddRange(
                CloneFillVboData(sourceRect)
            );
            _OriginsData.AddRange(
                CloneFillVboData(destRect.Position)
            );
            _DrawModesData.AddRange(
                CloneFillVboData((float) drawMode)
            );
            _AlphaScalesData.AddRange(
                CloneFillVboData(alpha)
            );
            _TintsData.AddRange(
                CloneFillVboData(tint)
            );

            VertexCount += 6;
        }
        
        /// <summary>
        /// Merges data from another VBO into the current one.
        /// </summary>
        /// <param name="source">
        /// The data source to merge.
        /// </param>
        public void Merge(
            GLVboData source
        )
        {
            _DrawContentsData.AddRange(source.DrawContentsData);
            _UvContentsData.AddRange(source.UvContentsData);
            _SourceRectsData.AddRange(source.SourceRectsData);
            _OriginsData.AddRange(source.OriginsData);
            _DrawModesData.AddRange(source.DrawModesData);
            _AlphaScalesData.AddRange(source.AlphaScalesData);
            _TintsData.AddRange(source.TintsData);

            VertexCount += source.VertexCount;
        }
        
        
        /// <summary>
        /// Clones values for insertion into the buffer.
        /// </summary>
        /// <param name="singleValue">
        /// The single float-point value to clone.
        /// </param>
        /// <returns>
        /// The cloned values as an array.
        /// </returns>
        public static float[] CloneFillVboData(
            float singleValue
        )
        {
            var buf = new List<float>();
            
            for (int i = 0; i < 6; i++)
            {
                buf.Add(singleValue);
            }

            return buf.ToArray();
        }
        
        /// <summary>
        /// Clones values for insertion into the buffer.
        /// </summary>
        /// <param name="vec2">
        /// The two-component vector value.
        /// </param>
        /// <returns>
        /// The cloned values as an array.
        /// </returns>
        public static float[] CloneFillVboData(
            Vector2i vec2
        )
        {
            var buf = new List<float>();
            
            for (int i = 0; i < 6; i++)
            {
                buf.Add(vec2.X);
                buf.Add(vec2.Y);
            }
            
            return buf.ToArray();
        }
        
        /// <summary>
        /// Clones values for insertion into the buffer.
        /// </summary>
        /// <param name="color">
        /// The colour data.
        /// </param>
        /// <returns>
        /// The cloned values as an array.
        /// </returns>
        public static float[] CloneFillVboData(
            Color color
        )
        {
            var buf = new List<float>();
            
            for (int i = 0; i < 6; i++)
            {
                buf.Add(color.R / 255f);
                buf.Add(color.G / 255f);
                buf.Add(color.B / 255f);
                buf.Add(color.A / 255f);
            }

            return buf.ToArray();
        }

        /// <summary>
        /// Clones values for insertion into the buffer.
        /// </summary>
        /// <param name="rect">
        /// The rectangle data.
        /// </param>
        /// <returns>
        /// The cloned values as an array.
        /// </returns>
        public static float[] CloneFillVboData(
            Rectanglei rect
        )
        {
            var buf = new List<float>();
            
            for (int i = 0; i < 6; i++)
            {
                buf.Add(rect.Left);
                buf.Add(rect.Top);
                buf.Add(rect.Width);
                buf.Add(rect.Height);
            }

            return buf.ToArray();
        }

        /// <summary>
        /// Creates an array of floating-point values from a rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle.
        /// </param>
        /// <returns>
        /// An array of floating-point values forming the rectangle, ready for
        /// insertion into a VBO.
        /// </returns>
        private static float[] MakeVboData(
            Rectanglei rect
        )
        {
            // Expand the rectangle to coordinates
            //
            float[] rectPoints = new float[]
            {
                rect.Left,  rect.Bottom,
                rect.Left,  rect.Top,
                rect.Right, rect.Top,
                
                rect.Left,  rect.Bottom,
                rect.Right, rect.Top,
                rect.Right, rect.Bottom
            };

            return rectPoints;
        }
    }
}
