using System.Threading.Tasks;
using L0gg3r;

namespace ConsoleLogSinkBaseTests.MethodTests.WriteAsyncMethodTests;

[TestClass]
public class TheWriteAsyncMethod
{
    [TestMethod]
    public async Task ShouldBeCalledForEachLogMessage()
    {
        // Arrange
        TestConsoleLogSink logSink = new();

        // Act
        await logSink.SubmitAsync(new LogMessage()).ConfigureAwait(false);
        await logSink.FlushAsync().ConfigureAwait(false);

        // Assert
        logSink.LogMessages.Should().HaveCount(1);
    }
}
