using System;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Template
{
    public class Shader
    {
        // data members
        public int programID, vsID, fsID;
        public int in_vertexPositionObject;
        public int in_vertexNormalObject;
        public int in_vertexUV;
        public int uniform_objectToScreen;
        public int uniform_objectToWorld;
        public int uniform_ambientLightColor;
        public int uniform_lightPosition1;
        public int uniform_lightPosition2;
        public int uniform_lightPosition3;
        public int uniform_lightPosition4;
        public int uniform_lightColor1;
        public int uniform_lightColor2;
        public int uniform_lightColor3;
        public int uniform_lightColor4;
        public int uniform_lightIntensity1;
        public int uniform_lightIntensity2;
        public int uniform_lightIntensity3;
        public int uniform_lightIntensity4;
        public int uniform_viewPosition;

        public int uniform_lightPositionSpotlight;
        public int uniform_lightDirectionSpotlight;
        public int uniform_lightInnerCutoffAngleSpotlight;
        public int uniform_lightOuterCutoffAngleSpotlight;
        public int uniform_lightColorSpotlight;
        public int uniform_lightIntensitySpotlight;

        // constructor
        public Shader(string vertexShader, string fragmentShader)
        {
            // compile shaders
            programID = GL.CreateProgram();
            if (!OpenTKApp.isMac) GL.ObjectLabel(ObjectLabelIdentifier.Program, programID, -1, vertexShader + " + " + fragmentShader);
            Load(vertexShader, ShaderType.VertexShader, programID, out vsID);
            Load(fragmentShader, ShaderType.FragmentShader, programID, out fsID);
            GL.LinkProgram(programID);
            string infoLog = GL.GetProgramInfoLog(programID);
            if (infoLog.Length != 0) Console.WriteLine(infoLog);

            // get locations of shader parameters
            in_vertexPositionObject = GL.GetAttribLocation(programID, "vertexPositionObject");
            in_vertexNormalObject = GL.GetAttribLocation(programID, "vertexNormalObject");
            in_vertexUV = GL.GetAttribLocation(programID, "vertexUV");
            uniform_objectToScreen = GL.GetUniformLocation(programID, "objectToScreen");
            uniform_objectToWorld = GL.GetUniformLocation(programID, "objectToWorld");
            uniform_ambientLightColor = GL.GetUniformLocation(programID, "ambientLightColor");
            uniform_lightPosition1 = GL.GetUniformLocation(programID, "lightPosition1");
            uniform_lightPosition2 = GL.GetUniformLocation(programID, "lightPosition2");
            uniform_lightPosition3 = GL.GetUniformLocation(programID, "lightPosition3");
            uniform_lightPosition4 = GL.GetUniformLocation(programID, "lightPosition4");
            uniform_lightColor1 = GL.GetUniformLocation(programID, "lightColor1");
            uniform_lightColor2 = GL.GetUniformLocation(programID, "lightColor2");
            uniform_lightColor3 = GL.GetUniformLocation(programID, "lightColor3");
            uniform_lightColor4 = GL.GetUniformLocation(programID, "lightColor4");
            uniform_lightIntensity1 = GL.GetUniformLocation(programID, "lightIntensity1");
            uniform_lightIntensity2 = GL.GetUniformLocation(programID, "lightIntensity2");
            uniform_lightIntensity3 = GL.GetUniformLocation(programID, "lightIntensity3");
            uniform_lightIntensity4 = GL.GetUniformLocation(programID, "lightIntensity4");
            uniform_viewPosition = GL.GetUniformLocation(programID, "viewPosition");
            uniform_lightPositionSpotlight = GL.GetUniformLocation(programID, "lightPositionSpotlight");
            uniform_lightDirectionSpotlight = GL.GetUniformLocation(programID, "lightDirectionSpotlight");
            uniform_lightInnerCutoffAngleSpotlight = GL.GetUniformLocation(programID, "lightInnerCutoffAngleSpotlight");
            uniform_lightOuterCutoffAngleSpotlight = GL.GetUniformLocation(programID, "lightOuterCutoffAngleSpotlight");
            uniform_lightColorSpotlight = GL.GetUniformLocation(programID, "lightColorSpotlight");
            uniform_lightIntensitySpotlight = GL.GetUniformLocation(programID, "lightIntensitySpotlight");
        }

        // loading shaders
        void Load(String filename, ShaderType type, int program, out int ID)
        {
            // source: http://neokabuto.blogspot.nl/2013/03/opentk-tutorial-2-drawing-triangle.html
            ID = GL.CreateShader(type);
            if (!OpenTKApp.isMac) GL.ObjectLabel(ObjectLabelIdentifier.Shader, ID, -1, filename);
            using (StreamReader sr = new StreamReader(filename)) GL.ShaderSource(ID, sr.ReadToEnd());
            GL.CompileShader(ID);
            GL.AttachShader(program, ID);
            string infoLog = GL.GetShaderInfoLog(ID);
            if (infoLog.Length != 0) Console.WriteLine(infoLog);
        }
    }
}
