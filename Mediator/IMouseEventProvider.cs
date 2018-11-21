using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public interface IMouseEventProvider
    {
        void AddAction(string key, IMouseEventHandler handler);
        void RemoveAction(string key, IMouseEventHandler handler);
    }
}
