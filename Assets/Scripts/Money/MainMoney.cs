using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMoney : MonoBehaviour
{
    public float money = 0f;
    public AudioSource audioSource;
    public TextMeshProUGUI moneyText;
    private DaySummary daySummary;

    void Start()
    {
        daySummary = FindAnyObjectByType<DaySummary>();
        moneyText = this.GetComponent<TextMeshProUGUI>();
    }
    public void AddMoney(float amount)
    {
        money += amount;
        // Money Sound
        if (amount > 0)
        {
            FindAnyObjectByType<SoundsManager>().PlaySound("money", false, audioSource);
            daySummary.dayStats["MoneyGained"] = (int)money;
            Debug.Log(daySummary.dayStats["MoneyGained"]);
            moneyText.text = "R" + money.ToString();
        }
    }
}
