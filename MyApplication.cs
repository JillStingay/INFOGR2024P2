using System.Diagnostics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using INFOGR2024TemplateP2;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.ES11;

namespace Template
{
    class MyApplication
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        Shader? shader;                         // shader to use for rendering
        Shader? postproc;                       // shader to use for post processing
        RenderTarget? target;                   // intermediate render target
        ScreenQuad? quad;                       // screen filling quad for post processing
        readonly bool useRenderTarget = true;   // required for post processing
        public Camera camera;
        node worldNode;

        // constructor
        public MyApplication(Surface screen)
        {
            this.screen = screen;
        }
        // initialize
        public void Init()
        {
            // load textures
            Texture wood = new Texture("../../../assets/wood.jpg");
            Texture coin = new Texture("../../../assets/coin.png");
            Texture yellow = new Texture("../../../assets/yellow.jpg");
            Texture metal = new Texture("../../../assets/metal.jpg");
            Texture grass = new Texture("../../../assets/grass.jpg");
            // load meshes
            Mesh teapot = new Mesh("../../../assets/teapot.obj", metal,Matrix4.CreateTranslation(new Vector3(3, 5.5f, 0)), Matrix4.CreateScale(2) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), (float)(Math.PI / 4)));
            Mesh banana = new Mesh("../../../assets/banana.obj", yellow, Matrix4.CreateTranslation(new Vector3(-3, 5.7f, 2)), Matrix4.CreateScale(0.5f) * Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), (float)(Math.PI / 2)) * Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), (float)(-Math.PI / 16)));
            Mesh floor = new Mesh("../../../assets/floor.obj", grass, Matrix4.CreateTranslation(new Vector3(0, 0, 0)), Matrix4.CreateScale(4));
            Mesh table = new Mesh("../../../assets/table.obj", wood, Matrix4.CreateTranslation(new Vector3(0, -8, 0)), Matrix4.CreateScale(0.03f));
            // create shaders
            shader = new Shader("../../../shaders/vs.glsl", "../../../shaders/fs.glsl");
            postproc = new Shader("../../../shaders/vs_post.glsl", "../../../shaders/fs_post.glsl");
            // create the render target
            if (useRenderTarget) target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();
            camera = new Camera(new Vector3(0, 0, 20), new Vector3(0, 1, 0), new Vector3(0, 0, -1), new Vector3(1, 0, 0));
            worldNode = new node(null, null);
            //add nodes with meshes to scene graph
            node floorNode = new node(worldNode, floor);
            node tableNode = new node(floorNode, table);
            node teapotNode = new node(tableNode, teapot);
            node bananaNode = new node(tableNode, banana);
            //add lights
            floorNode.AddLight(new Light(new Vector3(10, 10, -10), new Vector3(1, 1, 1), 0.3f));
            floorNode.AddLight(new Light(new Vector3(10, -5, 0), new Vector3(1, 1, 1), 0.4f));
            floorNode.AddLight(new Light(new Vector3(10, 5, 5), new Vector3(1, 1, 1), 0.2f));
            floorNode.AddLight(new Light(new Vector3(-10, 5, 0), new Vector3(1, 1, 1), 0.1f));
            floorNode.AddLight(new Spotlight(new Vector3(10, 10, -10), new Vector3(1, 0.1f, 0.1f), 1, 12.5f, 60.0f, new Vector3(0, -1, 0)));
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            worldNode.Render(camera.WorldToCamera(), Matrix4.Identity, screen, shader, camera.location);
        }
    }
}