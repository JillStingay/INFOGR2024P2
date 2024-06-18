using System.Diagnostics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using INFOGR2024TemplateP2;

namespace Template
{
    class MyApplication
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        Mesh? teapot, floor;                    // meshes to draw using OpenGL
        float a = 0;                            // teapot rotation angle
        readonly Stopwatch timer = new();       // timer for measuring frame duration
        Shader? shader;                         // shader to use for rendering
        Shader? postproc;                       // shader to use for post processing
        Texture? wood;                          // texture to use for rendering
        RenderTarget? target;                   // intermediate render target
        ScreenQuad? quad;                       // screen filling quad for post processing
        readonly bool useRenderTarget = true;   // required for post processing

        Surface map;
        float[,] h;
        Camera camera;
        node world;

        // constructor
        public MyApplication(Surface screen)
        {
            this.screen = screen;
        }
        // initialize
        public void Init()
        {
            /*
            // load teapot
            teapot = new Mesh("../../../assets/teapot.obj");
            floor = new Mesh("../../../assets/floor.obj");
            // initialize stopwatch
            timer.Reset();
            timer.Start();
            // create shaders
            shader = new Shader("../../../shaders/vs.glsl", "../../../shaders/fs.glsl");
            postproc = new Shader("../../../shaders/vs_post.glsl", "../../../shaders/fs_post.glsl");
            // load a texture
            wood = new Texture("../../../assets/wood.jpg");
            // create the render target
            if (useRenderTarget) target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();

            /*map = new Surface("../../../assets/coin.png");
            h = new float[256, 256];
            for (int y = 0; y < 256; y++) for (int x = 0; x < 256; x++)
                    h[x, y] = ((float)(map.pixels[x + y * 256] & 255)) / 256;*/

            camera = new Camera(new Vector3(1, 1, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 0));
            world = new node(null, null);
            new node(world, teapot);
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
            //screen.Print("hello world", 2, 2, 0xffff00);
            a += 0.1f;
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // prepare matrix for vertex shader
            float angle90degrees = MathF.PI / 2;
            Matrix4 teapotObjectToWorld = Matrix4.CreateScale(0.5f) * Matrix4.CreateFromAxisAngle(new Vector3(3, 0, 1), a);
            Matrix4 floorObjectToWorld = Matrix4.CreateScale(4.0f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            Matrix4 worldToCamera = Matrix4.CreateTranslation(new Vector3(0, -14.5f, 0)) * Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), angle90degrees);
            Matrix4 cameraToScreen = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), (float)screen.width/screen.height, .1f, 1000);

            // update rotation
            //a += 0.001f * frameDuration;
            if (a > 2 * MathF.PI) a -= 2 * MathF.PI;

            if (useRenderTarget && target != null && quad != null)
            {
                // enable render target
                target.Bind();

                // render scene to render target
                if (shader != null && wood != null)
                {
                    teapot?.Render(shader, teapotObjectToWorld * worldToCamera * cameraToScreen, teapotObjectToWorld, wood);
                    //floor?.Render(shader, floorObjectToWorld * worldToCamera * cameraToScreen, floorObjectToWorld, wood);
                }

                // render quad
                target.Unbind();
                if (postproc != null)
                    quad.Render(postproc, target.GetTextureID());
            }
            else
            {
                // render scene directly to the screen
                if (shader != null && wood != null)
                {
                    teapot?.Render(shader, teapotObjectToWorld * worldToCamera * cameraToScreen, teapotObjectToWorld, wood);
                    floor?.Render(shader, floorObjectToWorld * worldToCamera * cameraToScreen, floorObjectToWorld, wood);
                }
            }
            
            /*
            Matrix4 M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);
            GL.LoadMatrix(ref M);
            GL.Translate(0, 0, -1);
            GL.Rotate(110, 1, 0, 0);
            GL.Rotate(a * 180 / Math.PI, 0, 0, 1);

            GL.Color3(1.0f, 0.0f, 0.0f);

            GL.Begin(PrimitiveType.Quads);
            for (float x = 0; x < 255; x++)
                for (float y = 0; y < 255; y++)
                {
                    GL.Vertex3((x / (256f / 2f)) - 1f, y / (256f / 2f) - 1f, h[(int)x, (int)y]);
                    GL.Vertex3((x / (256f / 2f)) - 1f + (2f/256f), y / (256f / 2f) - 1f, h[(int)x + 1, (int)y]);
                    GL.Vertex3((x / (256f / 2f)) - 1f, y / (256f / 2f) - 1f + (2f/256f), h[(int)x, (int)y + 1]);
                    GL.Vertex3((x / (256f / 2f)) - 1f + (2f / 256f), y / (256f / 2f) - 1f + (2f / 256f), h[(int)x + 1, (int)y + 1]);
                }
            GL.End();
            */
            /*
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(0, 0, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(-1, 1, 1);
            GL.End();
            */


        }
    }
}