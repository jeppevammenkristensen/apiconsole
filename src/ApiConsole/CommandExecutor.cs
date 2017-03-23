using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Schema;

namespace ApiConsole
{
    internal class CommandExecutor
    {
        internal Dictionary<string, Type> Commands = new Dictionary<string, Type>(StringComparer.CurrentCultureIgnoreCase);

        internal void Execute(string[] args)
        {
            var commandType = ValidateAndExtractCommandType(args);
            var arguments = args.Skip(1).ToArray();

            var method = commandType.GetRuntimeMethods()
                .Where(x => !x.IsStatic && x.IsPublic)
                .FirstOrDefault(x => x.Name == "Execute");

            if (method == null)
                throw new InvalidOperationException($"Found command {commandType.FullName} but no Execute method was found");

            var callInstruction = new CallInstruction(commandType, arguments, method);
            callInstruction.Run();

        }

        private Type ValidateAndExtractCommandType(string[] args)
        {
            if (args.Length == 0)
            {
                throw new InvalidOperationException(
                    $"Expected at least one argument specifying the commands\n Candidates are {string.Join(",", this.Commands.Keys)}");
            }

            var commandName = args[0];
            if (!this.Commands.ContainsKey(commandName))
            {
                throw new InvalidOperationException(
                    $"A command with {commandName} was not found \n Candidates are {string.Join(",", this.Commands.Keys)}");
            }

            var commandType = Commands[commandName];
            return commandType;
        }
    }

}