using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }
    private State state;
    [Header("Weapon Settings")]
    [SerializeField] private float range;
    [SerializeField] private BoxCollider2D hitDetectionBoxCollider;

    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private float aimLerp;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform hitDetectionPos;
    [SerializeField] private float hitDetectionRadius;
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;

    private float attackTimer;

    private List<Enemy> damagedEnemies = new List<Enemy>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
            case State.Idle:
               AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }
        AutoAim();
        Attack();
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
        
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectionPos.position, hitDetectionBoxCollider.bounds.size,hitDetectionPos.localEulerAngles.z, enemyMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy currentEnemy = enemies[i].GetComponent<Enemy>();
            if (!damagedEnemies.Contains(currentEnemy))
            {
                currentEnemy.TakeDamage(damage);
                damagedEnemies.Add(currentEnemy);

            }

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
    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
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

    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;

        //Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
        {
            return null;
        }

        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);
            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionPos.position, hitDetectionRadius);
    }
}