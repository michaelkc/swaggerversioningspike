using System;
using System.Collections.Generic;
using System.Linq;

namespace Seges.CvrServices.Contract.Infrastructure
{
    internal class VersionsSupportedByTestAttribute : Attribute
    {
        public string[] Versions { get; }

        public VersionsSupportedByTestAttribute(params string[] versions)
        {
            Versions = versions;
        }
    }
}