using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;

    public LevelEnterState(
      IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    public void Enter() =>
      _stateMachine.Enter<LevelLoopState>();
  }
}