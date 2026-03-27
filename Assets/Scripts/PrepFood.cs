using System.Collections;
using UnityEngine;

public class PrepFood : MonoBehaviour
{
    UpdatePOSitem updatePOSitem;
    float PrepTime;
    public float originalPrepTime = 20f;
    SetFoodMaterials setFoodMaterials;

    void Awake()
    {
        updatePOSitem = GetComponent<UpdatePOSitem>();
        setFoodMaterials = FindAnyObjectByType<SetFoodMaterials>();
        this.enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(RawCountdown());
    }
    IEnumerator RawCountdown()
    {
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
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Done);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

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

        Debug.Log("Item is trashed!");
    }

    void OnDisable()
    {
        StopAllCoroutines();
        PrepTime = originalPrepTime;
    }

}
