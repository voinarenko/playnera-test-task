using Code.Gameplay.Input;
using Code.Gameplay.Input.Service;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Scroll
{
  public class LevelScroll : MonoBehaviour, IDraggable
  {
    public bool IsDragging { get; set; }
    public Vector2 DragOrigin { get; set; }

    private IInputService _inputService;
    
    [SerializeField] private float _dragSpeed;
    [SerializeField] private Collider2D _confinerCollider;
    private Collider2D _objectCollider;
    private Camera _camera;
    private CorrectTransforms _corrector;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Start()
    {
      _camera = Camera.main;
      TryGetComponent(out _corrector);
      _corrector.ResizeToCamera(_camera);
      TryGetComponent(out _objectCollider);
    }

    private void Update()
    {
      if (!IsDragging)
        return;

      Vector2 currentPosition = _camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>());
      var difference = currentPosition - DragOrigin;
      transform.position += new Vector3(-difference.x, 0, 0) * (_dragSpeed * Time.deltaTime);
      _corrector.ClampPosition(_confinerCollider, _objectCollider);
    }
  }
}