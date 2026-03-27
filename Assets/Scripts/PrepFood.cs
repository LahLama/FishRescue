using System.Collections;
using UnityEngine;

public class PrepFood : MonoBehaviour
{
    UpdatePOSitem updatePOSitem;
    float PrepTime;
    public float originalPrepTime = 20f;


    void Awake()
    {
        updatePOSitem = GetComponent<UpdatePOSitem>();
        this.enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(PrepCountdown());
    }

    IEnumerator PrepCountdown()
    {
        PrepTime = originalPrepTime;


        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Preping);

        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Done);
        Debug.Log("Prep done!");

        PrepTime = originalPrepTime;
        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }

        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Trash);
        Debug.Log("Item is trashed!");
    }

    void OnDisable()
    {
        StopCoroutine(PrepCountdown());
        PrepTime = originalPrepTime;
    }

}
