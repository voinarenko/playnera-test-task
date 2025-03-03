using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.StateMachine
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly IStateFactory _stateFactory;

    public GameStateMachine(IStateFactory stateFactory) =>
      _stateFactory = stateFactory;

    public void Enter<TState>() where TState : class, IState =>
      _stateFactory.GetState<TState>().Enter();
  }
}