using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mediator
{
    class ColorBuilder : Builder
    {
        public ColorBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            return ParseColor(configs);
        }
    }
}
