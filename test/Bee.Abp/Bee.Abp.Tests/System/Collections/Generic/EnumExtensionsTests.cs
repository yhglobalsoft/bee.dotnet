using System.ComponentModel;
using Shouldly;
using Xunit;

namespace System.Collections.Generic
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void ToDescriptionTest()
        {
            var value = TestEnum.EnumItemA;
            value.ToDescription().ShouldBe("枚举项A");

            value = TestEnum.EnumItemB;
            value.ToDescription().ShouldBe("EnumItemB");
        }

        [Fact]
        public void ExpandAndToStringTest()
        {
            var value = new List<string> {"A", "B", "C"};
            var result = value.ExpandAndToString(
                s=> $"'{s}'", ",");
            result.ShouldBe("'A','B','C'");
        }
        
        private enum TestEnum
        {
            [Description("枚举项A")]
            EnumItemA,
            EnumItemB
        }


    }
}