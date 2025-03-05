using UnityEngine;

namespace Code.Gameplay.Scroll
{
  public class Correct : MonoBehaviour
  {
    public void ResizeToCamera(Camera mainCamera)
    {
      var cameraHeight = 2f * mainCamera.orthographicSize;
      var cameraWidth = cameraHeight * mainCamera.aspect;

      transform.localScale = new Vector3(cameraWidth, cameraHeight, 1f);
    }

    public void ClampPosition(Collider2D confinerCollider, Collider2D objectCollider)
    {
      var confinerBounds = confinerCollider.bounds;
      var objectBounds = objectCollider.bounds;

      var minX = confinerBounds.min.x + objectBounds.extents.x;
      var maxX = confinerBounds.max.x - objectBounds.extents.x;
      var minY = confinerBounds.min.y + objectBounds.extents.y;
      var maxY = confinerBounds.max.y - objectBounds.extents.y;

      var clampedPosition = transform.position;
      clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
      clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
      transform.position = clampedPosition;
    }
  }
}