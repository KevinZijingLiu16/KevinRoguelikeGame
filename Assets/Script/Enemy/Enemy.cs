using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    private Player player;
    private bool hasSpawned = false;
    private EnemyMovement enemyMovement;

    [Header("Spawn Sequence")]
    [SerializeField] private SpriteRenderer render;
    private Color originalColor;
    [SerializeField] private Collider2D collider;

    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Attack Settings")]
    [SerializeField] private int damage = 1;

    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header("Pass Away Effects")]
    [SerializeField] private float playerDetectionRadius;

    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth;
    [SerializeField] private TextMeshPro healthText;

    [Header("Events")]
    public static Action<int, Vector2> onDamageTaken;


    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;
        if (healthText != null)
        {
            healthText.text = maxHealth.ToString();
        }
        enemyMovement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.Log("No player found.");
            Destroy(gameObject);
        }

        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
        originalColor = render.color;
    }

    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.5f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    private void Update()
    {
     
        if (attackTimer > attackDelay)
            TryAttack();
        else
            Wait();
    }

    private void SpawnSequenceCompleted()
    {
        SetRenderersVisibility(true);
        hasSpawned = true;
        collider.enabled = true;
        enemyMovement.StorePlayer(player);
    }

    private void SetRenderersVisibility(bool visibility)
    {
        render.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();

           // Debug.Log("Enemy attacked the player!");
        }
    }

    private void Attack()
    {
        //Debug.Log($"Enemy is attacking the player by {damage}!");
        attackTimer = 0f;
        player.TakeDamage(damage);
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void PassAway()
    {
        passAwayParticles.transform.SetParent(null);
        // or passAwayParticles.transform.parent == null;
        passAwayParticles.Play();
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
        onDamageTaken?.Invoke(damage, transform.position);
        StartCoroutine(FlashBlack());
        if ( health <= 0)
        {
            
         PassAway();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

    private IEnumerator FlashBlack()
    {
       render.color = Color.black;
        yield return new WaitForSeconds(0.1f); 
        render.color = originalColor;
    }
}