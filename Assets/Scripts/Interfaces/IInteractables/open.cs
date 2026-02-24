using UnityEngine;
namespace LahLama
{
    public class open : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Open me up");
        }
    }
}