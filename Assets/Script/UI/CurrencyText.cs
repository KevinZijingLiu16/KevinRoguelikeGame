using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrencyText : MonoBehaviour
{
    [Header("Settings")]
    private TextMeshProUGUI text;

    public void UpdateText(string currency)
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        text.text = currency.ToString();
    }

}
