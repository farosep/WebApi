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
        public void GetLiquid()
        {
            string dotl = "qweqwe 10.201л asdsadsa asffddfh ";
            int dotlint = 10201;
            string nodotml = "asdasd 900мл asdasd asdsadas";
            int nodotmlint = 900;
            string dotmlbelow1 = "фывфывыв 0.300л фывыфвыф фывфывфы";
            int dotmlbelow1int = 300;
            string dotmlmore1 = "asdasdas 1.2л asdasdsad";
            int dotmlmore1int = 1200;

            var (a, strg1) = dotl.GetLiquid();
            var (b, strg2) = nodotml.GetLiquid();
            var (c, strg3) = dotmlbelow1.GetLiquid();
            var (d, strg4) = dotmlmore1.GetLiquid();

            Assert.That(a == dotlint, $"неверно распарсилось {dotl}");
            Assert.That(b == nodotmlint, $"неверно распарсили {nodotml}");
            Assert.That(c == dotmlbelow1int, $"неверно распарсили {dotmlbelow1}");
            Assert.That(d == dotmlmore1int, $"неверно распарсили {dotmlmore1}");
        }

    }
}