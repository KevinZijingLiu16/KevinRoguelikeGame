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
    private int levelsEarnedThisWave;


    [Header("UI Elements")]
    [SerializeField] private Slider levelBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Debug")]
    [SerializeField] private bool DEBUG;

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
        levelsEarnedThisWave++;
        currentXP = 0;
        UpdateRequiredXP();

    }

    public bool HasPendingLevelUps()
    {
        if (DEBUG)
            return true;
        return levelsEarnedThisWave > 0;
    }

    public bool HasLeveledUp()
    {
        if (DEBUG)
            return true;
        if(levelsEarnedThisWave > 0)
        {
            levelsEarnedThisWave--;
            return true;
        }
        return false;
    }


}
