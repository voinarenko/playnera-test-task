using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.StateMachine
{
  public interface IGameStateMachine 
  {
    void Enter<TState>() where TState : class, IState;
  }
}