using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Health Settings")]

    [SerializeField] int baseMaxHealth = 100;
    private float maxHealth;

    private float health;
   private float armor;

   private float lifeSteal;
   private float healthRecoverySpeed;
   private float healthRecoveryTimer;
   private float healthRecoveryDuration;

   private float dodge;
   [Header("Actions and Events")]
   public static Action<Vector2> onAttackDodged;

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
        if (health < maxHealth)
        {
          RecoverHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        if(ShouldDodge())
        {
            onAttackDodged?.Invoke(transform.position);
          //  Debug.Log("Player dodged the attack!");
            return;
        }
        float realDamage = damage * Mathf.Clamp (1-(armor / 1000), 0,10000) ; 
        realDamage = Mathf.Min(realDamage, health);
        health -= realDamage;

      //  Debug.Log($"Player took {realDamage} damage");
        // Vibrate on health drop
        OnViboration();
        // Debug.Log($"Player took {realDamage} damage. Remaining health: {health}");

        UpdateUI();

        if (health <= 0)
        {
            PassAway();
        }
    }

    private bool ShouldDodge()
    {
        return Random.Range(0f, 100f) < dodge;
    }

    private void RecoverHealth()
    {
        healthRecoveryTimer += Time.deltaTime;

        if (healthRecoveryTimer >= healthRecoveryDuration)
        {
        
            healthRecoveryTimer = 0f; // Reset the timer
            float healthToAdd = Mathf.Min(.1f, maxHealth - health); // Calculate the health to add, ensuring it doesn't exceed maxHealth
            health += healthToAdd; 
            UpdateUI(); 
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
        dodge = playerStatsManager.GetStatValue(Stat.Dodge) ;

        healthRecoverySpeed = Mathf.Max(0.0001f, playerStatsManager.GetStatValue(Stat.HealthRecoverySpeed) );
        healthRecoveryDuration = 1f / healthRecoverySpeed;
    }
}