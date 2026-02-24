using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectHotbarItem : MonoBehaviour, IPointerDownHandler
{
    private DropHotbarItem dropHotbarItem;
    public GameObject selector;
    public GameObject slot;

    void Awake()
    {
        dropHotbarItem = FindAnyObjectByType<DropHotbarItem>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dropHotbarItem.slot = this.gameObject;
        selector.transform.position = this.gameObject.transform.position;
        slot = this.gameObject;
        Debug.Log(this.gameObject.name + " is currently being selected.");
    }
}
