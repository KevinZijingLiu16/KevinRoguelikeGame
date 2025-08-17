using System;
using UnityEngine;
using UnityEngine.Pool;


using Random = UnityEngine.Random;

public class DropManager : MonoBehaviour
{
    [Header("Element Reference")]
   [ SerializeField] private CandySoul enemySoulPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;

    [Header("Pooling")]
    private ObjectPool<CandySoul> candyPool;
    private ObjectPool<Cash> cashPool;

    [Header("Setting")]
    [SerializeField] [Range(0,100)] private int cashDropChance = 10;
   
    [SerializeField][Range(0, 100)] private int chestDropChance = 10;

    private void Awake()
    {
        Enemy.onPassedAway += EnemyPassedAwayCallback;
        CandySoul.onCollected += ReleaseCandy;
        Cash.onCollected += ReleaseCash;
    }
    private void OnDestroy()
    {
        Enemy.onPassedAway -= EnemyPassedAwayCallback;
        CandySoul.onCollected -= ReleaseCandy;
        Cash.onCollected -= ReleaseCash;
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        candyPool = new ObjectPool<CandySoul>(CandyCreateFunction, CandyActionOnGet, CandyActionOnRelease, CandyActionOnDestroy);
        cashPool= new ObjectPool<Cash>(CashCreateFunction, CashActionOnGet, CashActionOnRelease, CashActionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private CandySoul CandyCreateFunction()
    {
        CandySoul candytInstance = Instantiate(enemySoulPrefab, transform);
       
        return candytInstance;
    }

    private void CandyActionOnGet(CandySoul candy)
    {
      
        candy.gameObject.SetActive(true);
    }

    private void CandyActionOnRelease(CandySoul candy)
    {
        candy.gameObject.SetActive(false);
    }

    private void CandyActionOnDestroy(CandySoul candy)
    {
        Destroy(candy.gameObject);
    }

    private Cash CashCreateFunction()
    {
        Cash cashInstance = Instantiate(cashPrefab, transform);
       
        return cashInstance;
    }

    private void CashActionOnGet(Cash cash)
    {
    
        cash.gameObject.SetActive(true);
    }

    private void CashActionOnRelease(Cash cash)
    {
        cash.gameObject.SetActive(false);
    }

    private void CashActionOnDestroy(Cash cash)
    {
        Destroy(cash.gameObject);
    }

    private void EnemyPassedAwayCallback(Vector2 enemyPosition)
    {
        bool shouldSpawnCash = Random.Range(0f, 101f) < cashDropChance; // 20% chance to spawn cash

        DroppableCurreny droppable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();

      // DroppableCurreny drpppableInstance = Instantiate(droppable, enemyPosition, Quaternion.identity, transform);
      droppable.name = "Droppable" + Random.Range(0, 1000);
        droppable.transform.position = enemyPosition;
        
        TryDropChest(enemyPosition);


    }

    private void TryDropChest(Vector2 enemyPosition)
    {
        bool shouldSpawnChest = Random.Range(0f, 101f) < chestDropChance; // 10% chance to spawn a chest
        if(!shouldSpawnChest)
            return;
        Instantiate(chestPrefab, enemyPosition, Quaternion.identity, transform);
    }

    private void ReleaseCandy(CandySoul candy) => candyPool.Release(candy);
    private void ReleaseCash(Cash cash) => cashPool.Release(cash);
}
