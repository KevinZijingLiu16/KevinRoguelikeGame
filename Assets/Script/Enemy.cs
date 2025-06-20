using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    private Player player;
    private bool hasSpawned = false;
    private EnemyMovement enemyMovement;

    [Header("Spawn Sequence")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Attack Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [Header("Pass Away Effects")]
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private ParticleSystem passAwayParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.Log("No player found.");
            Destroy(gameObject);
        }

        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
    }

    private void StartSpawnSequence()
    {
      SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.5f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

 
    void Update()
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

            Debug.Log("Enemy attacked the player!");
        }
    }
    private void Attack()
    {
        Debug.Log($"Enemy is attacking the player by {damage}!");
        attackTimer = 0f;
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
  


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
