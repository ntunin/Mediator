using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public abstract class BodyKeyboardEventHandler : IKeyboardEventHandler
    {
        public Body Body;
        public abstract void HandleEvent(KeyEventArgs e);
    }
}
