using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace NES.Rendering
{
    class Shader
    {
        public int Id => id;
        public ShaderType Type => type;

        private int id;
        private ShaderType type;
        private string path;
        private string code;

        public Shader(ShaderType type, string path)
        {
            this.type = type;
            this.path = path;

            // load file
            code = File.ReadAllText(path);

            // generate OpenGL shader
            id = GL.CreateShader(type);

            GL.ShaderSource(id, code);
            GL.CompileShader(id);

            checkStatus();
        }

        public void Delete()
        {
            GL.DeleteShader(id);
        }

        private void checkStatus()
        {
            GL.GetShader(id, ShaderParameter.CompileStatus, out int status);

            // return if shader compiled successfully
            if (status == 1) return;

            GL.GetShaderInfoLog(id, out string log);
            throw new Exception($"Couldn't compile shader at '{path}'. Log: {log}");
        }
    }
}