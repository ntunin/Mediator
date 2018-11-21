using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    public class Body
    {
        public Vector3 Position = new Vector3();
        public Vector3 Rotation = new Vector3();
        public Vector3 Scale = new Vector3();
        public Matrix StartTransform = Matrix.Identity;
        public Matrix FinalTransform = Matrix.Identity;
        public List<Behaviour> Behaviour;

        public Body(Vector3 position, Vector3 rotation,  List<Behaviour> behaviour)
        {
            Position = position;
            Rotation = rotation;
            Behaviour = behaviour;
        }
    }
}
