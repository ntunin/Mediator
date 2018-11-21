using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class BodyMouseEventHandlerBinderBuilder : Builder
    {
        public BodyMouseEventHandlerBinderBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            IMouseEventProvider eventProvider = null;
            Dictionary<List<string>, BodyMouseEventHandler> handlers = null;
            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Instance", (object o)=>{ eventProvider = (IMouseEventProvider)DI.Get((string)o); } },
                {"Actions", (object o)=>{ handlers = ParseHandlers(o); } }
            });
            return new BodyMouseEventHandlerBinder(eventProvider, handlers);
        }

        private Dictionary<List<string>, BodyMouseEventHandler> ParseHandlers(object o)
        {
            List<object> descriptions = (List<object>)o;
            Dictionary<List<string>, BodyMouseEventHandler> result = new Dictionary<List<string>, BodyMouseEventHandler>();
            foreach (object descriptionObject in descriptions)
            {
                Dictionary<string, object> description = (Dictionary<string, object>)descriptionObject;
                List<string> eventNames = ParseEventNames(description);
                BodyMouseEventHandler handler = ParseHandler(description);
                result[eventNames] = handler;
            }
            return result;
        }

        private List<string> ParseEventNames(Dictionary<string, object> description)
        {
            List<object> eventDescriptions = (List<object>)description["Events"];
            List<string> result = new List<string>();
            foreach(object eventDescription in eventDescriptions)
            {
                string eventType = (string)eventDescription;
                result.Add(eventType);
            }
            return result;
        }

        private BodyMouseEventHandler ParseHandler(Dictionary<string, object> description)
        {
            string className = ((string)description["Control"]).Trim();
            return (BodyMouseEventHandler)Activator.CreateInstance(null, className).Unwrap();
        }
    }
}
