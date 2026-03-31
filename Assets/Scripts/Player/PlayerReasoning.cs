using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerReasoning : MonoBehaviour
{
    public TextMeshProUGUI ReasonText;
    public GameObject dialogueBox;

    public IEnumerator showDialuogeue(string reason)
    {
        dialogueBox.SetActive(true);
        ReasonText.text = "Me: " + reason;
        yield return new WaitForSeconds(3f);
        dialogueBox.SetActive(false);
    }

}
