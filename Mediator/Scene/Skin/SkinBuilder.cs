using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Windows.Forms;

namespace Mediator
{
    public class SkinBuilder : Builder
    {
        public SkinBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            string fileName = null;
            MeshFlags meshFlags = 0;
            ExtendedMaterial[] mtrl;
            Material[] materials = null;
            Texture[] textures = null;
            string texturesPath = null;

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"File", (object o)=>{ fileName = (string)o;  } },
                {"MeshFlags", (object o)=>{ meshFlags = ParseMeshFlags((string)o);  } },
                {"TexturesPath", (object o)=>{ texturesPath = (string)o;  } }
            });
            Device device = (Device)DI.Get(DIConfigs.Device);
            Mesh mesh = Mesh.FromFile(fileName, meshFlags, device, out mtrl);
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if ((mtrl != null) && mtrl.Length > 0)
            {
                materials = new Material[mtrl.Length];
                textures = new Texture[mtrl.Length];
                for (int i = 0; i < mtrl.Length; i++)
                {
                    materials[i] = mtrl[i].Material3D;
                    if(mtrl[i].TextureFilename!= null && mtrl[i].TextureFilename != String.Empty)
                    {
                        string texturefileName = mtrl[i].TextureFilename;
                        if(texturesPath != null)
                        {
                            texturefileName = texturesPath + @"\" + texturefileName;
                        }
                        textures[i] = TextureLoader.FromFile(device, texturefileName);
                    }
                }
            }
                

            return new Skin(mesh, materials, textures);
        }

        private MeshFlags ParseMeshFlags(string flagsString)
        {
            MeshFlags flags = 0;
            string[] flagStrings = flagsString.Split(',');
            foreach(string flagString in flagStrings)
            {
                MeshFlags flag = ParseMeshFlag(flagsString.Trim());
                flags |= flag;
            }
            return flags;
        }

        private MeshFlags ParseMeshFlag(string flagString)
        {
            return new Dictionary<string, MeshFlags>
            {
                {"DoNotClip", MeshFlags.DoNotClip },
                {"Dynamic", MeshFlags.Dynamic },
                {"IbDynamic", MeshFlags.IbDynamic },
                {"IbManaged", MeshFlags.IbManaged },
                {"IbSoftwareProcessing", MeshFlags.IbSoftwareProcessing },
                {"IbSystemMem", MeshFlags.IbSystemMem },
                {"IbWriteOnly", MeshFlags.IbWriteOnly },
                {"Managed", MeshFlags.Managed },
                {"NPatches", MeshFlags.NPatches },
                {"OptimizeAttributeSort", MeshFlags.OptimizeAttributeSort },
                {"OptimizeCompact", MeshFlags.OptimizeCompact },
                {"OptimizeDeviceIndependent", MeshFlags.OptimizeDeviceIndependent },
                {"OptimizeDoNotSplit", MeshFlags.OptimizeDoNotSplit },
                {"OptimizeIgnoreVerts", MeshFlags.OptimizeIgnoreVerts },
                {"OptimizeStripeReorder", MeshFlags.OptimizeStripeReorder },
                {"OptimizeVertexCache", MeshFlags.OptimizeVertexCache },
                {"Points", MeshFlags.Points },
                {"RtPatches", MeshFlags.RtPatches },
                {"SimplifyFace", MeshFlags.SimplifyFace },
                {"SimplifyVertex", MeshFlags.SimplifyVertex },
                {"SoftwareProcessing", MeshFlags.SoftwareProcessing },
                {"SystemMemory", MeshFlags.SystemMemory },
                {"Use32Bit", MeshFlags.Use32Bit },
                {"UseHardwareOnly", MeshFlags.UseHardwareOnly },
                {"VbDynamic", MeshFlags.VbDynamic },
                {"VbManaged", MeshFlags.VbManaged },
                {"VbShare", MeshFlags.VbShare },
                {"VbSoftwareProcessing", MeshFlags.VbSoftwareProcessing },
                {"VbSystemMem", MeshFlags.VbSystemMem },
                {"VbWriteOnly", MeshFlags.VbWriteOnly },
                {"WriteOnly", MeshFlags.WriteOnly }
            }[flagString];
        }
    }
}
