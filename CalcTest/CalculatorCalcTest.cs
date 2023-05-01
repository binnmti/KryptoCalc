namespace Calc;

[TestClass]
public class CalculatorCalcTest
{
    [TestMethod]
    [DataRow(new[] { "3", "+", "=", }, 6)]
    [DataRow(new[] { "1", "2", "+", "1", "2", "=", "1", "2", }, 12)]
    [DataRow(new[] { "1", "2", "3" }, 123)]
    [DataRow(new[] { "1", "2", "+" }, 12)]
    [DataRow(new[] { "1", "2", "+", "2", "1", "+" }, 33)]
    [DataRow(new[] { "1", "+", "2", "=" }, 3)]
    [DataRow(new[] { "1", "-", "2", "=" }, -1)]
    [DataRow(new[] { "2", "Å~", "3", "=" }, 6)]
    [DataRow(new[] { "6", "ÅÄ", "2", "=" }, 3)]
    [DataRow(new[] { "1", "2", "+", "2", "1" }, 21)]
    public void TestAdd(string[] input, double expected)
    {
        // Arrange
        var calc = new CalculatorCalc();

        // Act
        foreach (var str in input)
        {
            calc.Add(str);
        }

        // Assert
        Assert.AreEqual((decimal)expected, calc.CurrentNumber);
    }
}