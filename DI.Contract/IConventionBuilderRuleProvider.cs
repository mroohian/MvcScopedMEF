using System.Composition.Convention;

namespace DepInj.Contract {
    public interface IConventionBuilderRuleProvider {
        void AddRules(ConventionBuilder conventionBuilder);
    }
}