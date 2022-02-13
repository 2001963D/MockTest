using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockTest.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string TestsEndpoint = $"{Prefix}/tests";
        public static readonly string AccountsEndpoint = $"{Prefix}/accounts";

    }
}
