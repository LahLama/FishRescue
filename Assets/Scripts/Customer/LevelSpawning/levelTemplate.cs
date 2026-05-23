using UnityEngine;
using System.Collections;

public class levelTemplate : MonoBehaviour
{

    CustomerSpawning customerSpawning;

    public int maxNum = 3;
    public string level = "0";
    public float waitTime = 5;
    // How often the anger is ticked
    public float angerStep = 15;
    public int completedCustomers = 0;
    public GameObject nextLevel;

    void Start()
    {
        customerSpawning = FindAnyObjectByType<CustomerSpawning>();
        StartCoroutine(spawning());

    }

    IEnumerator spawning()
    {
        int currentCount = maxNum;
        bool canSpawnMore = true;
        yield return new WaitForSeconds(5);
        while (currentCount > 0)
        {
            canSpawnMore = customerSpawning.spawnCustomer();
            yield return new WaitForSeconds(waitTime);
            if (canSpawnMore)
                currentCount--;
        }
    }


    void Update()
    {
        if (completedCustomers == maxNum)
        {
            completedCustomers = 0;
            NextLevel();

        }
    }

    void NextLevel()
    {
        if (nextLevel != null)
        {
            GetComponentInParent<LevelManager>().TransitionToLevel(this.gameObject, nextLevel);
        }
    }

}

