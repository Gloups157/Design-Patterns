namespace Scripts.Utilities.StateMachine
{
    public interface IBetterTransition
    {
        void At(IState from, IState to, IPredicate condition);
        void Any(IState to, IPredicate condition);
    }
}