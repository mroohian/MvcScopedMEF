namespace Domain.Contract.Core {
    public interface ISessionContext : IDebugInfoProvider {
        int LanguageId { get; set; }
    }
}