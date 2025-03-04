using Code.Gameplay.Input.Service;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Scroll
{
  public class LevelScroll : MonoBehaviour
  {
    [SerializeField] private float _dragSpeed;
    [SerializeField] private Collider2D _confinerCollider;
    private Collider2D _objectCollider;
    private Camera _camera;
    private CorrectTransforms _corrector;
    private Vector2 _dragOrigin;
    private bool _isDragging;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void OnEnable()
    {
      _inputService.GetActions().Enable();
      _inputService.GetActions().Player.Touch.started += OnTouchStarted;
      _inputService.GetActions().Player.Touch.canceled += OnTouchCanceled;
    }

    private void Start()
    {
      _camera = Camera.main;
      TryGetComponent(out _corrector);
      _corrector.ResizeToCamera(_camera);
      TryGetComponent(out _objectCollider);
    }

    private void Update()
    {
      if (!_isDragging)
        return;

      Vector2 currentPosition = _camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>());
      var difference = currentPosition - _dragOrigin;
      transform.position += new Vector3(-difference.x, 0, 0) * (_dragSpeed * Time.deltaTime);
      _corrector.ClampPosition(_confinerCollider, _objectCollider);
    }

    private void OnDisable()
    {
      _inputService.GetActions().Disable();
      _inputService.GetActions().Player.Touch.started -= OnTouchStarted;
      _inputService.GetActions().Player.Touch.canceled -= OnTouchCanceled;
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
      _isDragging = true;
      _dragOrigin = _camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>());
    }

    private void OnTouchCanceled(InputAction.CallbackContext context) =>
      _isDragging = false;
  }
}