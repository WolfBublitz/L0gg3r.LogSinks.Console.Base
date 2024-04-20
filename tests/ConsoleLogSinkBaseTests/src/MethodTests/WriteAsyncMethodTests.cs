using System.Threading.Tasks;
using L0gg3r;
using L0gg3r.Base;

namespace ConsoleLogSinkBaseTests.MethodTests.WriteAsyncMethodTests;

[TestClass]
public class TheWriteAsyncMethod
{
    [TestMethod]
    public async Task ShouldBeCalledForEachLogMessage()
    {
        // Arrange
        Mock<ILogger> loggerMock = new();
        TestConsoleLogSink testConsoleLogSink = new(loggerMock.Object);

        // Act
        await testConsoleLogSink.SubmitAsync(new LogMessage()).ConfigureAwait(false);
        await testConsoleLogSink.FlushAsync().ConfigureAwait(false);

        // Assert
        testConsoleLogSink.LogMessages.Should().HaveCount(1);
    }
}
