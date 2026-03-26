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

        Dishes LHDish = leftHand.GetComponent<Dishes>();
        Dishes RHDish = rightHand.GetComponent<Dishes>();
        bool hasValidItemLeftHand = false;
        bool hasValidItemRightHand = false;

        if (col.TryGetComponent<AllowedItems>(out var LHAllowed))
            hasValidItemLeftHand = LHAllowed.queryDish(LHDish.currentDish);

        if (col.TryGetComponent<AllowedItems>(out var RHAllowed))
            hasValidItemRightHand = RHAllowed.queryDish(RHDish.currentDish);


        bool canGiveItem = hasValidItemLeftHand || hasValidItemRightHand;
        if (canGiveItem)
        {
            if (RHMeshRenderer.enabled)
            {
                RHMeshRenderer.enabled = false;
                RHDishes.setDish(RHDishes.getDish(0));
                canGiveItem = false;
                return null;
            }
            if (LHMeshRenderer.enabled)
            {
                LHMeshRenderer.enabled = false;
                LHDishes.setDish(LHDishes.getDish(0));
                canGiveItem = false;
                return null;
            }
        }
        return null;
    }
}
