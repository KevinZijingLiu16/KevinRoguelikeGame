using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{
    private RangeEnemyAttack rangeEnemyAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        rangeEnemyAttack = GetComponent<RangeEnemyAttack>();

        rangeEnemyAttack.StorePlayer(player);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!CanAttack())
            return;
        ManageAttack();
        FacePlayer();

     }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {
            enemyMovement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        rangeEnemyAttack.AutoAim();
    }
}