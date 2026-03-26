using System.Collections.Generic;
using UnityEngine;

public class orderManagement : MonoBehaviour
{
    Dishes dishes;
    AllowedItems allowedItems;
    List<Dishes.Dish> orderedMeals = new List<Dishes.Dish>();

    void Awake()
    {
        dishes = GameObject.FindFirstObjectByType<Dishes>();
        allowedItems = GetComponent<AllowedItems>();

        int attempts = 2;
        int startPoint = 3;
        int maxVal = dishes.allDishes.Length;


        dishes = GameObject.FindAnyObjectByType<Dishes>();

        for (var i = 0; i < Random.Range(1, attempts + 1); i++)
        {
            dishes.currentDish = dishes.getDish(Random.Range(startPoint, maxVal));
            // orderedMeals.Add(dishes.getDish(Random.Range(startPoint, maxVal)));
            allowedItems.onlyAllowedItems.Add(dishes.currentDish);
        }

        string msg = name + " has ordered " + allowedItems.listDishes();
        Debug.Log(msg);
    }




}
