using LahLama;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PlayerInputActions inputActions;
    private RestrictMouseMovements tankBounds;
    TankItem tankItem;
    ClarifyTank clarifyTank;
    Rigidbody2D rb;
    Vector2 intialVelocity;
    void Awake()
    {
        tankItem = GameObject.FindAnyObjectByType<TankItem>();
        inputActions = new PlayerInputActions(); // Initialize Input Actions
        clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
        rb = this.GetComponent<Rigidbody2D>();
        tankBounds = GameObject.FindAnyObjectByType<RestrictMouseMovements>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        tankBounds.SetTankBoundariesFromCollider(this.transform.parent.GetComponent<Collider2D>());
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        intialVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
        // this.transform.rotation = new Quaternion(0, 0, 0, 0);

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

        if (TryGetComponent<FishSwim>(out FishSwim fishSwim))
        {
            fishSwim.enabled = true;
        }
        rb.gravityScale = 0f;
        rb.linearVelocity = intialVelocity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.None;


    }
}
