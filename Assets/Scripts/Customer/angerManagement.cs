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
                case float n when (n <= angerOneFour):
                    // Debug.Log("Customer is very angry");
                    angryParticles.SetActive(false);
                    veryAngryParticles.SetActive(true);
                    break;
                case float n when (n > angerOneFour && n <= angerTwoFour):
                    // Debug.Log("Customer is getting angry");
                    calmParticles.SetActive(false);
                    angryParticles.SetActive(true);
                    break;
                case float n when (n > angerTwoFour && n <= angerThreeFour):
                    // Debug.Log("Customer is calm");
                    veryCalmParticles.SetActive(false);
                    calmParticles.SetActive(true);
                    break;
                default:
                    // Debug.Log("Customer is very calm");
                    veryCalmParticles.SetActive(true);
                    break;
            }


            yield return new WaitForSeconds(angerStepTime);
            bar.transform.localScale = new Vector3(bar.transform.localScale.x - 0.1f, bar.transform.localScale.y, bar.transform.localScale.z);
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
