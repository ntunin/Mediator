using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Mediator
{
    public class PrefabBuilder : Builder
    {
        public PrefabBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            Body body = null;
            Skin skin = null;
            List<Prefab> children = null;
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Body", (object o)=>{ body = ParseBody(o);  } },
                {"Skin", (object o)=>{ skin = (Skin)DI.Get((string)o);  } },
                {"Children", (object o)=>{ children = ParseChildren(o);  } }
            });
            return new Prefab(body, skin, children);
        }

        private Body ParseBody(object o)
        {
            if(o is string)
            {
                return (Body)DI.Get((string)o);
            } else if(o is Dictionary<string, object>)
            {
                Dictionary<string, object> data = (Dictionary<string, object>)o;
                object body = new BodyBuilder(data).Create();
                return (Body)body;
            }
            return null;
        }

        private List<Prefab> ParseChildren(object objectsDescription)
        {
            List<object> objectsDescriptions = (List<object>)objectsDescription;
            List<Prefab> result = new List<Prefab>();
            foreach(object objectDecription in objectsDescriptions)
            {
                if(objectDecription is string)
                {
                    string description = (string)objectDecription;
                    result.Add((Prefab)DI.Get(description));
                } else if (objectDecription is Dictionary<string, object>)
                {
                    Dictionary<string, object> description = (Dictionary<string, object>)objectDecription;
                    Prefab prefab = (Prefab)new PrefabBuilder(description).Create();
                    result.Add(prefab);
                }
            }
            return result;
        }
    }
}
