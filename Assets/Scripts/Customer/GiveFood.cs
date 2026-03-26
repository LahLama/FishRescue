using UnityEngine;

public class GiveFood : MonoBehaviour, IInteractable
{
    GameObject leftHand;
    GameObject rightHand;
    Dishes LHDishes;
    Dishes RHDishes;
    MeshRenderer RHMeshRenderer;
    MeshRenderer LHMeshRenderer;
    void Awake()
    {
        leftHand = GameObject.FindGameObjectWithTag("invL");
        LHMeshRenderer = leftHand.GetComponent<MeshRenderer>();
        LHDishes = leftHand.GetComponent<Dishes>();
        rightHand = GameObject.FindGameObjectWithTag("invR");
        RHMeshRenderer = rightHand.GetComponent<MeshRenderer>();
        RHDishes = rightHand.GetComponent<Dishes>();

    }
    public string Interact(Collider col)
    {
        if (RHMeshRenderer.enabled)
        {
            RHMeshRenderer.enabled = false;
            RHDishes.setDish(RHDishes.getDish(0));
            return null;
        }
        if (LHMeshRenderer.enabled)
        {
            LHMeshRenderer.enabled = false;
            LHDishes.setDish(LHDishes.getDish(0));
            return null;
        }
        return null;
    }
}
