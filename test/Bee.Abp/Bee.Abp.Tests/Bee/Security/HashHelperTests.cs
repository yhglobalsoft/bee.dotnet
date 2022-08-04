using Shouldly;

namespace Bee.Security
{
    public class HashHelperTests
    {

        [Fact]
        public void GetSha256Test()
        {
            const string value = "admin";
            const string actual = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            HashHelper.GetSha256(value).ShouldBe(actual);
        }

        [Fact]
        public void GetSha512Test()
        {
            const string value = "admin";
            const string actual = "c7ad44cbad762a5da0a452f9e854fdc1e0e7a52a38015f23f3eab1d80b931dd472634" +
                                  "dfac71cd34ebc35d16ab7fb8a90c81f975113d6c7538dc69dd8de9077ec";
            HashHelper.GetSha512(value).ShouldBe(actual);
        }
    }
}