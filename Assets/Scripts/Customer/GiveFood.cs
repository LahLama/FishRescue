using Unity.VisualScripting;
using UnityEngine;

public class GiveFood : MonoBehaviour, IInteractable
{
    // GameObject leftHand;
    GameObject rightHand;
    // Dishes LHDishes;
    Dishes RHDishes;
    MeshRenderer RHMeshRenderer;
    // MeshRenderer LHMeshRenderer;
    foodStates RHfoodStates;
    void Awake()
    {
        // leftHand = GameObject.FindGameObjectWithTag("invL");
        // LHMeshRenderer = leftHand.GetComponent<MeshRenderer>();
        // LHDishes = leftHand.GetComponent<Dishes>();
        rightHand = GameObject.FindGameObjectWithTag("invR");
        RHMeshRenderer = rightHand.GetComponent<MeshRenderer>();
        RHDishes = rightHand.GetComponent<Dishes>();
        RHfoodStates = rightHand.GetComponent<foodStates>();

    }
    public string Interact(Collider col)
    {

        // Dishes LHDish = leftHand.GetComponent<Dishes>();
        Dishes RHDish = rightHand.GetComponent<Dishes>();
        bool hasValidItemLeftHand = false;
        bool hasValidItemRightHand = false;

        // if (col.TryGetComponent<AllowedItems>(out var LHAllowed))
        // hasValidItemLeftHand = LHAllowed.queryDish(LHDish.currentDish);

        if (col.TryGetComponent<AllowedItems>(out var RHAllowed))
            hasValidItemRightHand = RHAllowed.queryDish(RHDish.currentDish);


        bool canGiveItem = hasValidItemLeftHand || hasValidItemRightHand;
        if (canGiveItem)
        {

            if (RHMeshRenderer.enabled)
            {
                // If there is a child that has "UpdatePosItem, then you are interacting with a Point of Service.
                if (col.transform.childCount > 0 && col.transform.GetChild(0).TryGetComponent<UpdatePOSitem>(out var updatePOSitem))
                {
                    if (RHfoodStates.currentState == foodStates.State.Raw || (this.tag == "noChange" && RHfoodStates.currentState == foodStates.State.UnWashed))
                        updatePOSitem.AddItem(RHDish.currentDish, RHfoodStates.currentState);
                    else
                        return null;

                }
                if (col.transform.childCount > 0 && col.TryGetComponent<RemoveCustomerPreviews>(out var removeCustomerPreviews))
                {
                    removeCustomerPreviews.RemovePreview(RHDish.currentDish);
                }

                RHMeshRenderer.enabled = false;
                RHDishes.setDish(RHDishes.getDish(0));
                canGiveItem = false;
                return null;
            }
            // if (LHMeshRenderer.enabled)
            // {
            //     if (col.transform.childCount > 0 && col.transform.GetChild(0).TryGetComponent<UpdatePOSitem>(out var updatePOSitem))
            //     {
            //         updatePOSitem.AddItem(RHDish.currentDish, RHfoodStates.currentState);
            //     }
            //     LHMeshRenderer.enabled = false;
            //     LHDishes.setDish(LHDishes.getDish(0));
            //     canGiveItem = false;
            //     return null;
            // }
        }
        return null;
    }
}
