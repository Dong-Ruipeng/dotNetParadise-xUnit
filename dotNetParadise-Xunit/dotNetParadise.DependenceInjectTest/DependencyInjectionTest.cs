using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;

namespace dotNetParadise.DependencyInjection;

public class DependencyInjectionTest
{
    private readonly ITestOutputHelperAccessor _testOutputHelperAccessor;
    private readonly ILogger<DependencyInjectionTest> _logger;
    public DependencyInjectionTest(ITestOutputHelperAccessor testOutputHelperAccessor, ILogger<DependencyInjectionTest> logger)
    {
        _testOutputHelperAccessor = testOutputHelperAccessor;
        _logger = logger;
    }

    [Fact]
    public void TestOutPut_Console()
    {
        _testOutputHelperAccessor.Output?.WriteLine("测试ITestOutputHelperAccessor");
        Assert.True(true);
    }

    [Fact]
    public void Test()
    {
        _logger.LogDebug("LogDebug");
        _logger.LogInformation("LogInformation");
        _logger.LogError("LogError");
    }
}
