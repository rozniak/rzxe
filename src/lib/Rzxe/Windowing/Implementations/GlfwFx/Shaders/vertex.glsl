﻿#version 330 core

layout(location = 0) in vec2  vsVertexPosition;
layout(location = 1) in vec2  vsVertexUV;
layout(location = 2) in vec4  vsSourceRect;
layout(location = 3) in vec2  vsOrigin;
layout(location = 4) in float vsDrawMode;

out vec2  fsUV;
out vec4  fsSourceRect;
out vec2  fsOrigin;
out float fsDrawMode;

uniform vec2 gCanvasResolution;


vec2 inViewportCoordSpace(
    in vec2 point
)
{
    return point * 2.0 - 1.0;
}

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
    // Set up vertex
    //
    gl_Position.xy = inViewportCoordSpace(
                         makeRelative(
                             vsVertexPosition,
                             gCanvasResolution
                         )
                     );
    gl_Position.z  = 1.0;
    gl_Position.w  = 1.0;
    
    // Pass-thru
    //
    fsUV         = vsVertexUV;
    fsSourceRect = vsSourceRect;
    fsOrigin     = vsOrigin;
    fsDrawMode   = vsDrawMode;
}