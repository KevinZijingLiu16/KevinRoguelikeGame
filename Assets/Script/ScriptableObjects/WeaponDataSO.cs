using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/New WeaponData", order = 0)]

public class WeaponDataSO : ScriptableObject
{
    [Header("Weapon")]
    [field: SerializeField] public string Name{ get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }
    [field: SerializeField] public Weapon Prefab { get; private set; }

    [HorizontalLine]
    [Header("Weapon Settings")]
    [field: SerializeField] private float attack;
    [field: SerializeField] private float attackSpeed;
    [field: SerializeField] private float criticalChance;
    [field: SerializeField] private float criticalPercent;
    [field: SerializeField] private float range;
     public Dictionary<Stat, float> BaseStats
    
    {
        get
        {
            return new Dictionary<Stat, float>
            {
                { Stat.Attack,                      attack },
                { Stat.AttackSpeed,                 attackSpeed },
                { Stat.CriticalChance,              criticalChance },
                { Stat.CriticalPercent,             criticalPercent },
                { Stat.Range,                       range },
                
            };
        }

        private set { }
    }

    public float GetStatValue(Stat stat)
    {
        foreach(KeyValuePair<Stat, float> kvp in BaseStats)
        {
            if(kvp.Key == stat)
            {
                return kvp.Value;
            }

        }
        Debug.LogWarning("Stat not found: " + stat);
            return 0;
    }

}
