using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediator
{
    public interface IKeyboardEventHandler
    {
        void HandleEvent(KeyEventArgs e);
    }
}
