
using UnityEngine;
using UnityEngine.EventSystems;

namespace LahLama
{
    public class TakeFishOut : MonoBehaviour, IPointerDownHandler
    {
        ManageVideoPlayer manageVideoPlayer;

        void Awake()
        {
            manageVideoPlayer = FindAnyObjectByType<ManageVideoPlayer>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetComponent<FishPersonality>().canBeReleased)
                manageVideoPlayer.PlayVideo(this.gameObject);
        }
    }
}