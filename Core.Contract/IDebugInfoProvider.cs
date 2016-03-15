using System.Collections.Generic;

namespace Domain.Contract.Core {
    public interface IDebugInfoProvider {
        void AddDebugLines(string prefix, Dictionary<string, string> debugLines);
    }
}
