using DepInj.Contract;
using Domain.Contract.Core;

namespace Domain.Core {
    public abstract class BaseDomain : IBaseDomain {
        protected readonly ICore Core;
        protected readonly ISessionContext SessionContext;

        protected BaseDomain(ICore core) {
            Core = core;
            SessionContext = core.GetExportedValue<ISessionContext>();
        }
    }
}