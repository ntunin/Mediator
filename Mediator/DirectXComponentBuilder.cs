using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    abstract class DirectXComponentBuilder : Builder
    {
        public DirectXComponentBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        protected Vector3 ParseVector3(object vectorObjectDescription)
        {
            Dictionary<string, object> vectorDescription = (Dictionary<string, object>)vectorObjectDescription;
            Vector3 vector = new Vector3();
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"X", (object o)=>{vector.X = float.Parse((string)o); } },
                {"Y", (object o)=>{vector.Y = float.Parse((string)o); } },
                {"Z", (object o)=>{vector.Z = float.Parse((string)o); } }
            }, vectorDescription);
            return vector;
        }

        protected Vector3 ParseAngleVector3(object vectorObjectDescription)
        {
            Dictionary<string, object> vectorDescription = (Dictionary<string, object>)vectorObjectDescription;
            Vector3 vector = new Vector3();
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"X", (object o)=>{vector.X = ParseAngle((string)o); } },
                {"Y", (object o)=>{vector.Y = ParseAngle((string)o); } },
                {"Z", (object o)=>{vector.Z = ParseAngle((string)o); } }
            }, vectorDescription);
            return vector;
        }
    }
}
