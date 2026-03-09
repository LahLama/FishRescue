using UnityEngine;
namespace LahLama
{
    public class TeleportNewZone : MonoBehaviour
    {
        Transform newLocation;
        private Camera cam;
        private Camera aquaCam;
        GameObject player;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            newLocation = transform.GetChild(0);
            aquaCam = GameObject.FindGameObjectWithTag("AquariumCamera").GetComponent<Camera>();

            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                player.transform.position = newLocation.position;
            if (this.tag == "aqua_trans")
            {
                cam.enabled = !cam.isActiveAndEnabled;
                aquaCam.enabled = !aquaCam.isActiveAndEnabled;
            }
        }
    }
}