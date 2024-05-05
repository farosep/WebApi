using api.Extensions;
using NUnit.Framework;

namespace tests.ParserTests
{
    public class ParserLiquidTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("qweqwe 10.201л asdsadsa asffddfh ", 10201)]
        [TestCase("фывфывыв 0.300л фывыфвыф фывфывфы", 300)]
        [TestCase("asdasdas 1.2л asdasdsad", 1200)]
        [TestCase("asdasd 5л asdasd asdsadas ", 5000)]
        [TestCase("asdasd 900мл asdasd asdsadas", 900)]
        [TestCase("asdasd 2900мл asdasd asdsadas", 2900)]
        [TestCase("asdasd 50мл asdasd asdsadas ", 50)]
        [TestCase("asdasd 12132фывмлфывсл asdasd asdsadas ", 0)]

        public void GetLiquid(string str, int result)
        {
            var (volume, modStr) = str.GetLiquid();

            Assert.That(volume == result, $"неверно распарсили {str}");
        }

        [Test]
        [TestCase("qweqwe 10.201кг asdsadsa asffddfh ", 10201)]
        [TestCase("фывфывыв 0.300кг фывыфвыф фывфывфы", 300)]
        [TestCase("asdasdas 1.2кг asdasdsad", 1200)]
        [TestCase("asdasd 5кг asdasd asdsadas ", 5000)]
        [TestCase("asdasd 900г asdasd asdsadas", 900)]
        [TestCase("asdasd 2900г asdasd asdsadas", 2900)]
        [TestCase("asdasd 50г asdasd asdsadas ", 50)]
        public void GetWeight(string str, int result)
        {
            var (volume, modStr) = str.GetWeight();

            Assert.That(volume == result, $"неверно распарсили {str}");
        }


        [Test]
        [TestCase("qweqwe 5шт asdsadsa asffddfh ", 5)]
        [TestCase("qweqwe 3шт*100г asdsadsa asffddfh ", 3)]
        [TestCase("qweqwe 100г asdsadsa asffddfh ", 0)]

        public void GetAmount(string str, int result)
        {
            var (volume, modStr) = str.GetAmount();

            Assert.That(volume == result, $"неверно распарсили {str}");
        }

        [Test]
        [TestCase("qweqwe 5% asdsadsa asffddfh ", "5%")]
        [TestCase("qweqwe 13-15% asdsadsa asffddfh ", "13-15%")]
        [TestCase("qweqwe 0.6% asdsadsa asffddfh ", "0.6%")]
        [TestCase("qweqwe adsdfsdg123sdsdf4323dsfsdf% asdsadsa asffddfh ", "")]

        public void GetPercent(string str, string result)
        {
            var (volume, modStr) = str.GetPercent();

            Assert.That(volume == result, $"неверно распарсили {str}");
        }

        [Test]
        [TestCase("qweqwe 10.201л asdsadsa asffddfh ", true)]
        [TestCase("фывфывыв 100мл фывыфвыф фывфывфы", true)]
        [TestCase("фывфывыв1231лфывыфвыф фывфывфы", true)]
        [TestCase("фывфывыв 1231фывфмл фывыфвыф фывфывфы", false)]

        public void IsLiquid(string str, bool result)
        {
            var volume = str.IsLiquid();

            Assert.That(volume == result, $"неверно определили есть ли объём в {str}");
        }

        [Test]
        [TestCase("qweqwe 10.201кг asdsadsa asffddfh ", true)]
        [TestCase("фывфывыв 100г фывыфвыф фывфывфы", true)]
        [TestCase("фывфывыв1231гфывыфвыф фывфывфы", true)]
        [TestCase("фывфывыв 1231фывфг фывыфвыф фывфывфы", false)]

        public void IsSolid(string str, bool result)
        {
            var volume = str.IsSolid();

            Assert.That(volume == result, $"неверно определили есть ли объём в {str}");
        }
    }
}