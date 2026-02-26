using UnityEngine;
namespace LahLama
{
    public class ActivateObjSpawn : MonoBehaviour
    {
        SpawnSpriteInArea spawnSpriteInArea;
        GameObject player;


        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            spawnSpriteInArea = GameObject.FindAnyObjectByType<SpawnSpriteInArea>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            spawnSpriteInArea.CanSpawnHappen();
        }
    }
}