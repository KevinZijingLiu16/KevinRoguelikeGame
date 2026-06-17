using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/NewObjectData", order = 0)]
public class ObjectDataSO : ScriptableObject
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public Sprite Icon {get; private set;}
    [field: SerializeField] public int Price {get; private set;}
    [field: SerializeField] public int RecyclePrice {get; private set;}
        
    [field: Range(0,3)]
    [field: SerializeField] public int Rarity {get; private set;}

    [SerializeField] private StatDatap[] statDatas;

    //  [HorizontalLine]
    // [field: SerializeField] private float attack;
    // [field: SerializeField] private float attackSpeed;
    // [field: SerializeField] private float criticalChance;
    // [field: SerializeField] private float criticalPercent;
    // [field: SerializeField] private float moveSpeed;
    // [field: SerializeField] private float maxHealth;
    // [field: SerializeField] private float range;
    // [field: SerializeField] private float healthRecoverySpeed;
    // [field: SerializeField] private float armor;
    // [field: SerializeField] private float luck;
    // [field: SerializeField] private float dodge;
    // [field: SerializeField] private float lifeSteal;

    public Dictionary<Stat, float> BaseStats
    {
        get
        {
            
            Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

                foreach (var statData in statDatas)
                {
                    stats.Add(statData.stat, statData.value);
                }
                return stats;
            // return new Dictionary<Stat, float>
            // {
            //     { Stat.Attack,                      attack },
            //     { Stat.AttackSpeed,                 attackSpeed },
            //     { Stat.CriticalChance,              criticalChance },
            //     { Stat.CriticalPercent,             criticalPercent },
            //     { Stat.MoveSpeed,                   moveSpeed },
            //     { Stat.MaxHealth,                   maxHealth },
            //     { Stat.Range,                       range },
            //     { Stat.HealthRecoverySpeed,         healthRecoverySpeed },
            //     { Stat.Armor,                       armor },
            //     { Stat.Luck,                        luck },
            //     { Stat.Dodge,                       dodge },
            //     { Stat.LifeSteal,                   lifeSteal },
            // };
        }

        private set { }
    }

}
[ System.Serializable]
public struct StatDatap
{
    public Stat stat;
    public float value;
}