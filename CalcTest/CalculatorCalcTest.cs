namespace KryptoCalc.Shared;

[TestClass]
public class CalculatorCalcTest
{
    [TestMethod]
    public void TestTest()
    {
        var nums = new List<int> { 1, 2, 2, 4, 6, 6, 6, 8, 9 };
        var result = Enumerable.Range(1, nums.Count).Where(x => nums.All(n => x != n));


        var c = CurrencyList.GetCurrentId();
    }

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
        Assert.AreEqual((decimal)expected, calc.CurrentInputNumber);
    }
}