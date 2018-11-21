using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;

namespace Mediator
{
    public class BodyRotationMouseEvent : BodyMouseEventHandler
    {
        private Point center;
        private float teta;
        private float fi;
        Point last;
        int width;
        int height;

        public BodyRotationMouseEvent()
        {
            Control form = (Control)DI.Get(DIConfigs.MainForm);
            width = form.Width;
            height = form.Height;
            center = new Point(form.Left + width/2, form.Top + height/2); 
            Cursor.Position = center;
            last = center;
        }

        public override void HandleEvent(MouseEventArgs e)
        {
            Point p = Cursor.Position;
            float deltaX = (float)((p.X - center.X) * Math.PI / 180.0);
            float deltaY = (float)((p.Y - center.Y) * Math.PI / 180.0);
            Cursor.Position = center;
            fi += deltaX;
            teta += deltaY;
            Vector3 dir = new Vector3((float)Math.Cos(fi), 0, (float)Math.Sin(fi));
            Matrix rotation = Matrix.RotationAxis(dir, teta) * Matrix.RotationY(fi);
            body.FinalTransform = rotation;
            Shared.Content["world_fi"] = fi; 
        }
    }
}
