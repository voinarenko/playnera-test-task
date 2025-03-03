namespace Code.Gameplay.Input.Service
{
  public class InputService : IInputService
  {
    private readonly PlayerInputActions _controls;

    public InputService()
    {
      _controls = new PlayerInputActions();
      _controls.Enable();
    }

    public PlayerInputActions GetActions() => 
      _controls;
  }
}