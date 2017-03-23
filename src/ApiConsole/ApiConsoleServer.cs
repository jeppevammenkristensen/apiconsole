namespace ApiConsole
{
    public class ApiConsoleServer
    {
        private readonly CommandExecutor _commandRunner;

        private ApiConsoleServer()
        {
            _commandRunner = new CommandExecutor();
        }

        public static ApiConsoleServer Init()
        {
            var builder = new ApiConsoleServer();
            return builder;
        }

        public ApiConsoleServer AddCommand<T>(string name = null)
        {
            name = name ?? typeof(T).Name;
            _commandRunner.Commands.Add(name, typeof(T));
            return this;
        }

        public ApiConsoleServer Run(string[] args)
        {
            _commandRunner.Execute(args);
            return this;
        }


    }
}
