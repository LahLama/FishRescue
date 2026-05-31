using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class setGoals : MonoBehaviour
{

    private levelTemplate levelTemplate;
    public GameObject panelbar;
    private RectTransform barRect;
    private MainMoney mainMoney;


    [SerializeField]
    private const float emptyTopOffset = 206f; // Matches your inspector value

    public void SetMoneyGoalsStart()
    {
        levelTemplate = FindObjectsByType<levelTemplate>(
            FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];

        this.GetComponent<TextMeshProUGUI>().text =
            "Money Goal: R" + levelTemplate.MoneyGoal +
            "\nPerfect Money Goal: R" + levelTemplate.PerfectMoneyGoal;
        mainMoney = FindAnyObjectByType<MainMoney>();
        barRect = panelbar.GetComponent<RectTransform>();

        //Reset the money count on a new day
        mainMoney.money = 0;
        mainMoney.AddMoney(-mainMoney.money);

        // Start empty - top offset at max
        barRect.offsetMax = new Vector2(barRect.offsetMax.x, -emptyTopOffset);
    }

    void Update()
    {
        if (barRect == null || levelTemplate == null) return;

        float progress = Mathf.Clamp01(
            (float)mainMoney.money / levelTemplate.MoneyGoal);

        // As progress goes 0 -> 1, topOffset goes 180 -> 0
        float targetTopOffset = emptyTopOffset * (1f - progress);

        float currentTopOffset = -barRect.offsetMax.y;
        float newTopOffset = Mathf.Lerp(currentTopOffset, targetTopOffset, Time.deltaTime * 5f);

        barRect.offsetMax = new Vector2(barRect.offsetMax.x, -newTopOffset);
    }
}
