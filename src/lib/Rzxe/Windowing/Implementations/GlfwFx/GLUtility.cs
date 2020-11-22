/**
 * GLUtility.cs - OpenGL Utility Methods
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Provides utility methods for working with OpenGL.
    /// </summary>
    internal static class GLUtility
    {
        /// <summary>
        /// Represents a 2-dimensional vector set to zero.
        /// </summary>
        public static readonly float[] Vec2Zero = new float[] { 0f, 0f };
        
        /// <summary>
        /// Represents a 3-dimensional vector set to zero.
        /// </summary>
        public static readonly float[] Vec3Zero = new float[] { 0f, 0f, 0f };
        
        /// <summary>
        /// Represents a 4-dimensional vector set to zero.
        /// </summary>
        public static readonly float[] Vec4Zero = new float[] { 0f, 0f, 0f, 0f };
        
        
        /// <summary>
        /// Compiles a shader program from the specified sources.
        /// </summary>
        /// <param name="vertexSource">
        /// The vertex shader source.
        /// </param>
        /// <param name="fragmentSource">
        /// The fragment shader source.
        /// </param>
        /// <returns>
        /// The ID in OpenGL for the compiled shader program.
        /// </returns>
        public static uint CompileShaderProgram(
            string vertexSource,
            string fragmentSource
        )
        {
            string infoLog = string.Empty; // Store any error information

            // Create the shader objects
            //
            uint vertexShaderId   = GL.CreateShader(ShaderType.VertexShader);
            uint fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);

            // Compile vertex shader
            //
            GL.ShaderSource(vertexShaderId, vertexSource);
            GL.CompileShader(vertexShaderId);

            GL.GetShaderInfoLog((int) vertexShaderId, out infoLog);

            if (infoLog.Length > 0)
            {
                throw new ArgumentException(
                    $"Failed to compile vertex shader, message: {infoLog}"
                );
            }

            // Compile fragment shader
            //
            GL.ShaderSource(fragmentShaderId, fragmentSource);
            GL.CompileShader(fragmentShaderId);

            GL.GetShaderInfoLog((int) fragmentShaderId, out infoLog);

            if (infoLog.Length > 0)
            {
                throw new ArgumentException(
                    $"Failed to compile fragment shader, message: {infoLog}"
                );
            }

            // Link the program
            //
            uint programId = GL.CreateProgram();

            GL.AttachShader(programId, vertexShaderId);
            GL.AttachShader(programId, fragmentShaderId);
            GL.LinkProgram(programId);

            GL.GetProgramInfoLog((int) programId, out infoLog);

            if (infoLog.Length > 0)
            {
                throw new ArgumentException(
                    $"Failed to link shaders to program, message: {infoLog}"
                );
            }

            // Delete old compiled shader source objects
            //
            GL.DetachShader(programId, vertexShaderId);
            GL.DetachShader(programId, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            return programId;
        }
        
        /// <summary>
        /// Loads the specified <see cref="Bitmap"/> into an OpenGL texture.
        /// </summary>
        /// <param name="bmp">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The ID in OpenGL for the texture created from the <see cref="Bitmap"/>.
        /// </returns>
        public static int LoadBitmapTexture(
            Bitmap bmp
        )
        {
            // Flip texture before processing
            //
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // Acquire bitmap raw data handle
            //
            BitmapData data =
                bmp.LockBits(
                    new System.Drawing.Rectangle(Point.Empty, bmp.Size),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );

            // Create the OpenGL texture
            //
            int textureId = GL.GenTexture();

            // Bind texture and load bitmap data
            //
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba8,
                bmp.Width,
                bmp.Height,
                0,
                Pencil.Gaming.Graphics.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0
            );

            // Set texture parameters
            //
            GL.TexParameterI(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                new int[] { (int)TextureMagFilter.Nearest }
            );
            GL.TexParameterI(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                new int[] { (int)TextureMinFilter.Nearest }
            );

            // Unlock bitmap raw data
            //
            bmp.UnlockBits(data);

            return textureId;
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
        public static float[] MakeVboData(
            System.Drawing.Rectangle rect
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
        public static float[] MakeVboData(
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
