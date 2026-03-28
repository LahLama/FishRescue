using System.Collections;
using UnityEngine;

public class PrepFood : MonoBehaviour
{
    UpdatePOSitem updatePOSitem;
    float PrepTime;
    public float originalPrepTime = 20f;
    SetFoodMaterials setFoodMaterials;
    public GameObject notReady;
    public GameObject ready;
    public GameObject trash;
    public ShakePOS shakePOS;

    void Awake()
    {
        updatePOSitem = GetComponent<UpdatePOSitem>();
        setFoodMaterials = FindAnyObjectByType<SetFoodMaterials>();
        this.enabled = false;
        shakePOS.enabled = false;
    }

    void OnEnable()
    {

        StartCoroutine(RawCountdown());
    }
    IEnumerator RawCountdown()
    {
        updatePOSitem.colliderItem.enabled = true; // Enable the collider when food is still raw, allowing it to be interacted with (e.g., thrown in the trash)

        notReady.SetActive(true);
        ready.SetActive(false);
        trash.SetActive(false);
        shakePOS.enabled = true;
        PrepTime = originalPrepTime / 2;


        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Raw);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }



        Debug.Log("Raw Done!");
        updatePOSitem.colliderItem.enabled = false; // Disable the collider while food is being prepped, preventing it from being thrown in the trash during the prep phase

        StartCoroutine(PrepCountdown());
    }

    IEnumerator PrepCountdown()
    {
        PrepTime = originalPrepTime;

        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Preping);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        notReady.SetActive(false);
        ready.SetActive(true);
        trash.SetActive(false);

        updatePOSitem.colliderItem.enabled = true; // Re-enable the collider when food is ready
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Done);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);
        shakePOS.enabled = false;
        Debug.Log("Prep done!");

        StartCoroutine(TrashCoolDown());
    }

    IEnumerator TrashCoolDown()
    {

        PrepTime = originalPrepTime;
        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }

        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Trash);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        notReady.SetActive(false);
        ready.SetActive(false);
        trash.SetActive(true);

        Debug.Log("Item is trashed!");
    }

    void OnDisable()
    {
        // if the player removes the item during the raw phase, stop the coroutine and disable the shake effect
        shakePOS.enabled = false;
        notReady.SetActive(false);
        ready.SetActive(false);
        trash.SetActive(false);
        StopAllCoroutines();
        PrepTime = originalPrepTime;
    }

}
