using LahLama;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PlayerInputActions inputActions;
    private RestrictMouseMovements tankBounds;
    TankItem tankItem;
    ClarifyTank clarifyTank;
    void Awake()
    {
        tankItem = GameObject.FindAnyObjectByType<TankItem>();
        inputActions = new PlayerInputActions(); // Initialize Input Actions
        clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();

        tankBounds = GameObject.FindAnyObjectByType<RestrictMouseMovements>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        tankBounds.SetTankBoundariesFromCollider(this.transform.parent.GetComponent<Collider2D>());
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;


    }
}
