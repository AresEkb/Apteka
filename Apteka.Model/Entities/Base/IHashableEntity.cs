namespace Apteka.Model.Entities.Base
{
    public interface IHashableEntity<T>
    {
        T Hash { get; set; }
    }
}
