using UnityEngine;
namespace LahLama
{
    public class open : MonoBehaviour, IInteractable
    {

        GameObject player;
        public GameObject tank;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public void Interact()
        {
            player.transform.position = tank.transform.position;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            tank.GetComponent<LeaveTank>().enabled = true;
        }
    }
}