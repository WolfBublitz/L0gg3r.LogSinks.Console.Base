using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using L0gg3r.LogSinks.Console.Base;

namespace ConsoleLogSinkBaseTests;

internal sealed class TestConsole : IConsole
{
    public BlockingCollection<object> Input { get; } = [];

    public StringBuilder Output { get; } = new();

    public TValue Ask<TValue>(string question, Func<string, TValue> converter, TValue defaultAnswer)
    {
        return (TValue)Input.Take();
    }

    public bool Confirm(string question, bool defaultAnswer)
    {
        return (bool)Input.Take();
    }

    public void Write(string message) => Output.Append(message);

    public void WriteLine(string message) => Output.AppendLine(message);

    public void WriteLine() => Output.AppendLine();
}
