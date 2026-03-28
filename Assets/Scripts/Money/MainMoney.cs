using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMoney : MonoBehaviour
{
    float money = 0f;
    public void AddMoney(float amount)
    {
        money += amount;
        this.GetComponent<TextMeshProUGUI>().text = "R" + money.ToString();
    }
}
