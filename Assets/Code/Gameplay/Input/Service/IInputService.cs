namespace Code.Gameplay.Input.Service
{
  public interface IInputService
  {
    void Enable();
    PlayerInputActions GetActions();
    void Disable();
  }
}