using FluentAssertions;

namespace KryptoCalc.Shared;

[TestClass]
public class CalculatorCalcTest
{
    [TestMethod]
    [DataRow(new[] { "1", "+", "×", "2", "=", "±" }, -2)]
    [DataRow(new[] { "1", "+", "×", "2", "=" }, 2)]
    [DataRow(new[] { "1", "+", "+", "2", "=" }, 3)]
    [DataRow(new[] { "1", "+", "+", "2", "=", "=" }, 3)]
    [DataRow(new[] { "1", "0", "±", }, -10)]
    [DataRow(new[] { "1", "0", "±", "+", "2", "=", }, -8)]
    [DataRow(new[] { "1", "0", "±", "±", "+", "2", "=", }, 12)]
    [DataRow(new[] { "1", "±", "0", "+", "2", }, 2)]
    [DataRow(new[] { "1", "0", "☒", }, 1)]
    [DataRow(new[] { "3", "+", "=", }, 6)]
    [DataRow(new[] { "1", "2", "+", "1", "2", "=", "1", "2", }, 12)]
    [DataRow(new[] { "1", "2", "3" }, 123)]
    [DataRow(new[] { "1", "2", "+" }, 12)]
    [DataRow(new[] { "1", "2", "+", "2", "1", "+" }, 33)]
    [DataRow(new[] { "1", "+", "2", "=" }, 3)]
    [DataRow(new[] { "1", "-", "2", "=" }, -1)]
    [DataRow(new[] { "2", "×", "3", "=" }, 6)]
    [DataRow(new[] { "6", "÷", "2", "=" }, 3)]
    [DataRow(new[] { "1", "2", "+", "2", "1" }, 21)]
    public void TestAdd(string[] input, double expected)
    {
        // Arrange
        var calc = new Calc();

        // Act
        foreach (var str in input)
        {
            calc.Input(str);
        }

        // Assert
        ((decimal)expected).Should().Be(calc.CurrentInputNumber);
    }

    [TestMethod]
    public void Test()
    {
        var rate1 = 29724.0000m / 4266701.0000m;
        var rate2 = 4266701.0000m / 29724.0000m;
        var j = 100m * rate1;
        var j1 = Math.Round(j, 4);
        var j2 = j * rate2;
        var j3 = Math.Round(j2, 4);
        j3.Should().Be(100.0000m);
    }
}