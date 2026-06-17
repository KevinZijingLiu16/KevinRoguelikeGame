using UnityEngine;
using System.Collections.Generic;

public class PlayerObject : MonoBehaviour
{
 
    [field: SerializeField] public List<ObjectDataSO> Objects { get; private set; } 
    private PlayerStatsManager playerStatsManager;

    void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ObjectDataSO objectData in Objects)
        {
            playerStatsManager.ApplyObject(objectData.BaseStats);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddObject(ObjectDataSO newObject)
    {
        Objects.Add(newObject);
        playerStatsManager.ApplyObject(newObject.BaseStats);
    }
}
