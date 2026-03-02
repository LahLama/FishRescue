using LahLama;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishTap : MonoBehaviour, IPointerDownHandler
{
    FishPersonality fishPersonality;
    void Awake()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (TryGetComponent<FishPersonality>(out fishPersonality))
            fishPersonality.ModifyHunger(Random.Range(1, 3) * 5);
    }


}
