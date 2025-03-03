using Code.Infrastructure.States.StateInfrastructure;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoopState : IState
  {
    public void Enter()
    {
      Debug.Log("LevelLoopState");
    }
  }
}