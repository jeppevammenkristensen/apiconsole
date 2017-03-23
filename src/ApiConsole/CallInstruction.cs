using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiConsole
{
    public class CallInstruction
    {
        public CallInstruction(Type command, IEnumerable<string> arguments, MethodInfo method)
        {
            Arguments = arguments.ToArray();
            Command = command;
            Method = method;
        }

        public MethodInfo Method { get; set; }

        public string[] Arguments { get; set; }
        public Type Command { get; set; }

        public void Run()
        {
            bool isCollection = Method.ReturnType != typeof(string) &&
                                typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(Method.ReturnType.GetTypeInfo());

            var instance = Activator.CreateInstance(Command);
            var result = Method.Invoke(instance, Arguments.Cast<object>().ToArray());
            if (isCollection)
            {
                RunCollection(result as IEnumerable);
            }
            else
            {
                RunItem(result);
            }
        }

        private void RunItem(object result)
        {
            Console.WriteLine(JsonConvert.SerializeObject(result, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }

        private void RunCollection(IEnumerable result)
        {
            foreach (var o in result)
            {
                RunItem(o);
            }
        }
    
    }
}