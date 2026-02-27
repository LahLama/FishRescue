using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace LahLama
{
    public class DragTankItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        TankItem tankItem;
        private PlayerInputActions inputActions;
        bool isHeld = false;
        public float scrollSpeed = 100;
        public int minX = 25, minY = 75, maxX = 300, maxY = 160;


        void Awake()
        {
            tankItem = GameObject.FindAnyObjectByType<TankItem>();
            inputActions = new PlayerInputActions(); // Initialize Input Actions
        }
        void FixedUpdate()
        {

            if (isHeld)
            {

                // When player scrolls, item should rotate.
                Vector2 scrollData = inputActions.UI.ScrollWheel.ReadValue<Vector2>();
                float rotationAmount = scrollData.y * 5f * Time.fixedDeltaTime;
                transform.Rotate(0, 0, rotationAmount * scrollSpeed);

            }
        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            // this.transform.rotation = new Quaternion(0, 0, 0, 0);
            isHeld = true;

            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
                fishSwim.enabled = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = eventData.position;

            float clampedX = Mathf.Clamp(mousePos.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePos.y, minY, maxY);
            Vector3 screenPos = new Vector3(clampedX, clampedY, 0);
            var worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            this.transform.position = new Vector3(worldPos.x, worldPos.y, 0); ;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0.6f;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            isHeld = false;
            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
                fishSwim.enabled = true;

        }
    }
}