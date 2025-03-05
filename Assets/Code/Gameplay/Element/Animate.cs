using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Element
{
  public class Animate : MonoBehaviour
  {
    private const float Duration = 0.3f;
    private const float EnlargeFactor = 1.2f;

    private Vector3 _initialScale;

    private void Awake() =>
      _initialScale = transform.localScale;

    public void Enlarge() =>
      transform.DOScale(_initialScale * EnlargeFactor, Duration);

    public void Restore() =>
      transform.DOScale(_initialScale, Duration);
  }
}