using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    public GameObject customerParent;
    public List<GameObject> availlebleCustomers;
    public MeshRenderer customerLight;
    public Material onLight;
    public Material offLight;
    public AudioSource audioSource;




    public bool spawnCustomer()
    {
        int randomIndex = Random.Range(0, availlebleCustomers.Count);
        bool inRange = randomIndex >= 0 && randomIndex < availlebleCustomers.Count;

        // If there is no availible customers it will return false, so that the currentCount in levelTemplate doesnt decrease
        if (!inRange)
            return false;

        availlebleCustomers[randomIndex].SetActive(true);
        availlebleCustomers.RemoveAt(randomIndex);
        customerLight.material = onLight;
        customerLight.gameObject.GetComponent<Light>().enabled = true;
        FindAnyObjectByType<SoundsManager>().PlaySound("newCustomerLight", false, audioSource);

        Invoke("TurnOffLight", 4f);
        return true;


    }

    void TurnOffLight()
    {
        customerLight.material = offLight;
        customerLight.gameObject.GetComponent<Light>().enabled = false;
    }

    // Random spawning of customers every 10 seconds
    public void Start()
    {
        availlebleCustomers = new List<GameObject>();
        // Add all customers to the list
        foreach (Transform child in customerParent.transform)
        {
            availlebleCustomers.Add(child.gameObject);
            child.gameObject.SetActive(false); // Ensure all customers are initially inactive
            child.TryGetComponent<orderManagement>(out var orderManageScript);
            if (orderManageScript != null)
                orderManageScript.enabled = true;
            child.GetComponent<IntializeCustomer>().enabled = true;
        }

        // InvokeRepeating("spawnCustomer", 10f, 25f);
    }
}
