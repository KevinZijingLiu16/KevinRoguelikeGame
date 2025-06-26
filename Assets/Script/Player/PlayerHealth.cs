using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth;

    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;

    [SerializeField] private TextMeshProUGUI healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;
        UpdateUI();
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
        // Handle player death logic here
        Debug.Log("Player has died.");
        SceneManager.LoadScene(0);
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
}