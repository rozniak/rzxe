﻿#version 330 core

in vec2  fsUV;
in vec4  fsSourceRect;
in vec2  fsOrigin;
in float fsDrawMode;

out vec4 outColor;

uniform vec2      gCanvasResolution;
uniform vec2      gUvMapResolution;
uniform sampler2D TextureSampler;


vec2 makeRelative(
    in vec2 point,
    in vec2 factor
)
{
    return vec2(
        point.x / factor.x,
        1.0 - (point.y / factor.y)
    );
}


void main()
{
    // Set up UV
    //
    vec2 naturalUV = vec2(0, 0);
    vec2 scaledUV  = vec2(0, 0);
    
    if (fsDrawMode == 0)
    {
        naturalUV = fsUV;
    }
    else if (fsDrawMode == 1)
    {
        vec2 modUV;
        
        modUV =
            vec2(
                mod(gl_FragCoord.x - fsOrigin.x, fsSourceRect.z),
                mod(gCanvasResolution.y - gl_FragCoord.y - fsOrigin.y, fsSourceRect.w)
            );
                    
        naturalUV = fsSourceRect.xy + modUV.xy;
    }
    
    scaledUV =
        makeRelative(
            naturalUV,
            gUvMapResolution
        );

    outColor = texture(TextureSampler, scaledUV).rgba;
}