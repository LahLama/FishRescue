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
                newItem.transform.SetParent(defaultLocation.parent);
                newItem.TryGetComponent<SpriteRenderer>(out var spriteRend);

                if (spriteRend != null)
                {


                    this.gameObject.layer = LayerMask.NameToLayer("tankItems");
                    spriteRend.sprite = currentSlot.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    if (currentSlot.transform.GetChild(1).tag == "fish")
                    {
                        newItem.AddComponent<FishSwim>();
                        newItem.GetComponent<Rigidbody2D>().gravityScale = 0;
                    }
                    removeItemFromHotbar(currentSlot);
                    newItem.AddComponent<PolygonCollider2D>();


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

