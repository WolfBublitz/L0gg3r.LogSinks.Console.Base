using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using L0gg3r.Base;
using L0gg3r.LogSinks.Base;
using L0gg3r.LogSinks.Console.Base;

namespace ConsoleLogSinkBaseTests.InjectionTests;

internal class LogMessageWriter : ILogMessageWriter<object>
{
    [Inject]
    public ConsoleLogSinkBase<TestConsole>? ConsoleLogSinkBase { get; set; }

    [Inject]
    public TestConsole? TestConsole { get; set; }

    [Inject]
    public ILogger? Logger { get; set; }

    public ValueTask WriteAsync(in DateTimeOffset timestamp, LogLevel logLevel, IReadOnlyCollection<string> senders, object payload)
        => ValueTask.CompletedTask;
}

[TestClass]
public class TheConsoleLogSinkBase
{
    [TestMethod]
    public void ShouldInjectTheConsoleLogSinkBase()
    {
        // Arrange
        TestConsoleLogSink testConsoleLogSink = new();
        LogMessageWriter logMessageWriter = new();

        // Act
        testConsoleLogSink.RegisterLogMessageWriter(logMessageWriter);

        // Assert
        logMessageWriter.ConsoleLogSinkBase.Should().BeSameAs(testConsoleLogSink);
    }

    [TestMethod]
    public void ShouldInjectTheConsole()
    {
        // Arrange
        TestConsoleLogSink testConsoleLogSink = new();
        LogMessageWriter logMessageWriter = new();

        // Act
        testConsoleLogSink.RegisterLogMessageWriter(logMessageWriter);

        // Assert
        logMessageWriter.TestConsole.Should().BeSameAs(testConsoleLogSink.Console);
    }

    [TestMethod]
    public void ShouldInjectTheLogger()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();
        TestConsoleLogSink testConsoleLogSink = new()
        {
            Logger = loggerMock.Object,
        };
        LogMessageWriter logMessageWriter = new();

        // Act
        testConsoleLogSink.RegisterLogMessageWriter(logMessageWriter);

        // Assert
        logMessageWriter.Logger.Should().BeSameAs(loggerMock.Object);
    }
}
