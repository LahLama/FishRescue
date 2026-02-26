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
        public Transform[] areaParents;
        int currentCountMountain, currentCountRiver, currentCountOcean;
        int mountainCountMax = 10, riverCountMax = 10, oceanCountMax = 10;

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

        public void CanSpawnHappen()
        {
            if (
                currentCountMountain < mountainCountMax ||
                currentCountOcean < oceanCountMax ||
                currentCountRiver < riverCountMax
                )
            {
                DecideToSpawn();
            }
        }
        void DecideToSpawn()
        {
            for (int i = 0; i < polyColliders.Length; i++)
            {
                var spawnArea = polyColliders[i];

                Vector2 randomSpawn = FindRandomSpawnPoint(spawnArea);

                if (randomSpawn.magnitude > 0)
                {
                    float randomChance = Random.Range(0f, 1f);
                    if (randomChance > 0.6f)
                    {
                        // Choose which array/parent to use based on the index 'i'
                        // Assumes: 0 = Mountain, 1 = River, 2 = Ocean
                        if (i == 0)
                            SpawnInArea(mountain, areaParents[0], randomSpawn, ref currentCountMountain);
                        else if (i == 1)
                            SpawnInArea(river, areaParents[1], randomSpawn, ref currentCountRiver);
                        else if (i == 2)
                            SpawnInArea(ocean, areaParents[2], randomSpawn, ref currentCountOcean);
                    }
                }
            }
        }




        void SpawnInArea(GameObject[] prefabArray, Transform spawnArea, Vector2 randomSpawn, ref int areaCounter)
        {
            int randomSpriteIndex = Random.Range(0, prefabArray.Length);
            Debug.Log("Spawning " + prefabArray[randomSpriteIndex].name + " at " + randomSpawn + " in the " + spawnArea.name);
            Instantiate(prefabArray[randomSpriteIndex], randomSpawn, Quaternion.identity, spawnArea);
            // Update the specific counter passed in
            areaCounter++;
            Debug.Log("Current count of sprites: " + areaCounter);
        }
    }
}
