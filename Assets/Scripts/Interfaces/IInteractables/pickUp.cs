using UnityEngine;
namespace LahLama
{
    public class pickUp : MonoBehaviour, IInteractable
    {
        private Hotbar hotbar;

        void Awake()
        {
            hotbar = FindAnyObjectByType<Hotbar>();
        }
        public void Interact()
        {
            Debug.Log("Pick me up");
            var availbleSlot = hotbar.TryEquipItem();
            if (availbleSlot != null)
            {
                hotbar.EquipItem(this.gameObject, availbleSlot);
                this.gameObject.SetActive(false);
            }
        }
    }
}