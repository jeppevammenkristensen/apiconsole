using ApiConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Commands;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiConsoleServer.Init()
                .AddCommand<IisSitesCommand>()
                .Run(args);
        }
    }
}
