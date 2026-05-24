using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMoney : MonoBehaviour
{
    public float money = 0f;
    public AudioSource audioSource;
    public void AddMoney(float amount)
    {
        money += amount;
        // Money Sound
        audioSource.Play();
        this.GetComponent<TextMeshProUGUI>().text = "R" + money.ToString();
    }
}
