using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class PlayerLevel : MonoBehaviour
{
    [Header("Settings")]
    private int requiredXP;
    private int currentXP;
    private int level;


    [Header("UI Elements")]
    [SerializeField] private Slider levelBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        CandySoul.onCollected += CandyCollectedCallback;
    }

    private void OnDestroy()
    {
        CandySoul.onCollected -= CandyCollectedCallback;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
  
        UpdateVisuals();
        UpdateRequiredXP();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateVisuals()
    {
        requiredXP = (level + 1) * 5;
    }

    private void UpdateRequiredXP()
    {
        levelBar.value = (float)currentXP / requiredXP;
        levelText.text = "lvl " + (level + 1);
    }

    private void CandyCollectedCallback(CandySoul candy)
    {
        currentXP++;
        if (currentXP >= requiredXP)
        {
          LevelUp();
            // Trigger level up effects or animations here
        }
       UpdateRequiredXP();

    }
    private void LevelUp()
        {
        level++;
        currentXP = 0;
        UpdateRequiredXP();

    }




}
