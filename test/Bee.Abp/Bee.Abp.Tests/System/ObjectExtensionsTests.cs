using Shouldly;
using Xunit;

namespace System
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void CastToGenericTest()
        {
            // null
            ((object) null).CastTo<object>().ShouldBe(null);

            // int?
            ((object)null).CastTo<int?>().ShouldBe(null);

            // int <=> string
            "123".CastTo<int>().ShouldBe(123);
            123.CastTo<string>().ShouldBe("123");
            123.CastTo<int>().ShouldBe(123);

            // bool <=> string
            true.CastTo<string>().ShouldBe("True");
            "true".CastTo<bool>().ShouldBe(true);

            // Guid <=> string
            "56D768A3-3D74-43B4-BD7B-2871D675CC4B".CastTo<Guid>()
                .ShouldBe(new Guid("56D768A3-3D74-43B4-BD7B-2871D675CC4B"));

            // enum <=> string
            1.CastTo<UriKind>().ShouldBe(UriKind.Absolute);
            "RelativeOrAbsolute".CastTo<UriKind>().ShouldBe(UriKind.RelativeOrAbsolute);

            // default
            "abc".CastTo<int>(123).ShouldBe(123);
            ((object)"abc").CastTo<int>(123).ShouldBe(123);

            Should.Throw<FormatException>(() =>
                "abc".CastTo<int>()
            );
        }

        [Fact]
        public void CastToTest()
        {

            // null
            ((object)null).CastTo(typeof(object)).ShouldBe(null);

            // int?
            ((object)null).CastTo(typeof(int?)).ShouldBe(null);
            ("123").CastTo(typeof(int?)).ShouldBe(123);
            ((object)"123").CastTo(typeof(int?)).ShouldBe(123);

            // int <=> string
            "123".CastTo(typeof(int)).ShouldBe(123);
            "123".CastTo(typeof(long)).ShouldBe(123);
            123.CastTo(typeof(string)).ShouldBe("123");
            ((object)123).CastTo(typeof(string)).ShouldBe("123");

            // bool <=> string
            true.CastTo(typeof(string)).ShouldBe("True");
            "true".CastTo(typeof(bool)).ShouldBe(true);

            // Guid <=> string
            "56D768A3-3D74-43B4-BD7B-2871D675CC4B".CastTo(typeof(Guid))
                .ShouldBe(new Guid("56D768A3-3D74-43B4-BD7B-2871D675CC4B"));

            // enum <=> string
            1.CastTo(typeof(UriKind)).ShouldBe(UriKind.Absolute);
            "RelativeOrAbsolute".CastTo(typeof(UriKind)).ShouldBe(UriKind.RelativeOrAbsolute);

            Should.Throw<FormatException>(() =>
                "abc".CastTo(typeof(int))
            );

        }

        [Fact]
        public void IsBetweenTest()
        {
            const int num = 5;
            num.IsBetween(1, 10).ShouldBeTrue();
            num.IsBetween(5, 10).ShouldBeTrue();
            num.IsBetween(5, 10, true).ShouldBeTrue();
            num.IsBetween(0, 5, true, true).ShouldBeTrue();
            num.IsBetween(5, 5, true, true).ShouldBeTrue();

            num.IsBetween(5, 10, false).ShouldBeFalse();
            num.IsBetween(0, 5, true, false).ShouldBeFalse();
            num.IsBetween(5, 5, true, false).ShouldBeFalse();
            num.IsBetween(5, 5, false, true).ShouldBeFalse();
            num.IsBetween(5, 5, false, false).ShouldBeFalse();
        }

        [Fact]
        public void ToDynamicTest()
        {
            var obj1 = new {Name = "GMF"};
            var res1 = obj1.ToDynamic();
            Assert.True(res1.Name == "GMF");

            var obj2 = new {Name = "GMF", Value = new {IsLocked = true}};
            Assert.True(obj2.ToDynamic().Value.IsLocked);
        }

        [Fact]
        public void IsInTest()
        {
            1.IsIn(1,2,3).ShouldBeTrue();
            1.IsIn(2, 3, 4).ShouldBeFalse();
            2.IsIn(2, 2, 2).ShouldBeTrue();
            1.IsIn(2, 2, 2).ShouldBeFalse();

        }

        [Fact]
        public void ToJsonStringTest()
        {
            ((object)null).ToJsonString().ShouldBe("null");
            "".ToJsonString().ShouldBe("\"\"");
            123.ToJsonString().ShouldBe("123");

            var test = new
            {
                Id = 1,
                Name = "AkiniXu",
                IsAdmin = false,
                WorkDay = DayOfWeek.Monday
            };
            // {"Id":1,"Name":"AkiniXu","IsAdmin":false,"WorkDay":1}
            test.ToJsonString().ShouldBe("{\"Id\":1,\"Name\":\"AkiniXu\",\"IsAdmin\":false,\"WorkDay\":1}");
            test.ToJsonString(true).ShouldBe("{\"id\":1,\"name\":\"AkiniXu\",\"isAdmin\":false,\"workDay\":1}");
            // 因为GITLAB CI 是在LINUX下运行 单元测试会导致换行符不一致 不能直接判断
            var strJson = test.ToJsonString(true, true);
            strJson.Contains("\"id\": 1,").ShouldBeTrue();
            strJson.Contains("\"name\": \"AkiniXu\",").ShouldBeTrue();
            strJson.Contains("\"isAdmin\": false,").ShouldBeTrue();
            strJson.Contains("\"workDay\": 1").ShouldBeTrue();

            //.ShouldBe("{\r\n  \"id\": 1,\r\n  \"name\": \"AkiniXu\",\r\n  \"isAdmin\": false,\r\n  \"workDay\": 1\r\n}");

        }
    }
}