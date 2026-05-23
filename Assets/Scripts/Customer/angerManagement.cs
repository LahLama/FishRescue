using System.Collections;
using UnityEngine;

public class angerManagement : MonoBehaviour
{
    public GameObject plane;
    public GameObject bar;
    float angerOneFour;
    float angerTwoFour;
    float angerThreeFour;
    private float angerStepTime;

    public GameObject veryCalmParticles;
    public GameObject calmParticles;
    public GameObject angryParticles;
    public GameObject veryAngryParticles;
    public GameObject sucessParticles;
    public Material veryCalmMat;
    public Material calmMat;
    public Material angryMat;
    public Material veryAngryMat;

    public CustomerSounds customerSounds;
    bool hasChangedAngerLevel = false;

    public levelTemplate levelTemplate;
    public enum AngerLevel
    {
        VeryCalm = 150,
        Calm = 125,
        Angry = 100,
        VeryAngry = 75
    }

    public float angerVal;
    public AngerLevel currentAngerLevel;
    public AngerLevel previousAngerLevel;

    void Start()
    {
        levelTemplate = FindObjectsByType<levelTemplate>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];
        Debug.Log("Level: " + levelTemplate.level);
    }

    public void startAnger()
    {
        customerSounds.audioSource = this.GetComponentInParent<AudioSource>();
        angerStepTime = levelTemplate.angerStep;
        angerVal = bar.transform.localScale.x;
        angerOneFour = (1f / 4f) * angerVal;
        angerTwoFour = (2f / 4f) * angerVal;
        angerThreeFour = (3f / 4f) * angerVal;


        if (this.gameObject.activeInHierarchy)
            StartCoroutine(angerTimer());
    }
    public IEnumerator angerTimer()
    {
        plane.SetActive(true);
        bar.SetActive(true);


        currentAngerLevel = AngerLevel.VeryCalm;
        previousAngerLevel = AngerLevel.VeryCalm; // Match so no change fires on start
        ApplyAngerLevelEffects(currentAngerLevel); // Explicitly trigger the first state once
        customerSounds.PlaySound("veryCalm", false);

        while (bar.transform.localScale.x > 0)
        {
            float n = bar.transform.localScale.x;

            // Determine level - no effects here, just assignment
            if (n <= angerOneFour && n > 0)
                currentAngerLevel = AngerLevel.VeryAngry;
            else if (n > angerOneFour && n <= angerTwoFour)
                currentAngerLevel = AngerLevel.Angry;
            else if (n > angerTwoFour && n <= angerThreeFour)
                currentAngerLevel = AngerLevel.Calm;
            else if (n > angerThreeFour && n <= angerVal)
                currentAngerLevel = AngerLevel.VeryCalm;



            // Only trigger effects on state change
            if (currentAngerLevel != previousAngerLevel)
            {
                hasChangedAngerLevel = true;
                previousAngerLevel = currentAngerLevel;
                ApplyAngerLevelEffects(currentAngerLevel);
            }
            else
            {
                hasChangedAngerLevel = false;
            }

            yield return new WaitForSeconds(angerStepTime);

            // Bar handling
            bar.transform.localScale = new Vector3(bar.transform.localScale.x - 0.1f, bar.transform.localScale.y, bar.transform.localScale.z);
            if (bar.transform.localScale.x < 0)
            {
                bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);

                // 2 second delay for voice line to play
                Invoke("stopAnger", 2);
                customerSounds.PlaySound("leaving");
                Debug.Log("player has failed to serve");
            }
        }
    }

    void ApplyAngerLevelEffects(AngerLevel level)
    {
        // Reset all particles first
        veryCalmParticles.SetActive(false);
        calmParticles.SetActive(false);
        angryParticles.SetActive(false);
        veryAngryParticles.SetActive(false);

        switch (level)
        {
            case AngerLevel.VeryAngry:
                veryAngryParticles.SetActive(true);
                bar.GetComponent<MeshRenderer>().material = veryAngryMat;
                customerSounds.PlaySound("veryAngry", false);
                break;
            case AngerLevel.Angry:
                angryParticles.SetActive(true);
                bar.GetComponent<MeshRenderer>().material = angryMat;
                customerSounds.PlaySound("angry", false);
                break;
            case AngerLevel.Calm:
                calmParticles.SetActive(true);
                bar.GetComponent<MeshRenderer>().material = calmMat;
                customerSounds.PlaySound("calm", false);
                break;
            case AngerLevel.VeryCalm:
                veryCalmParticles.SetActive(true);
                bar.GetComponent<MeshRenderer>().material = veryCalmMat;
                customerSounds.PlaySound("veryCalm", false);
                break;
        }
    }

    public void stopAnger()
    {

        sucessParticles.SetActive(true);
        StopAllCoroutines();
        plane.SetActive(false);
        bar.SetActive(false);
        bar.transform.localScale = new Vector3(1f, bar.transform.localScale.y, bar.transform.localScale.z);
        veryCalmParticles.SetActive(false);
        calmParticles.SetActive(false);
        angryParticles.SetActive(false);
        veryAngryParticles.SetActive(false);
        levelTemplate.completedCustomers++;
        Debug.Log("levelTemplate.completedCustomers: " + levelTemplate.completedCustomers);

        this.transform.parent.gameObject.SetActive(false);

    }
}
