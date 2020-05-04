using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Seges.CvrServices.Contract.Infrastructure
{
    public class VersionOptIn2TestCaseAttribute : TestCaseAttribute, ITestBuilder
    {
        public VersionOptIn2TestCaseAttribute(params object[] arguments) : base(arguments) { }

        IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test suite)
        {
            var cases = base.BuildFrom(method, suite);

            var modifiedCases = new List<TestMethod>();

            // There's only one test, but iterate to avoid making assumptions about implementation
            foreach (var test in cases)
            {
                if (test.RunState == RunState.Runnable)
                {
                    var typedFixture = (IVersionedTestFixture) test.Fixture;
                    var fixtureVersion = typedFixture.CurrentVersion;
                    if (fixtureVersion != this.Versions)
                    {
                        test.RunState = RunState.Skipped;
                        test.Properties.Set(PropertyNames.SkipReason,
                            $"Explicit opt-in and not opted in (fixture: {fixtureVersion} TestCase: {this.Versions}");
                    }
                }

                modifiedCases.Add(test);
            }

            return modifiedCases;
        }

        /// <summary>
        /// The versions to test
        /// </summary>
        public string Versions
        {
            get => _versions;
            set
            {
                _versions = value;
                Properties.Set("versions", value);
            }
        }
        private string _versions;
    }
}