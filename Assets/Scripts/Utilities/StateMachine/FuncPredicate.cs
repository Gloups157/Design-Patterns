using System;

namespace Scripts.Utilities.StateMachine
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> predicate;

        public FuncPredicate(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public bool Evaluate() => predicate.Invoke();
    }
}