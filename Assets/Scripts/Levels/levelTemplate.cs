using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class levelTemplate : MonoBehaviour
{

    CustomerSpawning customerSpawning;

    public int maxNum = 3;
    public int level = 0;
    public float waitTime = 5;
    // How often the anger is ticked
    public float angerStep = 15;
    public int completedCustomers = 0;
    public int upsetCustomers = 0;
    public int MoneyGoal = 0;
    public int PerfectMoneyGoal = 0;
    public GameObject nextLevel;
    public List<GameObject> countersToEnable;
    private DaySummary daySummary;
    private MainMoney mainMoney;
    private LevelManager levelManager;
    bool isRoundEnd = false;


    void OnEnable() //This should allow for retrying of a level
    {
        FindAnyObjectByType<SoundsManager>().PlaySound("levelStart", false, GetComponentInParent<AudioSource>());
        customerSpawning = FindAnyObjectByType<CustomerSpawning>();
        daySummary = FindAnyObjectByType<DaySummary>();
        mainMoney = FindAnyObjectByType<MainMoney>();
        levelManager = FindAnyObjectByType<LevelManager>();

        StartCoroutine(spawning());
        isRoundEnd = false;

        // 27 is the average of all dishes' prices thru the anger levels of 37,31,25,18 
        MoneyGoal = maxNum * 27;
        PerfectMoneyGoal = maxNum * 37;
        FindAnyObjectByType<setGoals>().SetMoneyGoalsStart();

        daySummary.dayStats["Day"] = level;
        daySummary.dayStats["Goal"] = MoneyGoal;
        daySummary.dayStats["PerfectGoal"] = PerfectMoneyGoal;

        daySummary.dayStats["MoneyGained"] = (int)mainMoney.money;

        completedCustomers = 0;
        upsetCustomers = 0;

        foreach (var counter in countersToEnable)
        {
            if (counter != null)
            {
                // This assumes visualItem is child 0.
                // This is to reset the counters after each day.
                counter.transform.GetChild(0).GetComponent<UpdatePOSitem>().RemoveItem();
            }
            counter.gameObject.SetActive(true);
        }

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
        if (completedCustomers == maxNum && !isRoundEnd)
        {
            isRoundEnd = true;
            FindAnyObjectByType<SoundsManager>().PlaySound("endLevel", false, GetComponentInParent<AudioSource>());
            Invoke("InitRoundEnd", 2);
            Debug.Log("d");

        }
    }

    private void OnDisable()
    {
        daySummary.dayStats["HappyCustomers"] = completedCustomers - upsetCustomers;
        daySummary.dayStats["UpsetCustomers"] = upsetCustomers;
    }

    void InitRoundEnd()
    {
        if (nextLevel != null)
        {
            GetComponentInParent<LevelManager>().ShowScreenAndButtons(this.gameObject, nextLevel, mainMoney.money >= MoneyGoal);
            Debug.Log("e");
        }
    }

}

