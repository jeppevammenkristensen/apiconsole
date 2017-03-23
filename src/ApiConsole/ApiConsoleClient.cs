using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiConsole
{
    public class ApiConsoleClient
    {
        public static IEnumerable<TResult> ExecuteMultiple<TResult>(string name, string path, bool debug, params string[] parameters)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName =
                    path,
                Arguments = $"{name}{ (!parameters.Any() ? "" : " " + string.Join(" ", parameters))}",
                UseShellExecute = false,
                RedirectStandardOutput = !debug, //true,
                CreateNoWindow = true,
            };
            var proc = new Process()
            {
                StartInfo = processStartInfo
            };

            proc.Start();

            if (debug)
                yield break;

            while (!proc.StandardOutput.EndOfStream)
            {
                var line = proc.StandardOutput.ReadLine();
                yield return JsonConvert.DeserializeObject<TResult>(line, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
        }

       
    }
}