using UnityEngine;

public class FishNames : MonoBehaviour
{
    string[] fishNames =
    { "Bob", "Sandy","Patrick", "Krabs"
    ,"Emma","Jeff","Thor","Loki",
    "Fishy Mc FishFace" , "Swimmy McSwimFace", "Finny McFinFace"
    ,"Fin","Nemo","Dory",
    "Fin the Ultimate destroyer of all things not fish."

    };

    public string GetRandomName()
    {
        return fishNames[Random.Range(0, fishNames.Length)];
    }
}
