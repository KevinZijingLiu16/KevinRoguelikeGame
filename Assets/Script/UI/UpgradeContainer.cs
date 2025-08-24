using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeValueText;

    [field: SerializeField] public Button Button { get; private set; }//[field: SerializeField] is for get reference in inspector.
    public void Configure(Sprite icon, string upgradeName, string upgradeValue )
    {
        iconImage.sprite = icon;
        upgradeNameText.text = upgradeName;
        upgradeValueText.text = upgradeValue;
    }

 
}
