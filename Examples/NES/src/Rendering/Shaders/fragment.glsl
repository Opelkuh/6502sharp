#version 320 es
precision lowp float;
out vec4 FragColor;
  
in vec2 texPos;

uniform sampler2D inTexture;

void main()
{
    FragColor = texture(inTexture, texPos);
}