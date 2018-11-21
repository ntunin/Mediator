using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Mediator
{
    class GameFormBuilder : Builder
    {
        public GameFormBuilder(Dictionary<string, object> configs) : base(configs)
        {
        }

        public override object Create()
        {
            ControlStyles styles = 0;
            string className = "GameForm";
            string assembly = "Mediator";
            Color cleanColor = Color.Azure;
            string worldName = null;
            string sceneName = null;

            HandleActionMap(new Dictionary<string, Action<object>>
            {
                {"Class", (object o)=>{ className = (string)o;  } },
                {"Assembly", (object o)=>{ assembly = (string)o;  } },
                {"ControlStyles", (object o)=>{ styles = ParseControlStyles((string)o);  } },
                {"CleanColor", (object o)=>{ cleanColor = ParseColor(o);  } },
                {"World", (object o)=>{ worldName = (string)o;  } },
                {"Scene", (object o)=>{ sceneName = (string)o;  } }
            });

            return Activator.CreateInstance(
                assembly,
                className,
                false, 0, null,
                new object[] { styles, cleanColor, worldName, sceneName },
                null, null).Unwrap();
        }

        private ControlStyles ParseControlStyles(string stylesString)
        {
            ControlStyles styles = 0;
            string[] styleStrings = stylesString.Split(',');
            foreach(string styleString in styleStrings)
            {
                styles |= ParseControlStyle(styleString.Trim());
            }
            return styles;
        }

        private ControlStyles ParseControlStyle(string styleString)
        {
            return new Dictionary<string, ControlStyles>
            {
                {"AllPaintingInWmPaint", ControlStyles.AllPaintingInWmPaint },
                {"CacheText", ControlStyles.CacheText },
                {"ContainerControl", ControlStyles.ContainerControl },
                {"EnableNotifyMessage", ControlStyles.EnableNotifyMessage },
                {"FixedHeight", ControlStyles.FixedHeight },
                {"FixedWidth", ControlStyles.FixedWidth },
                {"Opaque", ControlStyles.Opaque },
                {"OptimizedDoubleBuffer", ControlStyles.OptimizedDoubleBuffer },
                {"ResizeRedraw", ControlStyles.ResizeRedraw },
                {"Selectable", ControlStyles.Selectable },
                {"StandardClick", ControlStyles.StandardClick },
                {"StandardDoubleClick", ControlStyles.StandardDoubleClick },
                {"SupportsTransparentBackColor", ControlStyles.SupportsTransparentBackColor },
                {"UserMouse", ControlStyles.UserMouse },
                {"UserPaint", ControlStyles.UserPaint },
                {"UseTextForAccessibility", ControlStyles.UseTextForAccessibility }
            }[styleString];
        }


    }
}
