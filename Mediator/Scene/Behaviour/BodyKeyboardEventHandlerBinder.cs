using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public class BodyKeyboardEventHandlerBinder
    {
        IKeyboardEventProvider keyboardEventProvider;
        Dictionary<List<Keys>, BodyKeyboardEventHandler> handlers;

        public BodyKeyboardEventHandlerBinder(IKeyboardEventProvider keyboardEventProvider, Dictionary<List<Keys>, BodyKeyboardEventHandler> handlers)
        {
            this.keyboardEventProvider = keyboardEventProvider;
            this.handlers = handlers;
        }

        public void Register(Body body)
        {
            foreach(List<Keys> keys in handlers.Keys)
            {
                BodyKeyboardEventHandler handler = handlers[keys];
                handler.Body = body;
                foreach (Keys key in keys)
                {
                    keyboardEventProvider.AddHandler(key, handler);
                }
            }
        }
    }
}
