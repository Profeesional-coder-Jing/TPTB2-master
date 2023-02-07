using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPTB2.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string UsersEndpoint = $"{Prefix}/Users";
        public static readonly string BookingsEndpoint = $"{Prefix}/bookings";
        public static readonly string ReviewsEndpoint = $"{Prefix}/reviews";
        public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
        public static readonly string CountryEndpoint = $"{Prefix}/country";
        public static readonly string MakesEndpoint = $"{Prefix}/makes";
    }
}
