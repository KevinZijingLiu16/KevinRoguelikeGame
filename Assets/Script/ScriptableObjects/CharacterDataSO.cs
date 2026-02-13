using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/New CharacterData", order = 0)]
public class CharacterDataSO: ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }


    [field: SerializeField] public int PurchasePrice { get; private set; }

    [HorizontalLine]
    [field: SerializeField] private float attack;
    [field: SerializeField] private float attackSpeed;
    [field: SerializeField] private float criticalChance;
    [field: SerializeField] private float criticalPercent;
    [field: SerializeField] private float moveSpeed;
    [field: SerializeField] private float maxHealth;
    [field: SerializeField] private float range;
    [field: SerializeField] private float healthRecoverySpeed;
    [field: SerializeField] private float armor;
    [field: SerializeField] private float luck;
    [field: SerializeField] private float dodge;
    [field: SerializeField] private float lifeSteal;

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
                { Stat.MoveSpeed,                   moveSpeed },
                { Stat.MaxHealth,                   maxHealth },
                { Stat.Range,                       range },
                { Stat.HealthRecoverySpeed,         healthRecoverySpeed },
                { Stat.Armor,                       armor },
                { Stat.Luck,                        luck },
                { Stat.Dodge,                       dodge },
                { Stat.LifeSteal,                   lifeSteal },
            };
        }

        private set { }
    }
}
