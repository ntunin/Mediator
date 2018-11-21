using System;
using System.Collections.Generic;
using Microsoft.DirectX;

namespace Mediator
{
    class BodyBuilder : DirectXComponentBuilder
    {
        public BodyBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            Vector3 position = new Vector3();
            Vector3 rotation = new Vector3();
            List<Body> children = new List<Body>();
            List<Behaviour> behaviour = new List<Behaviour>();
            string name = null;
            BodyKeyboardEventHandlerBinder keyboardControlAction = null;
            BodyMouseEventHandlerBinder mouseControlAction = null;

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Position", (object o)=>{ position = ParseVector3(o);  } },
                {"Rotation", (object o)=>{ rotation = ParseAngleVector3(o);  } },
                {"Behaviour", (object o)=>{ behaviour = ParseBehaviour(o);  } },
                {"Children", (object o)=>{ children = ParseChildren((List<object>)o);  } },
                {"Name", (object o) => { name = (string)o; } },
                {"KeyboardEventProvider", (object o) => { keyboardControlAction = ParseKeyboardControlAction(o);  } },
                {"MouseEventProvider", (object o) => { mouseControlAction = ParseMouseControlAction(o);  } },
            });
            Body body = new Body(position, rotation, behaviour);
            if(name != null)
            {
                DI.Set(name, body);
            }
            if(keyboardControlAction != null)
            {
                keyboardControlAction.Register(body);
            } 
            if(mouseControlAction != null)
            {
                mouseControlAction.Register(body);
            }
            return body;
        }

        private List<Behaviour> ParseBehaviour(object namesDescription)
        {
            List<object> nameDescriptionList = (List<object>)namesDescription;
            List<Behaviour> behaviourList = new List<Behaviour>();
            foreach(object nameDescrionption in nameDescriptionList)
            {
                string name = (string)nameDescrionption;
                behaviourList.Add((Behaviour)DI.Get(name));
            }
            return behaviourList;
        }

        private List<Body> ParseChildren(List<object> childNames)
        {
            List<Body> children = new List<Body>();
            foreach(string name in childNames)
            {
                children.Add((Body)DI.Get(name));
            }
            return children;
        }

        private BodyKeyboardEventHandlerBinder ParseKeyboardControlAction(object o)
        {
            if(o is string)
            {
                return (BodyKeyboardEventHandlerBinder)DI.Get((string)o);
            }
            if(o is Dictionary<string, object>)
            {
                Dictionary<string, object> description = (Dictionary<string, object>)o;
                BodyKeyboardEventHandlerBinderBuilder builder = new BodyKeyboardEventHandlerBinderBuilder(description);
                return (BodyKeyboardEventHandlerBinder)builder.Create();
            }
            return null;
        }

        private BodyMouseEventHandlerBinder ParseMouseControlAction(object o)
        {
            if (o is string)
            {
                return (BodyMouseEventHandlerBinder)DI.Get((string)o);
            }
            if (o is Dictionary<string, object>)
            {
                Dictionary<string, object> description = (Dictionary<string, object>)o;
                BodyMouseEventHandlerBinderBuilder builder = new BodyMouseEventHandlerBinderBuilder(description);
                return (BodyMouseEventHandlerBinder)builder.Create();
            }
            return null;
        }
    }
}
