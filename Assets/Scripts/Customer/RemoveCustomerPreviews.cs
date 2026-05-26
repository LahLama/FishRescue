using System;
using UnityEngine;

public class RemoveCustomerPreviews : MonoBehaviour
{

    AllowedItems allowedItems;
    public angerManagement angerManagement;
    public AddMoneyVisual addMoneyVisual;


    public void RemovePreview(Dishes.Dish dish)
    {

        allowedItems = GetComponent<AllowedItems>();

        // Debug.Log("Giving the customer the " + dish);
        int index = allowedItems.onlyAllowedItems.IndexOf(dish);
        if (index != -1)
        {
            allowedItems.onlyAllowedItems[index] = Dishes.Dish.placeholder;
            transform.GetChild(index).gameObject.SetActive(false);
        }

        if (allowedItems.onlyAllowedItems.TrueForAll(dish => dish == Dishes.Dish.placeholder))
        {

            CustomerSounds customerSounds = FindAnyObjectByType<CustomerSounds>();
            customerSounds.PlaySound("thankyou", false);

            angerManagement.stopAnger();

            // The amount of money earned is based on the anger level of the customer, the calmer they are, the more money you earn
            // Divide by 100, to get a fraction.
            float moneyEarned = 25f * ((float)angerManagement.currentAngerLevel / 100);
            moneyEarned = Mathf.Floor(moneyEarned);
            addMoneyVisual.StartMoney(moneyEarned);

            FindAnyObjectByType<MainMoney>().AddMoney(moneyEarned);
        }

    }

    public void RemoveLeavingCustomerPreview()
    {
        allowedItems = GetComponent<AllowedItems>();

        for (int i = 0; i < allowedItems.onlyAllowedItems.Count; i++)
        {

            allowedItems.onlyAllowedItems[i] = Dishes.Dish.placeholder;
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
