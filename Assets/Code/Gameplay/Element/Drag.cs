using Code.Gameplay.Input;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Visual;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Element
{
  public class Drag : MonoBehaviour, IDraggable, IAnimated
  {
    public bool IsDragging { get; set; }
    public Vector3 Offset { get; set; }

    private Camera _camera;
    private SpriteRenderer _renderer;
    private Animate _animate;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Start()
    {
      TryGetComponent(out _renderer);
      _camera = Camera.main;
      TryGetComponent(out _animate);
    }

    private void Update()
    {
      if (!IsDragging)
        return;

      SetPositionToPointer(_camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>()));
      _renderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    public void SetPositionToPointer(Vector3 pointerPosition)
    {
      var correctedPosition = pointerPosition + Offset;
      transform.position = new Vector3(correctedPosition.x, correctedPosition.y, transform.position.z);
    }

    public void Enlarge() => 
      _animate.Enlarge();
    
    public void Restore() => 
      _animate.Restore();
  }
}