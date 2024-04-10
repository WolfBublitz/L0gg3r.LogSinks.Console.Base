namespace ConsoleLogSinkBaseTests;

using System.Collections.Generic;
using System.Threading.Tasks;
using L0gg3r;
using L0gg3r.LogSinks.Console.Base;

internal sealed class TestConsoleLogSink : ConsoleLogSinkBase<TestConsole>
{
    public List<LogMessage> LogMessages { get; } = [];

    public TestConsole TestConsole => (TestConsole)Console;

    public TestConsoleLogSink()
        : base(new TestConsole())
    {
    }

    protected override ValueTask WriteAsync(in LogMessage logMessage, TestConsole console)
    {
        return ValueTask.CompletedTask;
    }
}
