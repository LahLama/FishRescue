using UnityEngine;

public class RemoveCustomerPreviews : MonoBehaviour
{

    AllowedItems allowedItems;
    public angerManagement angerManagement;
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

            Debug.Log("Customer has received all their orders");
        }

    }
}
