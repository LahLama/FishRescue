using System.Threading;
using TMPro;
using UnityEngine;

public class resetTimer : MonoBehaviour, IInteractable
{
    public TextMeshPro TimerGO;
    public string Interact(Collider col)
    {
        if (TimerGO != null)
            TimerGO.text = "";
        return null;
    }

}
