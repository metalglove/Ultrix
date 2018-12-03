namespace Ultrix.Application.Interfaces
{
    public interface IEntityValidator<in T> where T : class
    {
        bool Validate(T entity);
    }
}
