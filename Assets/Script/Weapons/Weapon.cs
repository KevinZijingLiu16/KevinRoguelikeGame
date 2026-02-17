using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour, IPlayerStatsDependency
{
    [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }

    [Header("Weapon Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;
    [Header("Weapon Attack")]
    [SerializeField]protected int damage;
    [SerializeField] protected float attackDelay;
    protected float attackTimer;
    [Header("Weapon Critical Hit")]
    protected int criticalChance;
    protected float criticalPercent;
    [Header("Weapon Animation")]
    [SerializeField] protected float aimLerp;
    [SerializeField] protected Animator animator;

 
    [Header("Level")]
    [field: SerializeField] public int Level { get; private set; }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Debug.Log("Weapon Initialized: " + gameObject.name);
            //damage = baseDamage;
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

        if(Random.Range(0, 101) <= criticalChance) 
        {
            isCriticalHit = true;
            return Mathf.RoundToInt(damage * criticalPercent);
        }
        return damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public abstract void UpdateStats(PlayerStatsManager playerStatsManager);

    protected void ConfigueStats()
    {
        float multiplier = 1 + (float)Level /3;
        if (WeaponData == null)
        {
            Debug.LogWarning($"WeaponData is not assigned on {gameObject.name}");
            return;
        }

        damage = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.Attack)* multiplier);
        attackDelay = 1f / (WeaponData.GetStatValue(Stat.AttackSpeed) * multiplier);

        criticalChance = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.CriticalChance) * multiplier);
        criticalPercent = WeaponData.GetStatValue(Stat.CriticalPercent) * multiplier;
        if(WeaponData.Prefab.GetType() == typeof(RangeWeapon))
            range = WeaponData.GetStatValue(Stat.Range) * multiplier;
    }

}
