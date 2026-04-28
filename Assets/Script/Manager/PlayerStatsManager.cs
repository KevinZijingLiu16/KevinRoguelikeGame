using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDataSO playerData;

    [Header("Settings")]
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();
     private Dictionary<Stat, float> addends = new Dictionary<Stat, float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerStats = playerData.BaseStats;

        foreach(KeyValuePair<Stat, float> kvp in playerStats)
        {
            addends.Add(kvp.Key, 0);
        }
    }
    void Start()
    {
      
        UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerStats(Stat stat, float value)
    {
        //Player: Base Stats

        //Addends: Upgrades in the Wave Transition

        //Stat - value
        if (addends.ContainsKey(stat))
        {

            addends[stat] += value;
        }
        else
        {
            Debug.LogError($"The key {stat} is not present in the dictionary. Cannot add value.");
        }

        UpdatePlayerStats();
    }

    public float GetStatValue(Stat stat)
    {
     return playerStats[stat] + addends[stat];
        
    }
    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDependency> playerStatsDependencies =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IPlayerStatsDependency>();

        foreach (IPlayerStatsDependency dependency in playerStatsDependencies)
        {
            dependency.UpdateStats(this);
        }
    }
}

