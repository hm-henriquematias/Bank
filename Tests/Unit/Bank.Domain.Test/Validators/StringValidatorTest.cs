using Bank.Business.Domain.Validators;
using Xunit;

namespace Bank.Test.Unit.Domain.Test.Validators
{
    public class StringValidatorTest
    {
        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("test", true)]
        public void TestString(string str, bool expected)
        {
            Assert.Equal(expected, StringValidator.ValidateString(str));
        }
    }
}
