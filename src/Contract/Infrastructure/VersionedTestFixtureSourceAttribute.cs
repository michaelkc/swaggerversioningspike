using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Seges.CvrServices.Contract.Infrastructure
{
    public class VersionedTestFixtureSourceAttribute : TestFixtureSourceAttribute
    {
        public VersionedTestFixtureSourceAttribute(string sourceName) : base(sourceName)
        {
        }

        public VersionedTestFixtureSourceAttribute(Type sourceType, string sourceName) : base(sourceType, sourceName)
        {
        }

        public VersionedTestFixtureSourceAttribute(Type sourceType) : base(sourceType)
        {
        }


        public new IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo)
        {
            
            yield return new TestSuite(typeInfo, new object[] { "V1" });
            yield return new TestSuite(typeInfo, new object[] { "V2" });
        }

        public new IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo, IPreFilter filter)
        {
            yield return new TestSuite(typeInfo, new object[] { "V1" });
            yield return new TestSuite(typeInfo, new object[] { "V2" });
        }
    }
}
