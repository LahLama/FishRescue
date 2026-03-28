using UnityEngine;

public class ShakePOS : MonoBehaviour
{
    void Update()
    {
        this.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 10) * 5);
    }

    void OnDisable()
    {
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
