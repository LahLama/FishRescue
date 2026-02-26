using UnityEngine;
using UnityEngine.EventSystems;
namespace LahLama
{
    public class DragTankItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        TankItem tankItem;
        private PlayerInputActions inputActions;
        bool isHeld = false;
        public float scrollSpeed = 1;

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
                Debug.Log(this.transform.rotation.z + scrollData.magnitude);
                Debug.Log(this.transform.rotation.z);
                Debug.Log(scrollData.magnitude);
                if (scrollData.magnitude > 0)
                    this.transform.rotation = new(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, 0);
                if (scrollData.magnitude < 0)
                    this.transform.rotation = new(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, 0);
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
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            isHeld = true;

        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = eventData.position;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            this.transform.position = worldPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            isHeld = false;

        }
    }
}