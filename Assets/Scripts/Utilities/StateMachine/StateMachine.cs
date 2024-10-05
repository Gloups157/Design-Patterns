using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utilities.StateMachine
{
    public class StateMachine
    {
        StateNode currentState;
        Dictionary<Type, StateNode> nodeDic = new();
        HashSet<ITransition> anyTransitionSet = new();

        public void FixedUpdate() => currentState.State?.FixedUpdate();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null) ChangeState(transition.To);
            currentState.State?.Update();
        }

        public void OnCollisionEnter2D(Collision2D other) => currentState.State?.OnCollisionEnter2D(other);
        public void OnCollisionStay2D(Collision2D other) => currentState.State?.OnCollisionStay2D(other);
        public void OnCollisionExit2D(Collision2D other) => currentState.State?.OnCollisionExit2D(other);
        public void OnTriggerEnter2D(Collider2D other) => currentState.State?.OnTriggerEnter2D(other);
        public void OnTriggerStay2D(Collider2D other) => currentState.State?.OnTriggerStay2D(other);
        public void OnTriggerExit2D(Collider2D other) => currentState.State?.OnTriggerExit2D(other);

        public void SetState(IState state)
        {
            currentState = nodeDic[state.GetType()];
            currentState.State?.Enter();
        }

        void ChangeState(IState state)
        {
            if (state.Equals(currentState.State)) return;
            IState previousState = currentState.State;
            IState nextState = nodeDic[state.GetType()].State;
            previousState?.Exit();
            nextState?.Enter();
            currentState = nodeDic[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (ITransition transition in anyTransitionSet)
            {
                if (transition.Condition.Evaluate()) return transition;
            }
            foreach (ITransition transition in currentState.TransitionSet)
            {
                if (transition.Condition.Evaluate()) return transition;
            }
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition) => GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        public void AddAnyTransition(IState to, IPredicate condition) => anyTransitionSet.Add(new Transition(GetOrAddNode(to).State, condition));

        StateNode GetOrAddNode(IState state)
        {
            StateNode node = nodeDic.GetValueOrDefault(state.GetType());
            if(node == null)
            {
                node = new(state);
                nodeDic.Add(state.GetType(), node);
            }
            return node;
        }

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
}