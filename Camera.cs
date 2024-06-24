using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFOGR2024TemplateP2
{
    internal class Camera
    {
        public Vector3 location, upDirection, lookDirection, rightDirection;
        public Vector3 u, v, w; //normalized camera coordinate system, v points up, u points right, w points backwards

        public Camera(Vector3 location, Vector3 upDirection, Vector3 lookDirection, Vector3 rightDirection)
        {
            this.location = location;
            this.upDirection = upDirection;
            this.lookDirection = lookDirection;
            this.rightDirection = rightDirection;
            ConstructCoordinateSystem();
        }

        public void ConstructCoordinateSystem()
        {
            w = -lookDirection.Normalized();
            u = Vector3.Cross(upDirection, w).Normalized();
            v = Vector3.Cross(w, u).Normalized();
        }

        public Matrix4 WorldToCamera()
        {
            //Returns world to camera matrix
            Matrix4 translation = Matrix4.CreateTranslation(-location);
            Vector4 row0 = new Vector4(u.X, u.Y, u.Z, 0);
            Vector4 row1 = new Vector4(v.X, v.Y, v.Z, 0);
            Vector4 row2 = new Vector4(w.X, w.Y, w.Z, 0);
            Vector4 row3 = new Vector4(0, 0, 0, 1);
            Matrix4 rotation = new Matrix4(row0, row1, row2, row3);
            rotation.Transpose();
            return translation * rotation;
        }
    }
}
