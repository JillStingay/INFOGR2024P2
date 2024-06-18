using OpenTK.Mathematics;

namespace Template;

public class node
{
    //public Matrix4 objectToParent { get; set; }
    public Mesh mesh { get; set; }
    public List<node> children;

    public void Render(Matrix4 worldToCamera, Matrix4 objectToParent)
    {
        Matrix4 objectToWorld = objectToParent * mesh.parentToWorld;
        Matrix4 objectToCamera = objectToWorld * worldToCamera;
        Matrix4 cameraToScreen = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), (float)screen.width / screen.height, .1f, 1000);
        Matrix4 objectToScreen = objectToCamera * cameraToScreen;

        mesh.Render(shader, objectToScreen, objectToWorld, texture);

        foreach (node node in children)
        {
            node.Render(worldToCamera, mesh.parentToWorld);
        }
    }
}