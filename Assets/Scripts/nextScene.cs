using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    public void MoveToNextScene()
    {
        SceneManager.LoadScene("main");
    }
}
