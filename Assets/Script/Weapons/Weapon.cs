using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float range;

    [SerializeField] protected LayerMask enemyMask;

    [SerializeField] protected float aimLerp;
    [SerializeField] protected Animator animator;

    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;

    protected float attackTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Debug.Log("Weapon Initialized: " + gameObject.name);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    protected Enemy GetClosestEnemy()
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

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if(Random.Range(0, 101) < 50) 
        {
            isCriticalHit = true;
            return damage * 2; // Critical hit deals double damage
        }
        return damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}