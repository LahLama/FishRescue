using UnityEngine;
using UnityEngine.EventSystems;
namespace LahLama
{
    public class TakeFishOut : MonoBehaviour, IPointerDownHandler
    {
        public GameObject pickUpPrefab;
        private EquipHotbarItem hotbar;
        void Awake()
        {
            hotbar = FindAnyObjectByType<EquipHotbarItem>();
        }
        public void TakeOut()
        {
            Instantiate(pickUpPrefab);
            pickUpPrefab.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

            var availbleSlot = hotbar.TryEquipItem();
            if (availbleSlot != null)
            {
                hotbar.EquipItem(pickUpPrefab, availbleSlot);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetComponent<FishPersonality>().canBeReleased)
                TakeOut();
        }
    }
}