using System.Collections;
using System.Linq;

namespace Seges.CvrServices.Contract.Infrastructure
{
    // This class must be conventionally recreated per project as to be usable via TestCaseSource.
    // Name is kept short as to keep the TestCaseSource attribute declarations readable.
    // Input to TestCaseSource must be compile-time constant, and everything related to dynamically
    // generating test cases must be static, so it is difficult to externalize / parameterize
    // the available versions with less ceremony
    // Inheriting TestCaseSource / implementing its interfaces might make the interface a little bit
    // more smooth, but will still be subject to the same constraints.
    internal static class ApiVersions
    {
        //private static readonly string[] AllVersions = { V1, V2, V3 };
        private static readonly string[] AllVersions = { V1, V2 };
        public const string All = null;
        public const string V1 = nameof(V1);
        public const string V2 = nameof(V2);
        //public const string V3 = nameof(V3);

        public static IEnumerable TestCases(string[] versionsSupportedByTest)
        {
            var selectedVersions = AllVersions.Intersect(versionsSupportedByTest).ToArray();

            if (!selectedVersions.Any())
            {
                selectedVersions = AllVersions;
            }
            foreach (var selectedVersion in selectedVersions)
            {
                yield return new object[] { selectedVersion };
            }
        }

        public const string SourceName = nameof(TestCases);
    }
}