using UnityEngine;
namespace LahLama
{
    public class pickUp : MonoBehaviour, IInteractable
    {
        private EquipHotbarItem hotbar;

        void Awake()
        {
            hotbar = FindAnyObjectByType<EquipHotbarItem>();
        }
        public void Interact()
        {
            Debug.Log("Pick me up");
            var availbleSlot = hotbar.TryEquipItem();
            if (availbleSlot != null)
            {
                hotbar.EquipItem(this.gameObject, availbleSlot);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}