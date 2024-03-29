﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class HostLicenseMeterAttribute
    {

        public string Name;

        public uint AllowedUses;

        public uint TotalUses;

        public uint GrossUses;

        public HostLicenseMeterAttribute(string name, uint allowedUses, uint totalUses, uint grossUses)
        {
            this.Name = name;
            this.AllowedUses = allowedUses;
            this.TotalUses = totalUses;
            this.GrossUses = grossUses;
        }

    }
}

