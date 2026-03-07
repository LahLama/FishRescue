using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI money;
    int moneyAmt = 0;

    public void incMoney(int amt)
    {
        moneyAmt += amt;
        money.text = $" ${moneyAmt}";
    }

}
