using UnityEngine;
namespace LahLama
{
    public class pickUp : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Pick me up");
        }
    }
}