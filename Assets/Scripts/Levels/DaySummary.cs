using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaySummary : MonoBehaviour
{
    public TextMeshProUGUI daySum;

    public Dictionary<string, int> dayStats = new Dictionary<string, int>()
    {
        {"Day",0},
        {"Goal",0},
        {"PerfectGoal",0},
        {"MoneyGained",0},
        {"HappyCustomers",0},
        {"UpsetCustomers",0},
    };

    public void UpdateList()
    {
        string msg =
        "Day: " + dayStats["Day"] +
        "\n Goal: " + dayStats["Goal"] +
        "\n Perfect Goal: " + dayStats["PerfectGoal"] +
        "\n Money Gained: " + dayStats["MoneyGained"] +
        "\n Happy Customers: " + dayStats["HappyCustomers"] +
        "\n Upset Customers: " + dayStats["UpsetCustomers"]
        ;

        daySum.text = msg;
    }

    //Go to LevelManager for button Scripts


}
