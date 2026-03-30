using System;
using TMPro;
using UnityEngine;

public class AddMoneyVisual : MonoBehaviour
{
    void Awake()
    {
        this.GetComponent<TextMeshProUGUI>().text = "";
    }
    public void StartMoney(float amount)
    {
        this.GetComponent<TextMeshProUGUI>().text = "+R" + amount.ToString();
        StartCoroutine(MoveUpAndFade());
    }

    private System.Collections.IEnumerator MoveUpAndFade()
    {
        float duration = 1f; // Duration of the animation
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position; // Starting position
        Vector3 endPosition = startPosition + new Vector3(0, 10, 0); // Move up by 2 units
        Color startColor = GetComponent<TextMeshProUGUI>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); // Fade to transparent

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            GetComponent<TextMeshProUGUI>().color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and color are set
        transform.position = endPosition;
        GetComponent<TextMeshProUGUI>().color = endColor;
        EndMoney();
    }

    public void EndMoney()
    {
        this.GetComponent<TextMeshProUGUI>().text = ""; // Clear the text after animation
        transform.position -= new Vector3(0, 10, 0);    // Reset position for next time
        GetComponent<TextMeshProUGUI>().color = Color.white; // Reset color for next time

    }
}
