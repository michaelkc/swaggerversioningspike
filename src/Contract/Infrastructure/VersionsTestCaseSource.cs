using System.Collections;
using System.Linq;

namespace Seges.CvrServices.Contract.Infrastructure
{
    public class VersionsTestCaseSource
    {
        public static IEnumerable TestCases(string[] versionsSupportedByTest)
        {
            // Shoehorning instance data into this static...not going to be pretty
            var allVersions = new string[] {"V1", "V2"};

            var selectedVersions = allVersions.Intersect(versionsSupportedByTest);

            foreach (var selectedVersion in selectedVersions)
            {
                yield return new object[] { selectedVersion };
            }
        }
    }
}