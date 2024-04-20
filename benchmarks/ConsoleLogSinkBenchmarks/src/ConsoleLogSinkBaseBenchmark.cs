using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using ConsoleLogSinkBaseTests;
using L0gg3r;
using L0gg3r.Base;

namespace ConsoleLogSinkBaseBenchmarks
{
    [MemoryDiagnoser]
    public class ConsoleLogSinkBaseBenchmark
    {
        private TestConsoleLogSink testConsoleLogSink;
        private TestConsoleLogSink testConsoleLogSink2;

        [GlobalSetup]
        public void Setup()
        {
            testConsoleLogSink = new();
            testConsoleLogSink2 = new();
            testConsoleLogSink2.AddFilter(_ => true);
        }

        [Benchmark]
        public void Plain()
        {
            new LogMessage();
        }
    }
}
