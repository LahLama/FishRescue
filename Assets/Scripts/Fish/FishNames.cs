using UnityEngine;

public class FishNames : MonoBehaviour
{
    public string[] fishNames =
    { "abe", "sandy","greg","doug","dougless","Emma","jeff","thor","loki"

    };

    public string GetRandomName()
    {
        return fishNames[Random.Range(0, fishNames.Length)];
    }
}
