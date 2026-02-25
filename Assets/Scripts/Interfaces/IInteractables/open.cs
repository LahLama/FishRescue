using UnityEngine;
namespace LahLama
{
    public class open : MonoBehaviour, IInteractable
    {

        GameObject player;
        public GameObject tank;
        ClarifyTank clarifyTank;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
        }
        public void Interact()
        {
            clarifyTank.CurrentTank = tank;
            player.transform.position = tank.transform.position;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            tank.GetComponent<LeaveTank>().enabled = true;
            tank.GetComponent<TankItem>().enabled = true;
        }
    }
}