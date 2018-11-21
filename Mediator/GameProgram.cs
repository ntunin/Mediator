using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediator;

namespace Mediator
{
    public class GameProgram : IProgram
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.Initialize(new XMLDI("di.xml"));

            GameForm form = (GameForm)DI.Get(DIConfigs.MainForm);
            form.InitializeGraphics();
            Application.Run(form);
        }
    }
}
