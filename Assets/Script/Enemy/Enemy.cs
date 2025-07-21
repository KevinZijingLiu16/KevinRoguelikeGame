using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Component")]
    protected EnemyMovement enemyMovement;

    [Header("Health Settings")]
    [SerializeField] protected int maxHealth;

    protected int health;

    [Header("Elements")]
    protected Player player;

    [Header("Spawn Sequence")]
    [SerializeField] protected SpriteRenderer render;

    [SerializeField] protected Collider2D collider;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    protected bool hasSpawned = false;

    [Header("Facing Direction")]
    [SerializeField] private bool defaultFacingRight = true;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem passAwayParticles;
    protected Color originalColor;

    [Header("Events")]
    public static Action<int, Vector2> onDamageTaken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.Log("No player found.");
            Destroy(gameObject);
        }

        StartSpawnSequence();
        originalColor = render.color;
    }

    // Update is called once per frame
    protected bool CanAttack()
    {
        return render.enabled && hasSpawned;    
    }
  
    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.5f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
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

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        onDamageTaken?.Invoke(damage, transform.position);
        StartCoroutine(FlashBlack());
        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        passAwayParticles.transform.SetParent(null);
        // or passAwayParticles.transform.parent == null;
        passAwayParticles.Play();
        Destroy(gameObject);
    }

    protected void FacePlayer()
    {
        if (player == null) return;

        bool shouldFaceRight = player.transform.position.x > transform.position.x;
        float baseScaleX = Mathf.Abs(transform.localScale.x);

        float finalScaleX = defaultFacingRight
            ? (shouldFaceRight ? baseScaleX : -baseScaleX)
            : (shouldFaceRight ? -baseScaleX : baseScaleX);

        Vector3 scale = transform.localScale;
        scale.x = finalScaleX;
        transform.localScale = scale;
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