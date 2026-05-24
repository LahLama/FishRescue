using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    AudioSource audioSource;

    SoundsManager soundsManager;
    public TextMeshPro timerText;



    void Awake()
    {

        soundsManager = FindAnyObjectByType<SoundsManager>();
        audioSource = GetComponentInParent<AudioSource>();
        updatePOSitem = GetComponent<UpdatePOSitem>();
        setFoodMaterials = FindAnyObjectByType<SetFoodMaterials>();
        this.enabled = false;
        shakePOS.enabled = false;
    }

    void OnEnable()
    {
        timerText.text = "";
        if (this.tag == "noChange")
        {
            StartCoroutine(WashCountdown());
            // Debug.Log("Washing item!");
            return;
        }
        else if (this.tag == "counter")
        {
            counterStore();
        }
        else
            StartCoroutine(RawCountdown());
    }








    void counterStore()
    {
        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, updatePOSitem.state);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);
        timerText.text = "";
        updatePOSitem.colliderItem.enabled = true;
    }


    IEnumerator WashCountdown()
    {
        //play the sound that is attached to the parent object (the POS counter) when the washing process starts, and stop it when the washing process ends
        soundsManager.PlaySound("wash", true, audioSource);

        PrepTime = originalPrepTime / 2;
        shakePOS.enabled = true;
        updatePOSitem.colliderItem.enabled = false;




        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Raw);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            timerText.text = Mathf.Ceil(PrepTime).ToString();
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        timerText.text = "";
        shakePOS.enabled = false;
        updatePOSitem.colliderItem.enabled = true;
        ready.SetActive(true);

        soundsManager.PlaySound("washEnd", false, audioSource);

        //Debug.Log("Wash Done!");

    }

    IEnumerator RawCountdown()  //this is like the first phase of washing but for meat
    {// Enable the collider when food is still raw, allowing it to be interacted with (e.g., thrown in the trash)

        updatePOSitem.colliderItem.enabled = true;


        notReady.SetActive(true);
        ready.SetActive(false);
        trash.SetActive(false);
        shakePOS.enabled = true;

        PrepTime = originalPrepTime / 4;

        if (this.CompareTag("potatoeStand"))
        {
            soundsManager.PlaySound("chopStart", false, audioSource);
        }
        else if (this.CompareTag("saladStand"))
        {
            soundsManager.PlaySound("chopStart", false, audioSource);
        }
        else if (this.CompareTag("braai"))
        {
            soundsManager.PlaySound("braaiStart", false, audioSource);
        }


        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Raw);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            timerText.text = Mathf.Ceil(PrepTime).ToString();
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }



        //Debug.Log("Raw Done!");
        updatePOSitem.colliderItem.enabled = false; // Disable the collider while food is being prepped, preventing it from being thrown in the trash during the prep phase

        StartCoroutine(PrepCountdown());
    }

    IEnumerator PrepCountdown()
    {
        PrepTime = originalPrepTime;

        if (this.CompareTag("potatoeStand"))
        {
            soundsManager.PlaySound("chop", true, audioSource);
        }
        else if (this.CompareTag("saladStand"))
        {
            soundsManager.PlaySound("chop", true, audioSource);
        }
        else if (this.CompareTag("braai"))
        {
            soundsManager.PlaySound("braai", true, audioSource);
        }

        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Preping);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            timerText.text = Mathf.Ceil(PrepTime).ToString();
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        endPrep();
    }

    private void endPrep()
    {
        notReady.SetActive(false);
        ready.SetActive(true);
        trash.SetActive(false);

        if (this.CompareTag("potatoeStand"))
        {
            soundsManager.PlaySound("chopEnd", false, audioSource);
        }
        else if (this.CompareTag("saladStand"))
        {
            soundsManager.PlaySound("chopEnd", false, audioSource);
        }
        else if (this.CompareTag("braai"))
        {
            soundsManager.PlaySound("braaiEnd", false, audioSource);
        }


        updatePOSitem.colliderItem.enabled = true; // Re-enable the collider when food is ready
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Done);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);
        shakePOS.enabled = false;
        timerText.text = "";

        StartCoroutine(TrashCoolDown());
    }
    IEnumerator TrashCoolDown()
    {

        PrepTime = originalPrepTime;
        while (PrepTime > 0 && this.enabled)
        {
            timerText.text = Mathf.Ceil(PrepTime).ToString();
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        soundsManager.PlaySound("trash", false, audioSource);
        timerText.text = "";
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Trash);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        notReady.SetActive(false);
        ready.SetActive(false);
        trash.SetActive(true);

        //Debug.Log("Item is trashed!");
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
