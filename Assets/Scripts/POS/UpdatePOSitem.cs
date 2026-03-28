using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UpdatePOSitem : MonoBehaviour, IInteractable
{
    public MeshRenderer visualItem;
    public Collider colliderItem;
    public Dishes.Dish dish;
    public foodStates.State state;

    public Dishes dishScript;
    public foodStates foodStatesScript;
    public PrepFood prepFood;
    getState getState;
    public void AddItem(Dishes.Dish newDish, foodStates.State newState)
    {
        this.dish = newDish;
        this.state = newState;

        dishScript.currentDish = newDish;
        foodStatesScript.currentState = newState;

        visualItem.enabled = true;

        prepFood.enabled = true;
        getState = GetComponent<getState>();


    }

    public string Interact(Collider col)
    {
        RemoveItem();
        return null;
    }

    public void RemoveItem()
    {

        visualItem.enabled = false;
        colliderItem.enabled = false;
        prepFood.enabled = false;
        this.state = foodStates.State.Raw;


    }

    void Awake()
    {
        RemoveItem();
        visualItem = GetComponent<MeshRenderer>();
        colliderItem = GetComponent<Collider>();

    }
}
