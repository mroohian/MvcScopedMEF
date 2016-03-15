using Domain.Contract.Core;

namespace DepInj.Contract {
    public interface ICore : IDebugInfoProvider {
        T GetExportedValue<T>();
    }
}
