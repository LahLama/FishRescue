using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectItem : MonoBehaviour, IPointerDownHandler
{
    private DropHotbarItem dropHotbarItem;
    public GameObject selector;

    void Awake()
    {
        dropHotbarItem = FindAnyObjectByType<DropHotbarItem>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dropHotbarItem.slot = this.gameObject;
        selector.transform.position = this.gameObject.transform.position;
        Debug.Log(this.gameObject.name + " is currently being selected.");
    }
}
