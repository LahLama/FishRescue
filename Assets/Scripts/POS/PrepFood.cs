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
    SoundPlayer soundPlayer;
    AudioSource audioSource;

    SoundsManager soundsManager;

    void Awake()
    {
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        soundsManager = FindAnyObjectByType<SoundsManager>();
        audioSource = GetComponentInParent<AudioSource>();
        updatePOSitem = GetComponent<UpdatePOSitem>();
        setFoodMaterials = FindAnyObjectByType<SetFoodMaterials>();
        this.enabled = false;
        shakePOS.enabled = false;
    }

    void OnEnable()
    {
        if (this.tag == "noChange")
        {
            StartCoroutine(WashCountdown());
            // Debug.Log("Washing item!");
            return;
        }
        else
            StartCoroutine(RawCountdown());
    }

    void PlaySound(string clipName, bool loop = true)
    {
        AudioClip clip = soundsManager.GetClipByName(clipName);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            soundPlayer.PlaySound(audioSource);
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in SoundsManager.");
        }
    }
    IEnumerator WashCountdown()
    {
        //play the sound that is attached to the parent object (the POS counter) when the washing process starts, and stop it when the washing process ends
        PlaySound("wash", true);

        PrepTime = originalPrepTime / 2;
        shakePOS.enabled = true;
        updatePOSitem.colliderItem.enabled = false;

        if (setFoodMaterials == null)
        {
            Debug.Log("ABC");
        }
        if (updatePOSitem == null)
        {
            Debug.Log("DEF");
        }


        //Replace the current item with the same dish, just a differnt state
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Raw);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);

        while (PrepTime > 0 && this.enabled)
        {
            PrepTime -= Time.deltaTime;
            yield return null; // waits one frame, then continues
        }
        shakePOS.enabled = false;
        updatePOSitem.colliderItem.enabled = true;
        ready.SetActive(true);
        soundPlayer.StopSound(audioSource);

        PlaySound("customer_arrive", false);

        //Debug.Log("Wash Done!");

    }

    IEnumerator RawCountdown()
    {
        updatePOSitem.colliderItem.enabled = true; // Enable the collider when food is still raw, allowing it to be interacted with (e.g., thrown in the trash)
        soundPlayer.PlaySound(audioSource);
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



        //Debug.Log("Raw Done!");
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
        endPrep();
    }

    private void endPrep()
    {
        notReady.SetActive(false);
        ready.SetActive(true);
        trash.SetActive(false);

        updatePOSitem.colliderItem.enabled = true; // Re-enable the collider when food is ready
        updatePOSitem.AddItem(updatePOSitem.dish, foodStates.State.Done);
        setFoodMaterials.SetFoodMaterial(updatePOSitem.gameObject, updatePOSitem.state, updatePOSitem.dish);
        shakePOS.enabled = false;

        soundPlayer.StopSound(audioSource);
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
