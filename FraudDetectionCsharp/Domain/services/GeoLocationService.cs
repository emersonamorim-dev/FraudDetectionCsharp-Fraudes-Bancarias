using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FraudDetectionCsharp.Domain.services
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly List<IPRange> _ipRanges;

        public GeoLocationService()
        {
            // Lista real de intervalos de IP
            _ipRanges = new List<IPRange>
            {
                // Intervalos para o Brasil
                new IPRange { StartIP = IPAddress.Parse("45.224.0.0"), EndIP = IPAddress.Parse("45.239.255.255"), Country = "Brazil" },
                new IPRange { StartIP = IPAddress.Parse("143.0.0.0"), EndIP = IPAddress.Parse("143.255.255.255"), Country = "Brazil" },
                
                // Intervalos para os EUA
                new IPRange { StartIP = IPAddress.Parse("3.0.0.0"), EndIP = IPAddress.Parse("3.255.255.255"), Country = "USA" },
                new IPRange { StartIP = IPAddress.Parse("4.0.0.0"), EndIP = IPAddress.Parse("4.255.255.255"), Country = "USA" },
                
                // Intervalos para a Austrália
                new IPRange { StartIP = IPAddress.Parse("1.0.0.0"), EndIP = IPAddress.Parse("1.255.255.255"), Country = "Australia" },
                new IPRange { StartIP = IPAddress.Parse("14.0.0.0"), EndIP = IPAddress.Parse("14.255.255.255"), Country = "Australia" },
                
            };
        }

        public async Task<Location> GetLocationFromIPAsync(string ipAddress)
        {
            string country = GetCountryFromIP(ipAddress);
            return await Task.FromResult(new Location { Country = country, IPAddress = ipAddress });
        }

        private string GetCountryFromIP(string ipAddress)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            foreach (var range in _ipRanges)
            {
                if (IsIPInRange(ip, range))
                {
                    return range.Country;
                }
            }
            return "Unknown";
        }

        private bool IsIPInRange(IPAddress ip, IPRange range)
        {
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] startBytes = range.StartIP.GetAddressBytes();
            byte[] endBytes = range.EndIP.GetAddressBytes();

            bool isInRange = true;

            for (int i = 0; i < ipBytes.Length && isInRange; i++)
            {
                if (ipBytes[i] < startBytes[i] || ipBytes[i] > endBytes[i])
                {
                    isInRange = false;
                }
            }

            return isInRange;
        }

        private class IPRange
        {
            public IPAddress StartIP { get; set; }
            public IPAddress EndIP { get; set; }
            public string Country { get; set; }
        }
    }
}

