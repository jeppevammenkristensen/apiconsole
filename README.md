# Api Console
A console that can act as a simple api, that you can call from for instance asp.net core

You can see a simple example under sampes

## Server

Basically you register commands classes. The class should have an Execute method, that returns some kind of input (read it should return void)

```csharp
    public class SomeCommand 
    {
        IEnumerable<string> Execute(string input)
        {
            yield return "A";
            yield return "B";
        }
    }
```

To initialize this in a server you would do like so in the main of console appliction

```csharp
    class Program
    {
        static void Main(string[] args)
        {
            ApiConsoleServer.Init()
                .AddCommand<SomeCommand>()
                .Run(args);
        }
    }
```

## Client

From a client you could call it like this

```csharp
    IEnumerable<string> result = ApiConsoleClient.Execute<string>("SomeCommand");
```
