using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class BodyMouseEventHandlerBinder
    {
        IMouseEventProvider mouseEventProvider;
        Dictionary<List<string>, BodyMouseEventHandler> handlers;

        public BodyMouseEventHandlerBinder(IMouseEventProvider mouseEventProvider, Dictionary<List<string>, BodyMouseEventHandler> handlers)
        {
            this.mouseEventProvider = mouseEventProvider;
            this.handlers = handlers;
        }

        public void Register(Body body)
        {
            foreach (List<string> keys in handlers.Keys)
            {
                BodyMouseEventHandler action = handlers[keys];
                foreach (string key in keys)
                {
                    action.body = body;
                    mouseEventProvider.AddAction(key, action);
                }
            }
        }
    }
}
