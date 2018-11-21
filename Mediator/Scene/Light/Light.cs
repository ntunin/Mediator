using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace Mediator
{
    public class Light
    {
        Dictionary<string, object> settings = new Dictionary<string, object>();

        public Light(Dictionary<string, object> settings)
        {
            this.settings = settings;
        }

        public void Enable(int index)
        {
            Device device = (Device)DI.Get(DIConfigs.Device);

            Dictionary<string, Action<object>> actionMap = new Dictionary<string, Action<object>>
            {
                {"Type", (object o)=>{device.Lights[index].Type = (LightType)o; } },
                {"Ambient", (object o)=>{device.Lights[index].Ambient = (Color)o; } },
                {"Diffuse", (object o)=>{device.Lights[index].Diffuse = (Color)o; } },
                {"Direction", (object o)=>{device.Lights[index].Direction = (Vector3)o; } },
                {"Position", (object o)=>{device.Lights[index].Position = (Vector3)o;  } },
                {"Attenuation", (object o)=>{device.Lights[index].Attenuation0 = (float)o;  } },
                {"Range", (object o)=>{device.Lights[index].Range = (float)o;  } },
            };
            foreach(string key in settings.Keys)
            {
                actionMap[key](settings[key]);
            }
            device.Lights[index].Enabled = true;
        }

        public bool IsAvailableForIndex(int index)
        {
            Device device = (Device)DI.Get(DIConfigs.Device);
            if(device.DeviceCaps.MaxActiveLights <= index)
            {
                return false;
            }
            bool available = true;
            VertexProcessingCaps caps = device.DeviceCaps.VertexProcessingCaps;
            new Dictionary<LightType, Action>
            {
                {LightType.Point, ()=>{available = caps.SupportsPositionalLights; } },
                {LightType.Spot, ()=>{available = caps.SupportsPositionalLights; } },
                {LightType.Directional, ()=>{available = caps.SupportsDirectionalLights; } },
            }[(LightType)settings["Type"]]();
            return available;
        }
        
    }
}
