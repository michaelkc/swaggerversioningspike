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
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class VersionOptInTestCaseAttribute : NUnitAttribute, ITestBuilder, ITestCaseData, IImplyFixture
    {
        private readonly TestCaseAttribute _decoratedAttribute;
        private const string PropertyNameVersion = "versions";


        public VersionOptInTestCaseAttribute(params object[] arguments) 
        {
            _decoratedAttribute = new TestCaseAttribute(arguments);
        }

        public VersionOptInTestCaseAttribute(object arg) 
        {
            _decoratedAttribute = new TestCaseAttribute(arg);
        }

        public VersionOptInTestCaseAttribute(object arg1, object arg2) 
        {
            _decoratedAttribute = new TestCaseAttribute(arg1,arg2);
        }

        public VersionOptInTestCaseAttribute(object arg1, object arg2, object arg3) 
        {
            _decoratedAttribute = new TestCaseAttribute(arg1, arg2, arg3);
        }

        /// <summary>
        /// The versions this test should run for
        /// </summary>
        public string Versions
        {
            get { return Properties.Get(PropertyNameVersion) as string; }
            set { Properties.Set(PropertyNameVersion, value); }
        }


        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            return _decoratedAttribute.BuildFrom(method, suite);
        }

        public string TestName => _decoratedAttribute.TestName;
        public RunState RunState => _decoratedAttribute.RunState;
        public object[] Arguments => _decoratedAttribute.Arguments;
        public IPropertyBag Properties => _decoratedAttribute.Properties;
        public object ExpectedResult => _decoratedAttribute.ExpectedResult;
        public bool HasExpectedResult => _decoratedAttribute.HasExpectedResult;
    }
}
