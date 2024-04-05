using System.Threading.Tasks;
using L0gg3r;

namespace ConsoleLogSinkBaseTests.MethodTests.AskAsync_TValue__question_converter_defaultAnswer_MethodTests;

[TestClass]
public class TheAskAsyncMethod
{
    [TestMethod]
    public async Task ShouldDisableTheLogger()
    {
        // Arrange
        TestConsoleLogSink testConsoleLogSink = new();

        // Act
        Task task = testConsoleLogSink.AskAsync("question", _ => 0, 0);
        await testConsoleLogSink.SubmitAsync(new LogMessage());
        testConsoleLogSink.TestConsole.Input.Add(42);
        await task.ConfigureAwait(false);
        await testConsoleLogSink.FlushAsync().ConfigureAwait(false);

        // Assert
        testConsoleLogSink.LogMessages.Should().BeEmpty();
    }
}
