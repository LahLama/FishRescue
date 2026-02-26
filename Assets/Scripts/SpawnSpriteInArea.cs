using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
namespace LahLama
{
    public class SpawnSpriteInArea : MonoBehaviour
    {
        // ensure colliders are put in mountain,river,ocean
        public PolygonCollider2D[] polyColliders;
        public GameObject[] mountain;
        public GameObject[] river;
        public GameObject[] ocean;
        int currentCountMountain, currentCountRiver, currentCountOcean;

        Vector2 FindRandomSpawnPoint(PolygonCollider2D polyColl)
        {

            Bounds b = polyColl.bounds;
            Vector2 randomSpawnPoint = Vector2.zero;
            bool foundValidPoint = false;

            for (var i = 0; i < 10; i++)
            {
                float x = Random.Range(b.min.x, b.max.x);
                float y = Random.Range(b.min.y, b.max.y);
                randomSpawnPoint = new Vector2(x, y);

                if (polyColl.OverlapPoint(randomSpawnPoint))
                {
                    foundValidPoint = true;
                    break;
                }
            }

            if (foundValidPoint)
            {
                // Debug.Log("Found a spot at " + randomSpawnPoint);
                return randomSpawnPoint;
            }
            return Vector2.zero;
        }

        public void DecideToSpawn()
        {
            int i = 0;
            foreach (var spawnArea in polyColliders)
            {
                float randomVal = Random.Range(0.0f, 1.0f);
                Vector2 randomSpawn = FindRandomSpawnPoint(spawnArea);
                //Prevents spawning at (0,0)
                if (randomSpawn.magnitude > 0)
                {
                    if (randomVal > 0)
                    {

                        switch (i)
                        {
                            case 0:
                                if (currentCountMountain < 10)
                                {
                                    int randomSpriteIndex0 = Random.Range(0, mountain.Length);
                                    Debug.Log(randomSpriteIndex0);
                                    Debug.Log("Spawning " + mountain[randomSpriteIndex0].name + " at " + randomSpawn + " in the " + spawnArea.name);
                                    currentCountMountain++;
                                    Debug.Log("Current count of sprites: " + currentCountMountain);
                                }
                                break;
                            case 1:
                                if (currentCountRiver < 10)
                                {
                                    int randomSpriteIndex1 = Random.Range(0, river.Length);
                                    Debug.Log("Spawning " + river[randomSpriteIndex1].name + " at " + randomSpawn + " in the " + spawnArea.name);
                                    currentCountRiver++;
                                    Debug.Log("Current count of sprites: " + currentCountRiver);
                                }
                                break;
                            case 2:
                                if (currentCountOcean < 10)
                                {
                                    int randomSpriteIndex2 = Random.Range(0, ocean.Length);
                                    Debug.Log("Spawning " + ocean[randomSpriteIndex2].name + " at " + randomSpawn + " in the " + spawnArea.name);
                                    currentCountOcean++;
                                    Debug.Log("Current count of sprites: " + currentCountOcean);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                i++;

            }
        }

    }
}
