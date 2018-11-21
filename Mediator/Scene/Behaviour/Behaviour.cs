using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public abstract class Behaviour
    {
        public abstract void Update(float deltaTime, Body body);
    }
}
