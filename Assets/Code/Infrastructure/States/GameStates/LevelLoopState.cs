using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.StateInfrastructure;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoopState : IState
  {
    private readonly IInputService _inputService;

    public LevelLoopState(IInputService inputService) =>
      _inputService = inputService;

    public void Enter() =>
      _inputService.Enable();

    public void Exit() =>
      _inputService.Disable();
  }
}