using Scripts.Physic;

namespace Scripts.Utilities.StateMachine
{
    public interface IState : IPhysics2D
    {
        void Enter();
        void FixedUpdate();
        void Update();
        void Exit();
    }
}