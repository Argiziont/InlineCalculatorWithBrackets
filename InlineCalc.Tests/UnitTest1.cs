using System;
using Xunit;
using InlineCalc;
namespace InlineCalc.Tests
{
    public class InlineCalculatorWithBracketsTests
    {
        [Theory]
        [InlineData("(1+7/(5+54))*9-2", 8.08)]
        [InlineData("9*(8+45)*2/6", 159)]
        [InlineData("(49-8*49)*6+2/89+65", -1992.98)]
        [InlineData("45*(54+21-98)/145+98", 90.86)]

        public void CalculatorMustReturnExcpectedData(string input, double expected)
        {
            Assert.Equal(InlineCalculatorWithBrackets.StringCalculator(input), expected);
        }
    }
}
