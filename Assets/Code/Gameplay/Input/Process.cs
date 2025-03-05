using Code.Gameplay.Element;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Visual;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Input
{
  public class Process : MonoBehaviour
  {
    private const string BackgroundTag = "Background";
    private const string ElementTag = "Element";

    private IInputService _inputService;

    private Camera _camera;
    private CinemachineCamera _cinemachineCamera;
    private IDraggable _draggable;
    private IAnimated _animated;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void OnEnable()
    {
      _inputService.GetActions().Player.Touch.started += OnTouchStarted;
      _inputService.GetActions().Player.Touch.canceled += OnTouchCanceled;
      _inputService.GetActions().UI.Escape.performed += Quit;
    }

    private void Start()
    {
      _camera = Camera.main;
      FindAnyObjectByType<CinemachineCamera>().TryGetComponent(out _cinemachineCamera);
    }


    private void OnDisable()
    {
      _inputService.GetActions().Player.Touch.started -= OnTouchStarted;
      _inputService.GetActions().Player.Touch.canceled -= OnTouchCanceled;
      _inputService.GetActions().UI.Escape.performed -= Quit;
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
      var touchPosition = _camera.ScreenToWorldPoint(_inputService.GetActions().Player.Drag.ReadValue<Vector2>());
      var hit = Physics2D.Raycast(touchPosition, Vector2.zero);
      hit.transform.TryGetComponent(out _draggable);
      hit.transform.TryGetComponent(out _animated);
      _animated?.Enlarge();
      if (hit && hit.transform.CompareTag(BackgroundTag))
      {
        hit.transform.TryGetComponent<Scroll.Scroll>(out var scroll);
        _cinemachineCamera.Follow = hit.transform;
        scroll.IsDragging = true;
        scroll.DragOrigin = touchPosition;
      }
      else if (hit && hit.transform.CompareTag(ElementTag))
      {
        hit.transform.TryGetComponent<Drag>(out var drag);
        _cinemachineCamera.Follow = hit.transform;
        drag.IsDragging = true;
        drag.Offset = drag.transform.position - touchPosition;
        drag.SetPositionToPointer(touchPosition);
      }
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
      _draggable.IsDragging = false;
      _animated?.Restore();
    }

    private void Quit(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      Application.Quit();
#elif UNITY_ANDROID
      var activity =
        new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
      activity.Call<bool>("moveTaskToBack", true);
#endif
    }
  }
}