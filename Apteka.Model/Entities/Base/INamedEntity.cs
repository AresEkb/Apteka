namespace Apteka.Model.Entities.Base
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
