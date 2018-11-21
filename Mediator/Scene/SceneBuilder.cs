using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class SceneBuilder : Builder
    {
        public SceneBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            List<Prefab> prefabs = null;
            List<Light> lights = null;
            Camera camera = null;
            

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Camera", (object o)=>{ camera = (Camera)DI.Get((string)o);  } },
                {"Light", (object o)=>{ lights = ParseLights((List<object>)o);  } },
                {"Prefabs", (object o)=>{ prefabs = ParsePrefabs((List<object>)o);  } }
            });

            return new Scene(camera, lights, prefabs);
        }

        List<Prefab> ParsePrefabs(List<object> prefabDescriptions)
        {
            List<Prefab> prefabs = new List<Prefab>();
            foreach (object description in prefabDescriptions)
            {
                if(description is string)
                {
                    prefabs.Add((Prefab)DI.Get((string)description));
                } else if (description is Dictionary<string, object>)
                {
                    object prefab = new PrefabBuilder((Dictionary<string, object>)description).Create();
                    prefabs.Add((Prefab)prefab);
                }
            }
            return prefabs;
        }

        List<Light> ParseLights(List<object> lightsDescriptions)
        {
            List<Light> lights = new List<Light>();
            foreach(object lightDescription in lightsDescriptions)
            {
                string name = (string)lightDescription;
                Light light = (Light)DI.Get(name);
                lights.Add(light);
            }
            return lights;
        }
    }
}
