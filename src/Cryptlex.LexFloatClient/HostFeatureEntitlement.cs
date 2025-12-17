using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class HostFeatureEntitlement
    {
        /// <summary>
        /// The name of the feature
        /// </summary>
        public string FeatureName;

        /// <summary>
        /// The display name of the feature
        /// </summary>
        public string FeatureDisplayName;

        /// <summary>
        /// The value of the feature
        /// </summary>
        public string Value;

        /// <summary>
        /// The timestamp at which the license feature entitlement will expire.
        /// </summary>
        public long ExpiresAt;
    }
}