using LahLama;
using UnityEngine;
using UnityEngine.UI;

namespace LahLama
{
    public class TankItem : MonoBehaviour
    {

        public GameObject prefabItem;
        public GameObject newItem;
        public Transform defaultLocation;
        private PlayerInputActions inputActions;

        void Awake()
        {
            this.GetComponent<TankItem>().enabled = false;
        }
        public GameObject MakeTankItem(GameObject currentSlot)
        {
            if (currentSlot.transform.childCount > 1)
            {
                newItem = Instantiate(prefabItem);
                newItem.TryGetComponent<PolygonCollider2D>(out var polyColl);
                newItem.TryGetComponent<SpriteRenderer>(out var spriteRend);

                if (polyColl != null && spriteRend != null)
                {
                    //This should reset the current item to have a custom polygon collider

                    DestroyImmediate(polyColl);
                    spriteRend.sprite = currentSlot.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    if (currentSlot.transform.GetChild(1).tag == "fish")
                        newItem.AddComponent<FishSwim>();
                    removeItemFromHotbar(currentSlot);
                    newItem.AddComponent<PolygonCollider2D>();
                    newItem.AddComponent<DragTankItem>();

                    moveItemOnPointer(newItem);

                    return newItem;
                }
                else
                {
                    Debug.LogError("Sprite has no Renderer or Collider");
                }
            }
            return null;
        }

        void removeItemFromHotbar(GameObject slot)
        {

            slot.transform.GetChild(0).GetComponent<Image>().sprite = null;
            DestroyImmediate(slot.transform.GetChild(1).gameObject);
        }
        void moveItemOnPointer(GameObject item)
        {
            item.transform.position = defaultLocation.position;
        }


    }
}

