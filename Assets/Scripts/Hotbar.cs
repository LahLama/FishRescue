using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    GameObject[] smallSlots;



    public GameObject TryEquipItem()
    {
        foreach (var slot in smallSlots)
        {
            //Used as when an object is picked up it moves to the hotbar heirarchy, Index 0 is image.
            if (slot.transform.childCount < 2)
            {
                return slot;
            }
            else
            {
                Debug.Log("slotFull");
            }
        }
        return null;
    }

    public void EquipItem(GameObject item, GameObject slot)
    {
        item.transform.SetParent(slot.transform);
        ChangeIcon(item, slot);
    }

    void ChangeIcon(GameObject item, GameObject slot)
    {
        slot.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

    }
}
