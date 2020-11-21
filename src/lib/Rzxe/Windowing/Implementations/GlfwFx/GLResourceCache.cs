﻿using Oddmatics.Rzxe.Game;
using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    internal class GLResourceCache : IDisposable
    {
        private Dictionary<string, GLSpriteAtlas> Atlases { get; set; }

        private bool Disposing { get; set; }

        private IGameEngineParameters EngineParameters { get; set; }

        private Dictionary<string, uint> ShaderPrograms { get; set; }

        
        public GLResourceCache(IGameEngineParameters engineParameters)
        {
            Atlases = new Dictionary<string, GLSpriteAtlas>();
            EngineParameters = engineParameters;
            ShaderPrograms = new Dictionary<string, uint>();
        }


        public GLSpriteAtlas GetAtlas(string atlasName)
        {
            if (Disposing)
            {
                throw new ObjectDisposedException("GLFW Resource Cache");
            }

            string atlasNameLower = atlasName.ToLower();

            if (Atlases.ContainsKey(atlasNameLower))
            {
                return Atlases[atlasNameLower];
            }
            else
            {
                var newAtlas =
                    GLSpriteAtlas.FromFileSet(
                        Path.Combine(
                            EngineParameters.GameContentRoot,
                            "Atlas"
                        ),
                        atlasName
                    );

                Atlases.Add(newAtlas.Name, newAtlas);

                return newAtlas;
            }
        }

        public void Dispose()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException("GLFW Resource Cache");
            }

            Disposing = true;

            // Delete all shader programs
            //
            foreach (uint glProgramId in ShaderPrograms.Values)
            {
                GL.DeleteProgram(glProgramId);
            }

            ShaderPrograms.Clear();
            ShaderPrograms = null;

            // Delete all atlases
            //
            foreach (GLSpriteAtlas atlas in Atlases.Values)
            {
                atlas.Dispose();
            }

            Atlases.Clear();
            Atlases = null;
        }

        public uint GetShaderProgram(string programName)
        {
            if (Disposing)
            {
                throw new ObjectDisposedException("GLFW Resource Cache");
            }

            // If the shader program has previously been loaded, return its GL ID
            //
            if (ShaderPrograms.ContainsKey(programName))
            {
                return ShaderPrograms[programName];
            }

            // We reached here, program has not yet been cached, need to compile it
            // from sources
            //
            string shaderSrcPath =
                Path.Combine(
                    Environment.CurrentDirectory,
                    "Engine",
                    "Renderer",
                    "Shaders",
                    "GL"
                );
            
            string fragmentSource =
                File.ReadAllText(
                    Path.Combine(
                        shaderSrcPath,
                        "fragment.glsl"
                    )
                );
                
            string vertexSource =
                File.ReadAllText(
                    Path.Combine(
                        shaderSrcPath,
                        "vertex.glsl"
                    )
                );
            
            uint compiledProgramId =
                GLUtility.CompileShaderProgram(
                    vertexSource,
                    fragmentSource
                );

            ShaderPrograms.Add(programName, compiledProgramId);

            return compiledProgramId;
        }
    }
}
