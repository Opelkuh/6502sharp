#version 320 es
precision lowp float;
layout (location = 0) in vec2 inPos;
layout (location = 1) in vec2 inTex;

out vec2 texPos;

void main()
{
    gl_Position = vec4(inPos, 1.0, 1.0);
    texPos = inTex;
}