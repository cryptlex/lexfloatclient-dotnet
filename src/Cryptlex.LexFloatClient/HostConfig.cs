using System;
using System.Collections.Generic;
using System.Text;

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#endif

namespace Cryptlex
{
    public class HostConfig
    {
#if NETSTANDARD2_0
        [JsonConverter(typeof(LongToStringConverter))]
#endif
        public string maxOfflineLeaseDuration;
    }
}
