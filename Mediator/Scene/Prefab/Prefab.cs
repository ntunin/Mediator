using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    public class Prefab
    {
        public Body Body;
        public Skin Skin;
        private Device device = null;
        private List<Prefab> children = new List<Prefab>();

        public Prefab(Body body, Skin skin, List<Prefab> children)
        {
            Skin = skin;
            Body = body;
            this.children = children;
            device = (Device)DI.Get(DIConfigs.Device);
        }

        public void Present(float deltaTime)
        {
            Matrix parentTransform = device.Transform.World;
            Matrix transform = parentTransform;
            if (Body != null)
            {
                if(Body.Behaviour != null)
                {
                    foreach(Behaviour bevaviour in Body.Behaviour)
                    {
                        bevaviour.Update(deltaTime, Body);
                    }
                }
                Matrix rotation = Matrix.RotationYawPitchRoll(Body.Rotation.Y, Body.Rotation.X, Body.Rotation.Z);
                Matrix translation = Matrix.Translation(Body.Position.X, Body.Position.Y, Body.Position.Z);
                transform = Body.StartTransform * rotation * translation * Body.FinalTransform;
                transform *= parentTransform;
            }

            if (children != null)
            {
                foreach (Prefab prefab in children)
                {
                    device.Transform.World = transform;
                    prefab.Present(deltaTime);
                }
            }

            if(Skin != null)
            {
                device.Transform.World = transform;
                Skin.Present();
            }
        }
    }
}
