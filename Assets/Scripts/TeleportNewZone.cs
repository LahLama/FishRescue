using UnityEngine;
namespace LahLama
{
    public class TeleportNewZone : MonoBehaviour
    {
        Transform newLocation;
        GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            newLocation = transform.GetChild(0);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                player.transform.position = newLocation.position;
        }
    }
}