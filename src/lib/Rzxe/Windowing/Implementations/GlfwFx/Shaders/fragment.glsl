/**
 * fragment.glsl - Default Fragment Shader
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

#version 330 core

in vec2  fsUV;
in vec4  fsSourceRect;
in vec2  fsOrigin;
in float fsDrawMode;
in vec4  fsTint;
in float fsAlphaScale;

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

float scaleAlpha(
    in float sourceAlpha,
    in float scale
)
{
    float diff = sourceAlpha - (1.0 - scale);
    
    return clamp(diff, 0.0, 1.0);
}

vec3 tint(
    in vec3 baseColor,
    in vec4 tintColor
)
{
    return vec3(
        (tintColor.r * tintColor.a) + ((1 - tintColor.a) * baseColor.r),
        (tintColor.g * tintColor.a) + ((1 - tintColor.a) * baseColor.g),
        (tintColor.b * tintColor.a) + ((1 - tintColor.a) * baseColor.b)
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
    
    // Deal with final colour manipulation
    //
    vec4 sampled = texture(TextureSampler, scaledUV);
    vec3 tinted  = tint(sampled.rgb, fsTint);
    
    outColor =
        vec4(
            tinted,
            scaleAlpha(sampled.a, fsAlphaScale)
        );
}