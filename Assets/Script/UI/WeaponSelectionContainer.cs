using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Outline outline;

    [field: SerializeField] public Button Button { get; private set; }

    public void Configure(Sprite sprite, string name, int level)
    {
        nameText.text = name;
        icon.sprite = sprite;
        Color imageColor = ColorHolder.GetColor(level);

        foreach(Image image in levelDependentImage)
        {
            image.color = imageColor;
        }

        if (outline != null)
            outline.effectColor = ColorHolder.GetOutlineColor(level);
    }
    public void Select()
    {
        // Add selection logic here, such as highlighting the container or playing a sound effect
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.2f).setEase(LeanTweenType.easeInOutSine);
    }
        public void Deselect()
        {
            // Add deselection logic here, such as removing the highlight or stopping the sound effect
            LeanTween.cancel(gameObject);
            LeanTween.scale(gameObject, Vector3.one, 0.2f);
        }
}
