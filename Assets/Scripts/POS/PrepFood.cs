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

    float fadeDuration = 0.2f;
    Coroutine fadeCoroutine;

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



    void PlaySound(string clipName, bool loop = true)
    {
        AudioClip clip = soundsManager.GetClipByName(clipName);
        if (clip != null)
        {
            // Cancel any ongoing fade before starting a new one
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeToNewClip(clip, loop));
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in SoundsManager.");
        }
    }

    IEnumerator FadeToNewClip(AudioClip newClip, bool loop)
    {
        // Fade out the current clip if something is playing
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = 0f;
        }

        // Swap the clip and fade in
        audioSource.clip = newClip;
        audioSource.loop = loop;
        audioSource.Play();

        float fadeElapsed = 0f;
        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, fadeElapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
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
        PlaySound("wash", true);

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

        PlaySound("washEnd", false);

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
            PlaySound("chopStart", false);
        }
        else if (this.CompareTag("saladStand"))
        {
            PlaySound("chopStart", false);
        }
        else if (this.CompareTag("braai"))
        {
            PlaySound("braaiStart", false);
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
            PlaySound("chop", true);
        }
        else if (this.CompareTag("saladStand"))
        {
            PlaySound("chop", true);
        }
        else if (this.CompareTag("braai"))
        {
            PlaySound("braai", true);
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
            PlaySound("chopEnd", false);
        }
        else if (this.CompareTag("saladStand"))
        {
            PlaySound("chopEnd", false);
        }
        else if (this.CompareTag("braai"))
        {
            PlaySound("braaiEnd", false);
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
        PlaySound("trash", false);
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
