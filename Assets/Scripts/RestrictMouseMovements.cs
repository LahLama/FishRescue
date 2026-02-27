using UnityEngine;
using UnityEngine.EventSystems;

namespace LahLama
{
    public class RestrictMouseMovements : MonoBehaviour
    {
        // Add padding so the fish stays fully inside the glass
        public float padding = 0.5f;

        private float minX, maxX, minY, maxY;
        private Camera mainCam;

        void Awake()
        {
            mainCam = Camera.main;
        }

        public void SetTankBoundariesFromCollider(Collider2D tankCollider)
        {
            if (tankCollider == null) return;

            // This gets the exact world-space box of the collider
            Bounds bounds = tankCollider.bounds;

            minX = bounds.min.x + padding;
            maxX = bounds.max.x - padding;
            minY = bounds.min.y + padding;
            maxY = bounds.max.y - padding;
        }

        public Vector3 GetClampedWorldPoint(Vector2 mousePosition)
        {
            // Use the distance between the camera and the tank (usually 10)
            float distanceFromCamera = Mathf.Abs(mainCam.transform.position.z);
            Vector3 screenPos = new Vector3(mousePosition.x, mousePosition.y, distanceFromCamera);
            Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos);

            // Clamp the worldPos using our bounds
            float x = Mathf.Clamp(worldPos.x, minX, maxX);
            float y = Mathf.Clamp(worldPos.y, minY, maxY);

            return new Vector3(x, y, 0);
        }
    }
}