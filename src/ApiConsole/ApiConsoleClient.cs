using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiConsole
{
    public class ApiConsoleClient
    {
        public static IEnumerable<TResult> Execute<TResult>(string name, string path, params string[] parameters)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName =
                    path,
                Arguments = $"{name}{ (!parameters.Any() ? "" : " " + string.Join(" ", parameters))}",
                UseShellExecute = false,
                RedirectStandardOutput = true, //true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };
            var proc = new Process()
            {
                StartInfo = processStartInfo
            };

            proc.Start();

            while (!proc.StandardOutput.EndOfStream)
            {
                var line = proc.StandardOutput.ReadLine();
                yield return JsonConvert.DeserializeObject<TResult>(line, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }

            if (!proc.StandardError.EndOfStream)
            {
                throw new InvalidOperationException($"An exception occurred: {proc.StandardError.ReadToEnd()}");
            }
        }

       
    }
}