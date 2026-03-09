using UnityEngine;
namespace LahLama
{
    public class openTank : MonoBehaviour, IInteractable
    {

        GameObject player;
        public GameObject tank;
        ClarifyTank clarifyTank;
        Camera mainCam;
        Camera aquaCam;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        public void Interact()
        {
            mainCam.enabled = !mainCam.isActiveAndEnabled;


            clarifyTank.CurrentTank = tank;
            player.transform.position = tank.transform.position;
            player.GetComponent<Collider2D>().enabled = false;
            player.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            tank.GetComponent<LeaveTank>().enabled = true;
            tank.GetComponent<TankItem>().enabled = true;

        }
    }
}