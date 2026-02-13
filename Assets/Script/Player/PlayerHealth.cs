using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Health Settings")]

    [SerializeField] int baseMaxHealth = 100;
    private int maxHealth;

    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;

    [SerializeField] private TextMeshProUGUI healthText;

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
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
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
        float healthBarValue = (float)health / maxHealth;
        healthSlider.value = healthBarValue;
        healthText.text = health.ToString();
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
    }
}