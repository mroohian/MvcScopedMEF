using System.Collections.Generic;
using System.Composition;
using DepInj.Contract;
using Domain.Contract.Core;

namespace DepInj {
    // Shared through CoreFacade
    public class Core : ICore {
        private readonly CompositionContext _compositionContext;

        public Core(CompositionContext compositionContext) {
            _compositionContext = compositionContext;
        }

        T ICore.GetExportedValue<T>() {
            return _compositionContext.GetExport<T>();
        }

        void IDebugInfoProvider.AddDebugLines(string prefix, Dictionary<string, string> debugLines) {
            debugLines[$"{prefix}"] = $"(Core={GetHashCode()}), {_compositionContext.GetHashCode()}, {_compositionContext}";
        }
    }
}
