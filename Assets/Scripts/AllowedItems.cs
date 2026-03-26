using System.Collections.Generic;
using UnityEngine;

public class AllowedItems : MonoBehaviour
{
    public List<Dishes.Dish> onlyAllowedItems = new List<Dishes.Dish>();

    public void addDish(Dishes.Dish dish)
    {
        onlyAllowedItems.Add(dish);
    }

    public void removeDish(Dishes.Dish dish)
    {
        onlyAllowedItems.Remove(dish);
    }

    public bool queryDish(Dishes.Dish dish)
    {
        bool foundDish = false;
        foreach (var i in onlyAllowedItems)
        {
            if (i == dish)
            {
                foundDish = true;
            }
        }
        return foundDish;
    }

    public string listDishes()
    {
        string listed = "";
        foreach (var dish in onlyAllowedItems)
        {
            listed += dish.ToString() + '\t';
        }

        return listed;
    }


}
