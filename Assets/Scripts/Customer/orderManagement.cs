using System.Collections.Generic;
using UnityEngine;

public class orderManagement : MonoBehaviour
{
    Dishes dishes;
    List<Dishes.OrderableDish> orderedMeals = new List<Dishes.OrderableDish>();

    void Awake()
    {
        dishes = GameObject.FindFirstObjectByType<Dishes>();

        int attempts = 2;
        int maxVal = dishes.allOrderableDishes.Length;
        string orderList = "";

        dishes = GameObject.FindAnyObjectByType<Dishes>();

        for (var i = 0; i < Random.Range(1, attempts + 1); i++)
        {
            orderedMeals.Add(dishes.getOrderableDish(Random.Range(1, maxVal)));
        }

        foreach (var item in orderedMeals)
        {
            orderList += (item.ToString() + '\t');
        }

        Debug.Log(name + " has ordered " + orderList);
    }




}
