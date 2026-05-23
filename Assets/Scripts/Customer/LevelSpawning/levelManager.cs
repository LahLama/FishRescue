using UnityEngine;
using System.Collections;
public class LevelManager : MonoBehaviour
{
    public void TransitionToLevel(GameObject current, GameObject next)
    {
        current.SetActive(false);
        next.SetActive(true);
        FindAnyObjectByType<CustomerSpawning>().Start();
        
    }
}
