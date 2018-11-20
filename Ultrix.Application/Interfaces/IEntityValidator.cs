namespace Ultrix.Application.Interfaces
{
    public interface IEntityValidator<T>
    {
        bool Validate(T entity);
    }
}
