using System.Collections;

namespace Seges.CvrServices.Contract.Infrastructure
{
    public class VersionsTestFixtureSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { "V1"};
            yield return new object[] { "V2"};
        }
    }
}
