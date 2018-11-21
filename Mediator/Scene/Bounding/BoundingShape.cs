using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;

namespace Mediator
{
    public abstract class BoundingShape
    {
        public Vector3 Position;

        public abstract bool Collides(BoundingShape shape);
        public abstract Vector3 GetNearestPoint(BoundingShape shape);
        public abstract Vector3 GetCollisionDerection(BoundingShape shape);
    }
}
