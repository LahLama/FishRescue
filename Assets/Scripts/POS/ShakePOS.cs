using UnityEngine;

public class ShakePOS : MonoBehaviour
{
    // Localratation will result to a 0,0,0
    void Update()
    {
        Vector3 euler = this.transform.localEulerAngles;
        euler.z = Mathf.Sin(Time.time * 10) * 5;
        this.transform.localEulerAngles = euler;
    }

    void OnDisable()
    {
        Vector3 euler = this.transform.localEulerAngles;
        euler.z = 0;
        this.transform.localEulerAngles = euler;
    }
}
