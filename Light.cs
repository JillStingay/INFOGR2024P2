using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Template
{
    public class Light
    {
        public Vector3 Position { get; set; }
        public Vector3 Color { get; set; }
        public float Intensity { get; set; }

        public Light(Vector3 position, Vector3 color, float intensity) {
            this.Position = position;
            this.Color = color;
            this.Intensity = intensity;
        }
    }

    public class Spotlight : Light
    {
        public float InnerAngle { get; set; }
        public float OuterAngle { get; set; }
        public Vector3 Direction { get; set; }
        public Spotlight(Vector3 position, Vector3 color, float intensity, float innerAngle, float outerAngle, Vector3 lightDirection) : base(position, color, intensity)
        {
            this.InnerAngle = innerAngle;
            this.OuterAngle = outerAngle;
            this.Direction = lightDirection;
        }
    }
}
