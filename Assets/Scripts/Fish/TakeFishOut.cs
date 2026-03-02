using UnityEngine;
namespace LahLama
{
    public class TakeFishOut : MonoBehaviour
    {
        private EquipHotbarItem hotbar;
        void Awake()
        {
            hotbar = FindAnyObjectByType<EquipHotbarItem>();
        }
        public void TakeOut()
        {
            var availbleSlot = hotbar.TryEquipItem();
            if (availbleSlot != null)
            {
                hotbar.EquipItem(this.gameObject, availbleSlot);
            }
        }
    }
}