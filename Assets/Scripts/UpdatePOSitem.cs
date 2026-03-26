using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UpdatePOSitem : MonoBehaviour, IInteractable
{
    public MeshRenderer visualItem;
    public Collider colliderItem;
    public Dishes.Dish dish;
    public foodStates.State state;
    getState getState;
    public void AddItem(Dishes.Dish newDish, foodStates.State newState)
    {
        visualItem.enabled = true;
        colliderItem.enabled = true;
        this.dish = newDish;
        this.state = newState;
        getState = GetComponent<getState>();
    }

    public string Interact(Collider col)
    {
        RemoveItem();
        getState.Interact(col);
        return null;
    }

    public void RemoveItem()
    {
        visualItem.enabled = false;
        colliderItem.enabled = false;
        this.dish = Dishes.Dish.placeholder;
        this.state = foodStates.State.Raw;
    }

    void Awake()
    {
        RemoveItem();
        visualItem = GetComponent<MeshRenderer>();
        colliderItem = GetComponent<Collider>();
    }
}
