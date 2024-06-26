﻿using OpenTK.Mathematics;

namespace Template;

public class node
{
    //public Matrix4 objectToParent { get; set; }
    public Mesh? mesh { get; set; }
    public List<node> children = new List<node>();
    public List<Light> lights = new List<Light>();
    public node? parent;

    public node(node? parent, Mesh? mesh)
    {
        this.parent = parent;
        this.mesh = mesh;
        if (parent != null)
            parent.children.Add(this);
    }

    public void AddLight(Light l)
    {
        this.lights.Add(l);
    }

    public void Render(Matrix4 worldToCamera, Matrix4 objectToParent, Surface screen, Shader shader, Vector3 cameraPosition)
    {
        Matrix4 parentToWorld = Matrix4.Identity;
        Matrix4 scaleAndRotationMatrix = Matrix4.Identity;
        if (mesh != null)
        {
            parentToWorld = mesh.parentToWorld;
            scaleAndRotationMatrix = mesh.scaleAndRotationMatrix;
        }
        Matrix4 objectToWorld = scaleAndRotationMatrix * objectToParent * parentToWorld;
        Matrix4 objectToCamera = objectToWorld * worldToCamera;
        Matrix4 cameraToScreen = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), (float)screen.width / screen.height, .1f, 1000);
        Matrix4 objectToScreen = objectToCamera * cameraToScreen;

        if (mesh != null)
            mesh.Render(shader, objectToScreen, objectToWorld, mesh.texture, this.lights, cameraPosition);

        foreach (node node in children)
        {
            node.Render(worldToCamera, objectToWorld, screen, shader, cameraPosition);
        }
    }
}