using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class HostProductVersionFeatureFlag
    {

        public string Name;

        public bool Enabled;

        public string Data;

        public HostProductVersionFeatureFlag(string name, bool enabled, string data)
        {
            this.Name = name;
            this.Enabled = enabled;
            this.Data = data;
        }

    }
}