namespace Core
{
    public interface IStateMachine
    {
        bool ChangeState<T>();
    }
}