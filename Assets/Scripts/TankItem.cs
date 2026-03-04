using LahLama;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UI;

namespace LahLama
{
    public class TankItem : MonoBehaviour
    {

        public GameObject fishPrefabItem;
        public GameObject tankPrefabItem;
        public GameObject newItem;
        public Transform defaultLocation;
        private PlayerInputActions inputActions;
        public int numberOfItems;
        public int numberOfFish;
        int excludeMask;

        void Awake()
        {
            this.GetComponent<TankItem>().enabled = false;
            excludeMask = LayerMask.GetMask("fish", "tankItems");
        }
        public GameObject MakeTankItem(GameObject currentSlot)
        {
            if (currentSlot.transform.childCount > 1)
            {
                if (currentSlot.transform.GetChild(1).tag == "fish" && numberOfFish < 3)
                {
                    newItem = Instantiate(fishPrefabItem);
                }
                else if (currentSlot.transform.GetChild(1).tag != "fish")
                {
                    newItem = Instantiate(tankPrefabItem);
                }
                else
                {
                    return null;
                }

                newItem.transform.SetParent(defaultLocation.parent);
                newItem.TryGetComponent<SpriteRenderer>(out var spriteRend);
                newItem.transform.localScale = new Vector3(-1, 1, 1);

                if (spriteRend != null)
                {

                    spriteRend.sprite = currentSlot.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    if (currentSlot.transform.GetChild(1).tag == "fish")
                    {
                        newItem.gameObject.layer = LayerMask.NameToLayer("fish");
                        newItem.AddComponent<CapsuleCollider2D>();
                        CapsuleCollider2D col = newItem.GetComponent<CapsuleCollider2D>();
                        newItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        col.excludeLayers = excludeMask;
                        SpawnFish(GameObject.FindAnyObjectByType<FishNames>().GetRandomName());
                        numberOfFish++;
                    }
                    else
                    {
                        newItem.gameObject.layer = LayerMask.NameToLayer("tankItems");
                        newItem.AddComponent<PolygonCollider2D>();
                        PolygonCollider2D col = newItem.GetComponent<PolygonCollider2D>();
                        col.excludeLayers = LayerMask.NameToLayer("fish");
                        numberOfItems++;
                    }
                    removeItemFromHotbar(currentSlot);



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

        void SpawnFish(string newName)
        {
            if (newItem.TryGetComponent<FishPersonality>(out FishPersonality stats))
                stats = newItem.GetComponent<FishPersonality>();

            if (stats != null)
            {
                stats.ChangeName(newName);
                stats.IntializeFish();
                stats.ModifyHealth(10);
                newItem.transform.GetChild(0).GetComponent<TextMeshPro>().text = stats.health.ToString();

            }
        }

    }
}

