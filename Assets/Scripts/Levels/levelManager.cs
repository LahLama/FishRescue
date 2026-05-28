using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public RawImage transitionScene;
    public Button nextLevelButton;
    GameObject nextLevel;
    GameObject currentLevel;
    public void ShowScreenAndButtons(GameObject current, GameObject next, bool showNextBtn)
    {
        current.SetActive(false);
        this.GetComponent<DaySummary>().UpdateList();

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(showNextBtn);

        transitionScene.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        nextLevel = next;
        currentLevel = current;

    }


    public void GoToNextLevel()
    {
        transitionScene.gameObject.SetActive(false);
        nextLevel.SetActive(true);
        FindAnyObjectByType<CustomerSpawning>().Start();
        Cursor.lockState = CursorLockMode.Locked;


    }

    public void RetryCurrentLevel()
    {
        transitionScene.gameObject.SetActive(false);
        currentLevel.SetActive(true);
        FindAnyObjectByType<CustomerSpawning>().Start();
        Cursor.lockState = CursorLockMode.Locked;

    }
}