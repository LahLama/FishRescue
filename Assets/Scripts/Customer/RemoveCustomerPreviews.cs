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

            angerManagement.stopAnger();
            // Should have a EndCustomer script that handles the end of the customer, this is just a placeholder for now

            // The amount of money earned is based on the anger level of the customer, the calmer they are, the more money you earn
            // Divide by 100, to get a fraction.
            float moneyEarned = 25f * ((float)angerManagement.currentAngerLevel / 100);
            moneyEarned = Mathf.Floor(moneyEarned);
            addMoneyVisual.StartMoney(moneyEarned);

            FindAnyObjectByType<MainMoney>().AddMoney(moneyEarned);


            Debug.Log("Customer has received all their orders");
        }

    }
}
