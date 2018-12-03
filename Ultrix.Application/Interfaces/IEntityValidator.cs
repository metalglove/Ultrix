namespace Ultrix.Application.Interfaces
{
    public interface IEntityValidator<in T>
    {
        bool Validate(T entity);
    }
}
