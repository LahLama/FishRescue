using UnityEngine;
using Unity.VisualScripting;
using System;


public class Dishes : MonoBehaviour
{
    public orderableDish currentDish = orderableDish.placeholder;
    public int utilCount = 2;

    public orderableDish[] allDishes = (orderableDish[])Enum.GetValues(typeof(orderableDish));
    public void setDish(orderableDish newDish)
    {
        currentDish = newDish;
    }

    public string getDish(int index)
    {
        return allDishes[index].ToString();
    }

    public enum Dish
    {
        placeholder,
        //dish
        raw_chicken,
        cooked_chicken,
        raw_beef,
        cooked_beef,
        raw_potatoes,
        potato_salad,
        raw_salad,
        green_salad,

        //utilities
        wood,
        briquettes
    }

    public enum orderableDish
    {
        placeholder,
        //dish
        cooked_beef,
        potato_salad,
        green_salad,

    }

}

