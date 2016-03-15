using System.Composition;
using DepInj.Contract;

namespace DepInj {
    [Export(typeof(ICore))]
    // Cannot be shared because of mixed sharing boundaries
    public class CoreFacade : Core {

        [ImportingConstructor]
        public CoreFacade(CompositionContext compositionContext) : base(compositionContext) { }
    }
}