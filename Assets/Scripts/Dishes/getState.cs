using TMPro;
using UnityEngine;

public class getState : MonoBehaviour, IInteractable
{
    // GameObject leftHand;
    GameObject rightHand;
    // Dishes LHDishes;
    Dishes RHDishes;
    // foodStates LHFoodStates;
    foodStates RHFoodStates;
    MeshRenderer RHMeshRenderer;
    // MeshRenderer LHMeshRenderer;
    void Awake()
    {
        // leftHand = GameObject.FindGameObjectWithTag("invL");
        // LHMeshRenderer = leftHand.GetComponent<MeshRenderer>();
        // LHDishes = leftHand.GetComponent<Dishes>();
        // LHFoodStates = leftHand.GetComponent<foodStates>();
        rightHand = GameObject.FindGameObjectWithTag("invR");
        RHMeshRenderer = rightHand.GetComponent<MeshRenderer>();
        RHDishes = rightHand.GetComponent<Dishes>();
        RHFoodStates = rightHand.GetComponent<foodStates>();
    }
    public string Interact(Collider col)
    {
        if (!RHMeshRenderer.enabled)
        {
            RHMeshRenderer.enabled = true;
            RHMeshRenderer.material = GetComponent<MeshRenderer>().material;
            RHFoodStates.SetState(col.GetComponent<foodStates>().currentState);
            RHDishes.setDish(col.GetComponent<Dishes>().currentDish);
            return null;
        }
        else
            return null;
    }
}
