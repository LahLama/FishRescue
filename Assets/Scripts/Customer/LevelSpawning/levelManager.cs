using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public RawImage transitionScene;

    public void TransitionToLevel(GameObject current, GameObject next)
    {

        current.SetActive(false);
        transitionScene.gameObject.SetActive(true);
        StartCoroutine(GoToNextLevelAfterWait(next));
    }

    IEnumerator GoToNextLevelAfterWait(GameObject next)
    {
        yield return new WaitForSeconds(5f);


        transitionScene.gameObject.SetActive(false);
        next.SetActive(true);
        FindAnyObjectByType<CustomerSpawning>().Start();
    }
}