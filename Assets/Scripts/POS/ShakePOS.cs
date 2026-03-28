using UnityEngine;

public class ShakePOS : MonoBehaviour
{
    void Update()
    {
        this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, 90, Mathf.Sin(Time.time * 10) * 5);
    }

    void OnDisable()
    {
        this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, 90, 0);
    }
}
