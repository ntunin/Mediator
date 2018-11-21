using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        static IProgram program;

        [STAThread]
        static void Main()
        {
            program = new GameProgram();
            program.Run();
        }
    }
}
