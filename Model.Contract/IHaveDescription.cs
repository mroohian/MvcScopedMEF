namespace Model.Contract {
    public interface IHaveDescription<TDescriptionModel> where TDescriptionModel : class, IDescriptionModel {}
}