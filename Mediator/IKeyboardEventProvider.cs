using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public interface IKeyboardEventProvider
    {
        void AddHandler(Keys key, IKeyboardEventHandler handler);
        void RemoveHandler(Keys key, IKeyboardEventHandler handler);
    }
}
