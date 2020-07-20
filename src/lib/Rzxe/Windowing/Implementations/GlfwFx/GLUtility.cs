﻿using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    internal static class GLUtility
    {
        public static readonly float[] Vec2Zero = new float[] { 0f, 0f };
        public static readonly float[] Vec3Zero = new float[] { 0f, 0f, 0f };
        public static readonly float[] Vec4Zero = new float[] { 0f, 0f, 0f, 0f };
    
    
        public static uint CompileShaderProgram(string vertexSource, string fragmentSource)
        {
            string infoLog = String.Empty; // Store any error information

            // Create the shader objects
            //
            uint vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            uint fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);

            // Compile vertex shader
            //
            GL.ShaderSource(vertexShaderId, vertexSource);
            GL.CompileShader(vertexShaderId);

            GL.GetShaderInfoLog((int)vertexShaderId, out infoLog);

            if (infoLog.Length > 0)
                throw new ArgumentException("GlUtil: Failed to compile vertex shader, message: " + infoLog);

            // Compile fragment shader
            //
            GL.ShaderSource(fragmentShaderId, fragmentSource);
            GL.CompileShader(fragmentShaderId);

            GL.GetShaderInfoLog((int)fragmentShaderId, out infoLog);

            if (infoLog.Length > 0)
                throw new ArgumentException("GlUtil: Failed to compile fragment shader, message: " + infoLog);

            // Link the program
            //
            uint programId = GL.CreateProgram();

            GL.AttachShader(programId, vertexShaderId);
            GL.AttachShader(programId, fragmentShaderId);
            GL.LinkProgram(programId);

            GL.GetProgramInfoLog((int)programId, out infoLog);

            if (infoLog.Length > 0)
                throw new ArgumentException("GlUtil: Failed to link shaders to program, message: " + infoLog);

            // Delete old compiled shader source objects
            //
            GL.DetachShader(programId, vertexShaderId);
            GL.DetachShader(programId, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            return programId;
        }

        public static int LoadBitmapTexture(Bitmap bmp)
        {
            // Flip texture before processing
            //
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // Acquire bitmap raw data handle
            //
            BitmapData data = bmp.LockBits(
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

        public static float[] MakeVboData(System.Drawing.Rectangle rect)
        {
            // Expand the rectangle to coordinates
            //
            float[] rectPoints = new float[]
            {
                rect.Left, rect.Bottom,
                rect.Left, rect.Top,
                rect.Right, rect.Top,

                rect.Left, rect.Bottom,
                rect.Right, rect.Top,
                rect.Right, rect.Bottom
            };

            return rectPoints;
        }

        public static float[] MakeVboData(Rectanglei rect)
        {
            // Expand the rectangle to coordinates
            //
            float[] rectPoints = new float[]
            {
                rect.Left, rect.Bottom,
                rect.Left, rect.Top,
                rect.Right, rect.Top,

                rect.Left, rect.Bottom,
                rect.Right, rect.Top,
                rect.Right, rect.Bottom
            };

            return rectPoints;
        }
    }
}
