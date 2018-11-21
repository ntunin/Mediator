using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mediator
{
    public partial class Form1 : GameForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(ControlStyles controlStyles, Color cleanColor, string worldName, string sceneName) : base(controlStyles, cleanColor, worldName, sceneName)
        {
            InitializeComponent();
            DI.Set(DIConfigs.RenderControl, RenderPanel);
            StartGame();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Point p = Cursor.Position;
            label1.Text = $"{p.X} : {p.Y}";
        }
    }
}
