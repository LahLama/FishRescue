using UnityEngine;
using UnityEngine.UI;

public class DropHotbarItem : MonoBehaviour
{
    public GameObject slot;
    GameObject item;
    public GameObject defaultSpace;

    public float dropInput;
    private PlayerInputActions inputActions;
    GameObject player;
    void Awake()
    {
        inputActions = new PlayerInputActions(); // Initialize Input Actions
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
    void FixedUpdate()
    {
        if (slot != null)
        {
            if (isDropping() > 0 && TryDropItem(slot))
            {
                DropItem(slot);
            }
        }
    }

    float isDropping()
    {
        dropInput = inputActions.Player.Drop.ReadValue<float>();
        return dropInput;
    }

    public bool TryDropItem(GameObject slot)
    {
        //Used as when an object is picked up it moves to the hotbar heirarchy, Index 0 is image.
        return slot.transform.childCount > 1;
    }

    public void DropItem(GameObject slot)
    {
        item = slot.transform.GetChild(1).gameObject;
        item.SetActive(true);
        item.transform.SetParent(defaultSpace.transform);
        item.transform.position = player.transform.position;
        ChangeIcon(slot);
    }

    void ChangeIcon(GameObject slot)
    {
        slot.transform.GetChild(0).GetComponent<Image>().sprite = null;

    }
}
