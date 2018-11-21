using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;

namespace Mediator
{
    public class Skin
    {

        private Mesh mesh;
        private Material[] materials;
        private Texture[] textures;
        private Device device;

        public Skin(Mesh mesh, Material[] materials, Texture[] textures)
        {
            this.mesh = mesh;
            this.materials = materials;
            this.textures = textures;
            device = (Device)DI.Get(DIConfigs.Device);
        }

        public void Present()
        {
            for (int i = 0; i < materials.Length; i++)
            {
                device.Material = materials[i];
                device.SetTexture(0, textures[i]);
                mesh.DrawSubset(i);
            }
        }

    }
}
