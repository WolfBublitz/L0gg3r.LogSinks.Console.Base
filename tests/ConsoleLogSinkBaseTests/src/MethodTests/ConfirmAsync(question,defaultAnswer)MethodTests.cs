using System.Threading.Tasks;
using L0gg3r;

namespace ConsoleLogSinkBaseTests.MethodTests.ConfirmAsync__question_defaultAnswer_MethodTests;

[TestClass]
public class TheConfirmAsyncMethod
{
    [TestMethod]
    public async Task ShouldDisableTheLogger()
    {
        // Arrange
        TestConsoleLogSink testConsoleLogSink = new();

        // Act
        Task task = testConsoleLogSink.ConfirmAsync("Are you sure?", true);
        await Task.Delay(1000).ConfigureAwait(false);
        await testConsoleLogSink.SubmitAsync(new LogMessage());
        testConsoleLogSink.TestConsole.Input.Add(true);
        await task.ConfigureAwait(false);
        await testConsoleLogSink.FlushAsync().ConfigureAwait(false);

        // Assert
        testConsoleLogSink.LogMessages.Should().BeEmpty();
    }
}