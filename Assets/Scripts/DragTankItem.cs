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
        private tankDeleteItem tankDeleteItem;
        bool isHeld = false;
        public float scrollSpeed = 100;
        Rigidbody2D rb;


        void Awake()
        {
            tankItem = GameObject.FindAnyObjectByType<TankItem>();
            inputActions = new PlayerInputActions(); // Initialize Input Actions
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
            // tankDeleteItem = FindAnyObjectByType<tankDeleteItem>();
            tankBounds = GameObject.FindAnyObjectByType<RestrictMouseMovements>();
            rb = this.GetComponent<Rigidbody2D>();

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
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            // this.transform.rotation = new Quaternion(0, 0, 0, 0);
            isHeld = true;


            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
                fishSwim.enabled = false;
            else
                rb.linearVelocity = Vector2.zero;
            if (TryGetComponent<FishPersonality>(out FishPersonality fishPeronality))
                fishPeronality.ModifyHealth(+2);



        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = tankBounds.GetClampedWorldPoint(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            rb.gravityScale = 0.6f;

            rb.constraints = RigidbodyConstraints2D.None;
            isHeld = false;
            if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
            {
                fishSwim.enabled = true;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

        }
    }
}