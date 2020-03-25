public interface IStateMachine<in T>
{
    void SetTrigger(T trigger);
}