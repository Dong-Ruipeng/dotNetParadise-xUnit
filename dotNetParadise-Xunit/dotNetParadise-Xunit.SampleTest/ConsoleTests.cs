using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace dotNetParadise_Xunit.SampleTest;

public class ConsoleTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Console_ShouldBe_Output()
    {
        Console.WriteLine("hi 测试Console.WriteLine");
        testOutputHelper.WriteLine("hello world");
        Assert.True(true);
    }
}
