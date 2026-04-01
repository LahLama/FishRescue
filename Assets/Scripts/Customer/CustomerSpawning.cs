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
        availlebleCustomers[randomIndex].SetActive(true);
        availlebleCustomers.RemoveAt(randomIndex);
        customerLight.material = onLight;
        customerLight.gameObject.GetComponent<AudioSource>().Play();
        Invoke("TurnOffLight", 4f);


    }

    void TurnOffLight()
    {
        customerLight.material = offLight;
    }

    // Random spawning of customers every 10 seconds
    void Start()
    {
        availlebleCustomers = new List<GameObject>();
        // Add all customers to the list
        foreach (Transform child in customerParent.transform)
        {
            availlebleCustomers.Add(child.gameObject);
            child.gameObject.SetActive(false); // Ensure all customers are initially inactive
        }

        InvokeRepeating("spawnCustomer", 25f, 25f);
    }
}
