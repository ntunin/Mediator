using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public abstract class BodyMouseEventHandler: IMouseEventHandler
    {
        public Body body;

        public abstract void HandleEvent(MouseEventArgs e);
    }
}
