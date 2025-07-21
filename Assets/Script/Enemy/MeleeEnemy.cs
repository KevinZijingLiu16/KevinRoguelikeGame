using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{
    [Header("Attack Settings")]
    [SerializeField] private int damage = 1;

    [SerializeField] private float attackFrequency;
    private float attackDelay;
    private float attackTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        attackDelay = 1f / attackFrequency;
    }

    private void Update()
    {
        if (!CanAttack())
            return;

        if (attackTimer > attackDelay)
            TryAttack();
        else
            Wait();
        enemyMovement.FollowPlayer();
        FacePlayer();
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
}