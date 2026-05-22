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
    PlayerReasoning playerReasoning;
    void Awake()
    {
        // leftHand = GameObject.FindGameObjectWithTag("invL");
        // LHMeshRenderer = leftHand.GetComponent<MeshRenderer>();
        // LHDishes = leftHand.GetComponent<Dishes>();
        rightHand = GameObject.FindGameObjectWithTag("invR");
        RHMeshRenderer = rightHand.GetComponent<MeshRenderer>();
        RHDishes = rightHand.GetComponent<Dishes>();
        RHfoodStates = rightHand.GetComponent<foodStates>();
        //playerReasoning = FindAnyObjectByType<//playerReasoning>();

    }
    public string Interact(Collider col)
    {

        // Dishes LHDish = leftHand.GetComponent<Dishes>();
        Dishes RHDish = rightHand.GetComponent<Dishes>();
        bool hasValidItemLeftHand = false;
        bool hasValidItemRightHand = false;

        // if (col.TryGetComponent<AllowedItems>(out var LHAllowed))
        // hasValidItemLeftHand = LHAllowed.queryDish(LHDish.currentDish);

        bool isPOS = col.transform.childCount > 0 && col.transform.GetChild(0).TryGetComponent<UpdatePOSitem>(out var updatePOSitem);
        bool isRaw = RHfoodStates.currentState == foodStates.State.Raw;
        bool isBasin = this.tag == "noChange" && RHfoodStates.currentState == foodStates.State.UnWashed;
        bool isTrashCan = this.tag == "trashBin";
        bool canGiveToPOS = isPOS && (isRaw || isBasin);

        bool isCustomer = col.transform.childCount > 0;
        bool isFoodDone = RHfoodStates.currentState == foodStates.State.Done;
        bool canServeCustomerFood = isCustomer && isFoodDone;

        if (col.TryGetComponent<AllowedItems>(out var RHAllowed))
            hasValidItemRightHand = RHAllowed.queryDish(RHDish.currentDish);


        bool canGiveItem = hasValidItemLeftHand || hasValidItemRightHand;
        if (canGiveItem)
        {

            if (RHMeshRenderer.enabled)
            {
                // If there is a child that has "UpdatePosItem, then you are interacting with a Point of Service.
                if (isPOS || isTrashCan)
                {
                    if (canGiveToPOS)
                    {
                        col.transform.GetChild(0).GetComponent<UpdatePOSitem>().AddItem(RHDish.currentDish, RHfoodStates.currentState);
                        RHMeshRenderer.enabled = false;
                        RHDishes.setDish(RHDishes.getDish(0));
                        canGiveItem = false;
                    }
                    else if (isTrashCan)
                    {
                        RHMeshRenderer.enabled = false;
                        RHDishes.setDish(RHDishes.getDish(0));
                        canGiveItem = false;
                        this.gameObject.GetComponent<AudioSource>().Play();

                    }
                    else
                    {
                        //playerReasoning.StopAllCoroutines();
                        //playerReasoning.StartCoroutine(//playerReasoning.showDialuogeue("I need to wash this food first. Let me read the posters"));
                        Debug.Log("ITEM NOT READY TO BE GIVEN");
                        return null;
                    }
                }
                else if (canServeCustomerFood && col.TryGetComponent<RemoveCustomerPreviews>(out RemoveCustomerPreviews removeCustomerPreviews))
                {

                    removeCustomerPreviews.RemovePreview(RHDish.currentDish);
                    RHMeshRenderer.enabled = false;
                    RHDishes.setDish(RHDishes.getDish(0));
                    canGiveItem = false;

                }
                else
                {
                    //playerReasoning.StopAllCoroutines();
                    //playerReasoning.StartCoroutine(//playerReasoning.showDialuogeue("I need to prep this food first. I can't give the customer raw food!"));
                }
                return null;
            }
            else
            {
                //playerReasoning.StopAllCoroutines();
                //playerReasoning.StartCoroutine(//playerReasoning.showDialuogeue("I need to pick up the trash first!"));
            }
            // }if (LHMeshRenderer.enabled)
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
        // else
        //playerReasoning.StartCoroutine(playerReasoning.showDialuogeue("I can't do this yet, let me read the posters."));
        return null;
    }
}
