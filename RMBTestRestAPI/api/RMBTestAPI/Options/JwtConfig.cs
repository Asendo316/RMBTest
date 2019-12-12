using System;

namespace RMBTestRestAPI.Options
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}