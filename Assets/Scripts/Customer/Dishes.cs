using UnityEngine;
using Unity.VisualScripting;
using System;


public class Dishes : MonoBehaviour
{
    public Dish currentDish = Dish.placeholder;


    public OrderableDish[] allOrderableDishes = (OrderableDish[])Enum.GetValues(typeof(OrderableDish));
    public Dish[] allDishes = (Dish[])Enum.GetValues(typeof(Dish));


    public void setDish(Dish newDish)
    {
        currentDish = newDish;
    }

    public string getDish(int index)
    {
        return allDishes[index].ToString();
    }

    public string getOrderableDish(int index)
    {
        return allOrderableDishes[index].ToString();
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

    public enum OrderableDish
    {
        placeholder,
        //dish
        cooked_chicken,
        cooked_beef,
        potato_salad,
        green_salad,

    }

}

