using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Health Settings")]

    [SerializeField] int baseMaxHealth = 100;
    private float maxHealth;

    private float health;
   private float armor;

   private float lifeSteal;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
       Enemy.onDamageTaken += EnemyTookDamageCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallback;
    }

    private void EnemyTookDamageCallback(int damage, Vector2 enemyPos, bool isCriticalHit)
    {
       if (health >= maxHealth) return;

         float lifeStealAmount = damage * lifeSteal;
         float heathToAdd = Mathf.Min(lifeStealAmount, maxHealth - health);

            health += heathToAdd;

            UpdateUI();
    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        float realDamage = damage * Mathf.Clamp (1-(armor / 1000), 0,10000) ; 
        realDamage = Mathf.Min(realDamage, health);
        health -= realDamage;

        Debug.Log($"Player took {realDamage} damage");
        // Vibrate on health drop
        OnViboration();
        // Debug.Log($"Player took {realDamage} damage. Remaining health: {health}");

        UpdateUI();

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
       GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    private void UpdateUI()
    {
        float healthBarValue = health / maxHealth;
        healthSlider.value = healthBarValue;
        healthText.text = $"{(int)health} / {(int)maxHealth}";
    }

    private void OnViboration()
    {
        // Implement vibration logic if needed
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float addedHealth = playerStatsManager.GetStatValue(Stat.MaxHealth);
        maxHealth = baseMaxHealth + (int)addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1); // Ensure maxHealth is at least 1

        health = maxHealth;
        UpdateUI();

        armor = playerStatsManager.GetStatValue(Stat.Armor);
        lifeSteal = playerStatsManager.GetStatValue(Stat.LifeSteal) / 100;
    }
}