using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;

namespace Mediator
{
    public class BoundingPlane : BoundingShape
    {
        public Vector3 Normal;
        public Vector3 BaseW;
        public Vector3 BaseH;
        private float epsilon = 3;

        public override bool Collides(BoundingShape shape)
        {
            Vector3 nearest = shape.GetNearestPoint(this);
            Vector3 nearestProjection = getProgection(nearest);
            if(Math.Abs((nearest - nearestProjection).Length()) > epsilon)
            {
                return false;
            }
            Vector3 direction = shape.Position - nearestProjection;
            float distance = direction.Length();
            direction.Normalize();
            return false;
        }

        public override Vector3 GetCollisionDerection(BoundingShape shape)
        {
            return Normal;
        }

        public override Vector3 GetNearestPoint(BoundingShape shape)
        {
            return Position;
        }

        private Vector3 getProgection(Vector3 point)
        {
            float division = Normal.X * (point.X - Position.X) + Normal.Y * (point.Y - Position.Y) + Normal.Z * (point.Z - Position.Z);
            float divider = Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z;
            float a = division / divider;
            float x = a * Normal.X + Position.X;
            float y = a * Normal.Y + Position.Y;
            float z = a * Normal.Z + Position.Z;
            return new Vector3(x, y, z);
        }

        private Vector3 getIntersection(Vector3 point1, Vector3 direction1, Vector3 point2, Vector3 direction2)
        {
            float a2 = ((point2.Y - point1.Y) / direction1.Y - (point2.X - point1.X) / direction1.X) /
                       (direction2.X/direction1.X - direction2.Y/direction1.Y);
            float a = (point2.X - point1.X + a2 * direction2.X) / direction1.X;
            float aPx_a2P2x = a * direction1.X / (a2 * direction2.X);
            float aPy_a2P2z = a * direction1.Y / (a2 * direction2.Y);
            float aPz_a2P2z = a * direction1.Z / (a2 * direction2.Z);
            float x = (point1.X - aPx_a2P2x*point2.X)/(1 - aPx_a2P2x);
            float y = (point1.Y - aPy_a2P2z * point2.X) / (1 - aPy_a2P2z);
            float z = (point1.Z - aPz_a2P2z * point2.X) / (1 - aPz_a2P2z);
            return new Vector3(x, y, z);
        }

        private float getAngleBetween(Vector3 v1, Vector3 v2)
        {
            float mul = v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            return (float)Math.Acos(mul);
        }

        private Vector3 getEdgeInDirection(Vector3 direction, out Vector3 position)
        {
            position = new Vector3(0, 0, 0);
            return new Vector3(0, 0, 0);
            List<Vector3> array = new List<Vector3>() {
                BaseW, -BaseW,
                BaseH, -BaseH
            };
            int i = 0;
            int edgeIndex = -1;
            float minAngle = int.MaxValue;
            foreach(Vector3 basis in array)
            {
                float angle = getAngleBetween(basis, direction);
                if(minAngle > angle)
                {
                    minAngle = angle;
                    edgeIndex = i;
                }
                i++;
            }
        }
    }


    

}
