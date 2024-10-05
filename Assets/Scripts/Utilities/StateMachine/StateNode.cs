using System.Collections.Generic;

namespace Scripts.Utilities.StateMachine
{
    public class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> TransitionSet { get; }

        public StateNode(IState state)
        {
            State = state;
            TransitionSet = new();
        }

        public void AddTransition(IState to, IPredicate condition) => TransitionSet.Add(new Transition(to, condition));
    }
}