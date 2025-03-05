using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoadState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;

    public LevelLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public void Enter() =>
      _sceneLoader.LoadScene(Scenes.Level, OnLoaded);

    public void Exit()
    {
      
    }

    private void OnLoaded() =>
      _stateMachine.Enter<LevelEnterState>();
  }
}