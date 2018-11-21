using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    public class Scene
    {
        private List<Prefab> prefabs;
        private List<Light> light;
        private Camera camera;

        public Scene(Camera camera, List<Light> light, List<Prefab> prefabs)
        {
            this.prefabs = prefabs;
            this.camera = camera;
            this.light = light;
        }

        public void Prepare()
        {
            SetupCamera();
            SetupLight();
        }

        private void SetupCamera()
        {
            camera.Setup();
        }

        private void SetupLight()
        {
            Device device = (Device)DI.Get(DIConfigs.Device);
            device.RenderState.Lighting = true;
            for(int i = 0; i < device.Lights.Count; i++)
            {
                device.Lights[i].Enabled = false;
            } 
            int index = 0;
            foreach (Light l in light)
            {
                if (l.IsAvailableForIndex(index))
                {
                    l.Enable(index++);
                }
            }
        }

        public void Present(float deltaTime)
        {
            Device device = (Device)DI.Get(DIConfigs.Device);
            device.Transform.World = Matrix.Identity;
            foreach (Prefab prefab in prefabs)
            {
                prefab.Present(deltaTime);
            }
        }
    }
}
