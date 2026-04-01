using UnityEngine;
using System;


public class Dishes : MonoBehaviour
{
    public Dish currentDish = Dish.placeholder;


    // public OrderableDish[] allOrderableDishes = (OrderableDish[])Enum.GetValues(typeof(OrderableDish));
    public Dish[] allDishes = (Dish[])Enum.GetValues(typeof(Dish));


    public void setDish(Dish newDish)
    {
        currentDish = newDish;
    }

    public Dish getDish(int index)
    {
        return allDishes[index];
    }

    // public OrderableDish getOrderableDish(int index)
    // {
    //     return allOrderableDishes[index];
    // }

    public enum Dish
    {
        placeholder,
        wood,
        briquettes,

        //dish
        chicken,
        beef,
        lamb,
        potato,
        salad,

    }

    // public enum OrderableDish
    // {
    //     placeholder,
    //     wood,
    //     briquettes,

    //     //dish
    //     chicken,
    //     beef,
    //     lamb,
    //     green_salad,

    // }

}

