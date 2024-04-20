using System.Threading.Tasks;
using L0gg3r;
using L0gg3r.Base;

namespace ConsoleLogSinkBaseTests.MethodTests.AskAsync_TValue__question_MethodTests;

[TestClass]
public class TheAskAsyncMethod
{
    [TestMethod]
    public async Task ShouldDisableTheLogger()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();
        TestConsoleLogSink testConsoleLogSink = new(loggerMock.Object);

        // Act
        Task task = testConsoleLogSink.AskAsync<int>("question");
        await Task.Delay(1000).ConfigureAwait(false);
        await testConsoleLogSink.SubmitAsync(new LogMessage());
        testConsoleLogSink.TestConsole.Input.Add(42);
        await task.ConfigureAwait(false);
        await testConsoleLogSink.FlushAsync().ConfigureAwait(false);

        // Assert
        testConsoleLogSink.LogMessages.Should().BeEmpty();
    }
}
