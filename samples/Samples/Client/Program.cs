using System;
using System.Diagnostics;
using System.IO;
using ApiConsole;
using Server.Commands;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is specific to my dev environment. In a perfect world this would be more "intelligent"
            var path = Path.Combine(@"C:\Code\dotnetcore\apiconsole\samples\Samples\Server\bin\Debug","server");
            
            foreach (var iisSite in ApiConsoleClient.Execute<IisSite>(name: "IisSitesCommand",path: path))
            {
                Console.WriteLine($"Id:{iisSite.Id} Name: {iisSite.Name}");
            }

            Console.ReadLine();
        }
    }
}