namespace dotNetParadise_TDD.Test;

public class MathCalculatorTests
{
    //[Fact]
    //public void Add_TwoNumbers_ReturnSum()
    //{
    //    // Arrange
    //    var calculator = new MathCalculator();

    //    // Act
    //    var result = calculator.Add(3, 5);

    //    // Assert
    //    Assert.Equal(8, result);
    //}


    [Fact]
    public void DoubleNumber_WhenGivenSingleNumber_ReturnsDouble()
    {
        // Arrange
        var calculator = new MathCalculator();

        // Act
        var result = calculator.DoubleNumber(2);

        // Assert
        Assert.Equal(4, result);
    }
}
