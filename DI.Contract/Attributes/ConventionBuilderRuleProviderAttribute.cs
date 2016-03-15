using System;

namespace DepInj.Contract.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConventionBuilderRuleProviderAttribute : Attribute { }
}
