using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header("Elements")]
    [SerializeField] private BoxCollider2D hitDetectionBoxCollider;
    [SerializeField] private Transform hitDetectionPos;
    [SerializeField] private float hitDetectionRadius;

    [Header("Setting")]
    private List<Enemy> damagedEnemies = new List<Enemy>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;

            case State.Attack:
                Attacking();
                break;
        }
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector2 targetUpVector = Vector3.up;
        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        IncrementAttackTimer();
    }

    private void ManageAttack()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;
            if (state == State.Idle)
            {
                StartAttack();
            }
            else if (state == State.Attack)
            {
                StopAttack();
            }
        }
    }

    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }

    [NaughtyAttributes.Button("Start Attack")]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        damagedEnemies.Clear();
        animator.speed = 1f / attackDelay;
    }

    private void Attacking()
    {
        Attack();
    }

    [NaughtyAttributes.Button("Stop Attack")]
    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectionPos.position, hitDetectionBoxCollider.bounds.size, hitDetectionPos.localEulerAngles.z, enemyMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy currentEnemy = enemies[i].GetComponent<Enemy>();
            if (!damagedEnemies.Contains(currentEnemy))
            {
                int damage = GetDamage(out bool isCriticalHit);
                currentEnemy.TakeDamage(damage, isCriticalHit);
                damagedEnemies.Add(currentEnemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitDetectionPos != null)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitDetectionPos.position, hitDetectionRadius);
        }
    }
}
