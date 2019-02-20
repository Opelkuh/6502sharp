using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace NES.Rendering
{
    class ShaderProgram
    {
        private int program;
        private List<Shader> shaders = new List<Shader>();

        public ShaderProgram()
        {
            program = GL.CreateProgram();
        }

        public ShaderProgram Add(Shader shader)
        {
            shaders.Add(shader);
            GL.AttachShader(program, shader.Id);

            return this;
        }

        public int Finish()
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int status);
            if (status != 1)
            {
                GL.GetProgramInfoLog(program, out string log);
                throw new System.Exception($"Couldn't link shader program. Log: {log}");
            }

            // delete all linked shaders
            foreach (Shader shader in shaders)
            {
                shader.Delete();
            }

            return program;
        }
    }
}