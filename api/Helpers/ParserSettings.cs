using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class ParserSettings
    {
        public static int PageReloads = 5;

        public static int CountOfOpeningPages = 10;

        public static TimeSpan WebdriverWait = new TimeSpan(0, 1, 30);

    }
}