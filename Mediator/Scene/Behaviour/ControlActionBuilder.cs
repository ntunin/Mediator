using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class ControlActionBuilder : Builder
    {
        public ControlActionBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            float step = 1;
            string control = "Default";
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Step", (object o)=>{step = float.Parse((string)o); } },
                {"Control", (object o)=>{control = (string)o; } }
            });
            Action<Body> action = new Dictionary<string, Action<Body>>
            {
                {"position.x", (Body body)=>{ body.Position.X += step; } },
                {"position.y", (Body body)=>{ body.Position.Y += step; } },
                {"position.z", (Body body)=>{ body.Position.Z += step; } },
                {"rotation.x", (Body body)=>{ body.Rotation.X += step; } },
                {"rotationyx", (Body body)=>{ body.Rotation.Y += step; } },
                {"rotation.z", (Body body)=>{ body.Rotation.Z += step; } },
                {"default", null },
            }[control.ToLower().Trim()];
            return action;
        }
    }
}
