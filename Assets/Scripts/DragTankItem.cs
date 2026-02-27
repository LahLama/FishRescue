using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace LahLama
{
    public class DragTankItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        TankItem tankItem;
        ClarifyTank clarifyTank;
        private PlayerInputActions inputActions;
        private RestrictMouseMovements tankBounds;
        bool isHeld = false;
        public float scrollSpeed = 100;


        void Awake()
        {
            tankItem = GameObject.FindAnyObjectByType<TankItem>();
            inputActions = new PlayerInputActions(); // Initialize Input Actions
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();

            tankBounds = GameObject.FindAnyObjectByType<RestrictMouseMovements>();

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
            tankBounds.SetTankBoundariesFromCollider(this.transform.parent.GetComponent<Collider2D>());
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            // this.transform.rotation = new Quaternion(0, 0, 0, 0);
            isHeld = true;

            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
                fishSwim.enabled = false;
            if (TryGetComponent<FishPersonality>(out FishPersonality fishPeronality))
                fishPeronality.ModifyHealth(+2);



        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = tankBounds.GetClampedWorldPoint(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0.6f;

            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            isHeld = false;
            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
            {
                fishSwim.enabled = true;
                this.GetComponent<Rigidbody2D>().gravityScale = 0f;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }

        }
    }
}