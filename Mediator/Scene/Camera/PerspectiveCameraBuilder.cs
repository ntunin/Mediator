using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    class PerspectiveCameraBuilder : DirectXComponentBuilder
    {
        public PerspectiveCameraBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            float angle = (float)Math.PI/4;
            Control control = null;
            float near = 1;
            float far = 500;
            Vector3 position = new Vector3();
            Vector3 target = new Vector3();
            Vector3 up = new Vector3();

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Angle", (object o)=>{ angle = ParseAngle((string)o);  } },
                {"Control", (object o)=>{ control = (Control)DI.Get((string)o);  } },
                {"Near", (object o)=>{ near = float.Parse((string)o);  } },
                {"Far", (object o)=>{ far = float.Parse((string)o); } },
                {"Position", (object o)=>{ position = ParseVector3(o);  } },
                {"Target", (object o)=>{  target = ParseVector3(o);   } },
                {"Up", (object o)=>{  up = ParseVector3(o);   } }
            });

            return new PerspectiveCamera(angle, control, near, far, position, target, up);
        }      

        private float ParseMultipliers(string component)
        {
            string[] dividendComponentStrings = component.Split('*');
            float dividend = 1;
            foreach (string dividentComponentString in dividendComponentStrings)
            {
                float dividentComponent = ParseComponent(dividentComponentString);
                dividend *= dividentComponent;
            }
            return dividend;
        }

        private float ParseComponent(string component)
        {
            if(component.Contains("PI"))
            {
                return (float)Math.PI;
            }
            return float.Parse(component);
        }
    }
}
