using OpenTK.Mathematics;

namespace Template;

public class node
{
    public Matrix4 objectToParent { get; set; }
    public Mesh mesh { get; set; }
    //List<node> { }

    public void Render(Matrix4 cameraMatrix)
    {
        Matrix4 worldSpace = Matrix4.Mult(mesh.modelMatrix, objectToParent);
        Matrix4 modelView = Matrix4.Mult(worldSpace, cameraMatrix);
    }
}