using UnityEngine;
using UnityEngine.EventSystems;

namespace LahLama
{
    public class TakeFishOut : MonoBehaviour, IPointerDownHandler
    {
        ManageVideoPlayer manageVideoPlayer;
        MoneyManager moneyManager;

        void Awake()
        {
            manageVideoPlayer = FindAnyObjectByType<ManageVideoPlayer>();
            moneyManager = GameObject.FindAnyObjectByType<MoneyManager>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {

            if (GetComponent<FishPersonality>().canBeReleased)
            {
                manageVideoPlayer.PlayVideo(this.gameObject);
                moneyManager.incMoney(Random.Range(1, 6) * 100);
            }
        }
    }
}