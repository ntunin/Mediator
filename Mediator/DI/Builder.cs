using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace Mediator
{
    public abstract class Builder
    {
        protected Dictionary<string, object> configs;

        public Builder(Dictionary<string, object> configs)
        {
            this.configs = configs;
        }

        public abstract object Create();

        protected void HandleActionMap(Dictionary<string, Action<object>> actionMap)
        {
            foreach (string key in configs.Keys)
            {
                if (actionMap.ContainsKey(key))
                {
                    actionMap[key](configs[key]);
                }
            }
        }

        protected void HandleActionMap(Dictionary<string, Action<object>> actionMap, Dictionary<string, object> parseInfo)
        {
            foreach (string key in parseInfo.Keys)
            {
                if(actionMap.ContainsKey(key))
                {
                    actionMap[key](parseInfo[key]);
                }
            }
        }

        protected int ParseConfigInt(string key)
        {
            string value = (string)configs[key];
            return int.Parse(value);
        }


        protected float ParseConfigFloat(string key)
        {
            string value = (string)configs[key];
            return float.Parse(value);
        }

        protected Color ParseColor(object o)
        {
            if(o is string)
            {
                return ParseColor((string)o);
            }
            if(o is Dictionary<string, object>)
            {
                return ParseColor((Dictionary<string, object>)o);
            }
            return Color.Empty;
        }

        protected Color ParseColor(string colorName)
        {
            return Color.FromName(colorName);
        }

        protected Color ParseColor(Dictionary<string, object> colorDescription)
        {
            byte alpha = 0;
            byte red = 0;
            byte green = 0;
            byte blue = 0;
            string name = null;

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"A", (object o)=>{alpha=byte.Parse((string)o); } },
                {"R", (object o)=>{red=byte.Parse((string)o); } },
                {"G", (object o)=>{green=byte.Parse((string)o); } },
                {"B", (object o)=>{blue=byte.Parse((string)o); } },
                {"Name", (object o)=>{name=(string)o; } }
            }, colorDescription);

            if (name != null)
            {
                return Color.FromName(name);
            }

            return Color.FromArgb(alpha, red, green, blue);
        }



        protected float ParseAngle(string andleDescription)
        {
            DataTable dt = new DataTable();
            andleDescription = andleDescription.Replace("PI", $"{Math.PI}");
            andleDescription = andleDescription.Replace(",", ".");
            object result = dt.Compute(andleDescription, "");
            if (result is decimal)
            {
                return (float)(decimal)result;
            }
            else if (result is int)
            {
                return (int)result;
            }
            else
            {
                return (float)result;
            }
        }
    }
}
