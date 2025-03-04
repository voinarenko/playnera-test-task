using Code.Gameplay.Input;
using Code.Gameplay.Input.Service;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Element
{
  public class Drag : MonoBehaviour, IDraggable
  {
    public bool IsDragging { get; set; }
    public Vector3 Offset { get; set; }

    private Camera _camera;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Start() =>
      _camera = Camera.main;

    private void Update()
    {
      if (IsDragging)
        SetPositionToPointer(_camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>()));
    }

    public void SetPositionToPointer(Vector3 pointerPosition)
    {
      var correctedPosition = pointerPosition + Offset;
      transform.position = new Vector3(correctedPosition.x, correctedPosition.y, transform.position.z);
    }
  }
}