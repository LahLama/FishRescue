using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    public GameObject customerParent;
    public List<GameObject> availlebleCustomers;

    public MeshRenderer customerLight;
    public Material onLight;
    public Material offLight;



    public void spawnCustomer()
    {
        int randomIndex = Random.Range(0, availlebleCustomers.Count);
        bool inRange = randomIndex >= 0 && randomIndex < availlebleCustomers.Count;
        if (!inRange)
            

        availlebleCustomers[randomIndex].SetActive(true);
        availlebleCustomers.RemoveAt(randomIndex);
        customerLight.material = onLight;
        customerLight.gameObject.GetComponent<Light>().enabled = true;
        customerLight.gameObject.GetComponent<AudioSource>().Play();
        Invoke("TurnOffLight", 4f);


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
        }

        // InvokeRepeating("spawnCustomer", 10f, 25f);
    }
}
