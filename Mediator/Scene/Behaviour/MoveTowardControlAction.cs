using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;

namespace Mediator
{
    public class MoveTowardEventHandler : BodyKeyboardEventHandler
    {
        private float angle;
        private float step;

        public MoveTowardEventHandler(Dictionary<string, object> description)
        {
            description = (Dictionary<string, object>)new MoveTowardPropertiesBuilder(description).Create();

            angle = (float)description["Angle"];
            step = (float)description["Step"];
        }

        public override void HandleEvent(KeyEventArgs e)
        {

            float fi = (Shared.Content.ContainsKey("world_fi")) ? 
                (float)Shared.Content["world_fi"] : 0;
            //fi -= (float)Math.PI / 2;
            Vector3 direction = new Vector3((float)Math.Cos(fi + angle), 0, (float)Math.Sin(fi + angle));
            Body.Position.X += step* direction.X;
            Body.Position.Z += step* direction.Z;
        }

        protected class MoveTowardPropertiesBuilder : Builder
        {
            public MoveTowardPropertiesBuilder(Dictionary<string, object> configs) : base(configs)
            {
            }

            public override object Create()
            {
                Dictionary<string, object> properties = new Dictionary<string, object>();
                HandleActionMap(new Dictionary<string, Action<object>>
                {
                    { "Angle", (object o)=>{properties["Angle"] = ParseAngle((string)o); } },
                    { "Step", (object o)=>{properties["Step"] = float.Parse((string)o); } }
                });
                return properties;
            }
        }
    }
}
