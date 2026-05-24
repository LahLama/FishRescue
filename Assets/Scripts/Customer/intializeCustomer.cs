using UnityEngine;
using System;
using Unity.VisualScripting;


public class IntializeCustomer : MonoBehaviour
{
    public GameObject order1;
    public GameObject order2;
    public GameObject anger;
    void OnEnablex()
    {
        Debug.Log("Starting up: " + this.name);

        GetComponent<orderManagement>().enabled = true;

        GetComponent<GiveFood>().enabled = true;

        GetComponent<AllowedItems>().enabled = true;
        GetComponent<AllowedItems>().onlyAllowedItems.Clear();

        GetComponent<RemoveCustomerPreviews>().enabled = true;

        GetComponent<Dishes>().enabled = true;
        GetComponent<Dishes>().currentDish = Dishes.Dish.placeholder;

        GetComponent<orderManagement>().enabled = true;

        order1.SetActive(false);
        order2.SetActive(false);

        anger.SetActive(true);
        anger.GetComponent<angerManagement>().enabled = true;
    }
}