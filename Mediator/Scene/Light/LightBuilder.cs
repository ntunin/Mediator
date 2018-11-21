using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System.Drawing;

namespace Mediator
{
    class LightBuilder : DirectXComponentBuilder
    {
        public LightBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>();

            HandleActionMap(new Dictionary<string, Action<object>>()
            {
                {"Type", (object o)=>{settings["Type"]=ParseType((string)o); } },
                {"Ambient", (object o)=>{settings["Ambient"]=ParseColor(o); } },
                {"Diffuse", (object o)=>{settings["Diffuse"]=ParseColor(o); } },
                {"Direction", (object o)=>{settings["Direction"]=ParseVector3(o); } },
                {"Position", (object o)=>{settings["Position"]=ParseVector3(o); } },
                {"Attenuation", (object o)=>{settings["Attenuation"]=float.Parse((string)o); } },
                {"Range", (object o)=>{settings["Range"]=float.Parse((string)o); } },
            });
            return new Light(settings);
        }

        private LightType ParseType(string typeString)
        {
            return new Dictionary<string, LightType>()
            {
                {"Directional", LightType.Directional },
                {"Point", LightType.Point },
                {"Spot", LightType.Spot },
            }[typeString];
        }
    }
}
