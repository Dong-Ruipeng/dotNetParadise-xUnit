using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetParadise_Xunit.SampleTest;

public class MathCalculatorTests
{
    [Fact]
    public void Add_TwoNumbers_ReturnSum()
    {
        // Arrange
        var calculator = new MathCalculator();

        // Act
        var result = calculator.Add(3, 5);

        // Assert
        Assert.Equal(8, result);
    }


    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 4, 7)]
    public void Add_TwoNumbers_ReturnsSumofNumbers(int first, int second, int sum)
    {
        // Arrange
        var calculator = new MathCalculator();

        // Act
        var result = calculator.Add(first, second);

        // Assert
        Assert.Equal(sum, result);
    }


    public static IEnumerable<object[]> GetComplexTestData()
    {
        yield return new object[] { 10, 5, 15 }; // 测试数据 1
        yield return new object[] { -3, 7, 4 }; // 测试数据 2
        yield return new object[] { 0, 0, 0 }; // 测试数据 3
        // 可以根据需要继续添加更多的测试数据
    }

    [Theory]
    [MemberData(nameof(GetComplexTestData))]
    public void Add_TwoNumbers_ReturnsSumofNumbers01(int first, int second, int sum)
    {
        // Arrange
        var calculator = new MathCalculator();

        // Act
        var result = calculator.Add(first, second);

        // Assert
        Assert.Equal(sum, result);
    }

    [Theory]
    [CustomData(1, 2, 3)]
    [CustomData(2, 3, 5)]
    public void Add_TwoNumbers_ReturnSum03(int num1, int num2, int expectedSum)
    {
        // Arrange
        var calculator = new MathCalculator();

        // Act
        var result = calculator.Add(num1, num2);

        // Assert
        Assert.Equal(expectedSum, result);
    }


    [Fact]
    public void Add_TwoNumbers_ReturnsSumofNumbers02()
    {
        // Arrange
        var calculator = new MathCalculator();
        var testData = new List<(int, int, int)>
    {
        (1, 2, 3),
        (2, 3, 5),
        (3, 4, 7)
    };

        // Act & Assert
        foreach (var (first, second, sum) in testData)
        {
            var result = calculator.Add(first, second);
            Assert.Equal(sum, result);
        }
    }
}
