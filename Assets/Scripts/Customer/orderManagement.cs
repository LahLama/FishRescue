using System.Collections.Generic;
using UnityEngine;

public class orderManagement : MonoBehaviour
{
    Dishes dishes;
    List<string> orderedMeals = new List<string>();

    void Awake()
    {
        dishes = GameObject.FindAnyObjectByType<Dishes>();

        int maxVal = dishes.allDishes.Length - dishes.utilCount;
        orderedMeals.Add(dishes.getDish(Random.Range(1, maxVal)));



        string orderList = "";

        for (var i = 0; i < Random.Range(1, 3); i++)
        {
            foreach (var item in orderedMeals)
            {
                orderList += (item + '\t');
            }
        }

        Debug.Log(name + " has ordered " + orderList);
    }




}
