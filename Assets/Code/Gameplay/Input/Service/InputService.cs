namespace Code.Gameplay.Input.Service
{
  public class InputService : IInputService
  {
    private readonly PlayerInputActions _controls = new();

    public void Enable() =>
      _controls.Enable();

    public PlayerInputActions GetActions() =>
      _controls;

    public void Disable() =>
      _controls.Disable();
  }
}