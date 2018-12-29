namespace Ultrix.Application.Interfaces
{
    public interface IFactory<out TResult>
    {
        TResult Create();
    }
    public interface IFactory<out TResult, in TParameter>
    {
        TResult Create(TParameter parameter);
    }
    public interface IFactory<out TResult, in TParameter1, in TParameter2>
    {
        TResult Create(TParameter1 parameter, TParameter2 parameter2);
    }
}
