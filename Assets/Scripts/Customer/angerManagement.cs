using System.Collections;
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
    public Material veryCalmMat;
    public Material calmMat;
    public Material angryMat;
    public Material veryAngryMat;

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


        float angerLevel = bar.transform.localScale.x;
        angerOneFour = (1f / 4f) * angerLevel;
        angerTwoFour = (2f / 4f) * angerLevel;
        angerThreeFour = (3f / 4f) * angerLevel;

        // Debug.Log(angerOneFour + " " + angerTwoFour + " " + angerThreeFour + " " + angerLevel);

        if (this.gameObject.activeInHierarchy)
            StartCoroutine(angerTimer());
    }
    public IEnumerator angerTimer()
    {
        plane.SetActive(true);
        bar.SetActive(true);

        if (bar.transform.localScale.x > 0)
        {
            switch (bar.transform.localScale.x)
            {
                case float n when (n <= 0):
                    Debug.Log("Customer is gone");

                    break;
                case float n when (n <= angerOneFour && n > 0):
                    currentAngerLevel = AngerLevel.VeryAngry;
                    angryParticles.SetActive(false);
                    veryAngryParticles.SetActive(true);
                    bar.GetComponent<MeshRenderer>().material = veryAngryMat;
                    break;
                case float n when (n > angerOneFour && n <= angerTwoFour):
                    currentAngerLevel = AngerLevel.Angry;
                    bar.GetComponent<MeshRenderer>().material = angryMat;
                    // Debug.Log("Customer is getting angry");
                    calmParticles.SetActive(false);
                    angryParticles.SetActive(true);
                    break;
                case float n when (n > angerTwoFour && n <= angerThreeFour):
                    currentAngerLevel = AngerLevel.Calm;
                    bar.GetComponent<MeshRenderer>().material = calmMat;
                    // Debug.Log("Customer is calm");
                    veryCalmParticles.SetActive(false);
                    calmParticles.SetActive(true);
                    break;
                default:
                    currentAngerLevel = AngerLevel.VeryCalm;
                    // Debug.Log("Customer is very calm");
                    veryCalmParticles.SetActive(true);
                    bar.GetComponent<MeshRenderer>().material = veryCalmMat;
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
