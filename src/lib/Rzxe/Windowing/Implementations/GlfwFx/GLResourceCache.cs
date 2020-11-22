/**
 * GLResourceCache.cs - Graphics Object Cache
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Game;
using Pencil.Gaming.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Represents a resource cache that retrieves and stores graphics objects for the
    /// OpenGL renderer implementation.
    /// </summary>
    internal class GLResourceCache : IDisposable
    {
        /// <summary>
        /// The collection of sprite atlases that are currently loaded.
        /// </summary>
        private Dictionary<string, GLSpriteAtlas> Atlases { get; set; }
        
        /// <summary>
        /// The value that indicates whether the class is disposing or has been
        /// disposed.
        /// </summary>
        private bool Disposing { get; set; }
        
        /// <summary>
        /// The game engine parameters.
        /// </summary>
        private IGameEngineParameters EngineParameters { get; set; }
        
        /// <summary>
        /// The mapping of shader program names to their corresponding IDs in OpenGL.
        /// </summary>
        private Dictionary<string, uint> ShaderPrograms { get; set; }

        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLResourceCache"/> class.
        /// </summary>
        /// <param name="engineParameters">
        /// The game engine parameters.
        /// </param>
        public GLResourceCache(
            IGameEngineParameters engineParameters
        )
        {
            Atlases          = new Dictionary<string, GLSpriteAtlas>();
            EngineParameters = engineParameters;
            ShaderPrograms   = new Dictionary<string, uint>();
        }
        
        
        /// <inheritdoc />
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
        
        /// <summary>
        /// Gets a sprite atlas from the cache - the atlas will be loaded if it is not
        /// already present.
        /// </summary>
        /// <param name="atlasName">
        /// The name of the atlas.
        /// </param>
        /// <returns>
        /// The requested sprite atlas.
        /// </returns>
        public GLSpriteAtlas GetAtlas(
            string atlasName
        )
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
        
        /// <summary>
        /// Gets a shader program from the cache - the program will be loaded and
        /// compiled from sources if it is not already present.
        /// </summary>
        /// <param name="programName">
        /// The name of the program.
        /// </param>
        /// <returns>
        /// The requested shader program.
        /// </returns>
        public uint GetShaderProgram(
            string programName
        )
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
