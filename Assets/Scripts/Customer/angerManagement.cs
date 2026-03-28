using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class angerManagement : MonoBehaviour
{
    public GameObject plane;
    public GameObject bar;
    float angerOneFour;
    float angerTwoFour;
    float angerThreeFour;
    public float angerStepTime;

    public GameObject veryCalmParticles;
    public GameObject calmParticles;
    public GameObject angryParticles;
    public GameObject veryAngryParticles;
    public GameObject sucessParticles;

    public enum AngerLevel
    {
        VeryCalm = 150,
        Calm = 125,
        Angry = 100,
        VeryAngry = 75
    }

    public AngerLevel currentAngerLevel;

    public void startAnger()
    {
        plane.SetActive(true);
        bar.SetActive(true);

        float angerLevel = bar.transform.localScale.x;
        angerOneFour = (1f / 4f) * angerLevel;
        angerTwoFour = (2f / 4f) * angerLevel;
        angerThreeFour = (3f / 4f) * angerLevel;

        // Debug.Log(angerOneFour + " " + angerTwoFour + " " + angerThreeFour + " " + angerLevel);

        StartCoroutine(angerTimer());
    }
    public IEnumerator angerTimer()
    {
        if (bar.transform.localScale.x > 0)
        {
            switch (bar.transform.localScale.x)
            {
                case float n when (n <= 0):
                    Debug.Log("Customer is gone");

                    break;
                case float n when (n <= angerOneFour && n > 0):
                    angryParticles.SetActive(false);
                    veryAngryParticles.SetActive(true);
                    break;
                case float n when (n > angerOneFour && n <= angerTwoFour):
                    currentAngerLevel = AngerLevel.Angry;
                    // Debug.Log("Customer is getting angry");
                    calmParticles.SetActive(false);
                    angryParticles.SetActive(true);
                    break;
                case float n when (n > angerTwoFour && n <= angerThreeFour):
                    currentAngerLevel = AngerLevel.Calm;
                    // Debug.Log("Customer is calm");
                    veryCalmParticles.SetActive(false);
                    calmParticles.SetActive(true);
                    break;
                default:
                    currentAngerLevel = AngerLevel.VeryCalm;
                    // Debug.Log("Customer is very calm");
                    veryCalmParticles.SetActive(true);
                    break;
            }


            yield return new WaitForSeconds(angerStepTime);
            bar.transform.localScale = new Vector3(bar.transform.localScale.x - 0.1f, bar.transform.localScale.y, bar.transform.localScale.z);
            if (bar.transform.localScale.x < 0)
            {
                bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);
                this.transform.parent.gameObject.SetActive(false);
            }
            StartCoroutine(angerTimer());
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
    }
}
