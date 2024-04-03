using System.Reflection;
using Xunit.Sdk;

namespace dotNetParadise_Xunit.SampleTest;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CustomDataAttribute : DataAttribute
{

    private readonly int _first;
    private readonly int _second;
    private readonly int _sum;

    public CustomDataAttribute(int first, int second, int sum)
    {
        _first = first;
        _second = second;
        _sum = sum;
    }
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { _first, _second, _sum };
    }
}
