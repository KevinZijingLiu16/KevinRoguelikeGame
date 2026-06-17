using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using NaughtyAttributes;
using Unity.VisualScripting;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    public static WaveTransitionManager instance;
    [Header("Elements")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private UpgradeContainer[] upgradeContainers;
      [SerializeField] private GameObject upgradeContainersParents;
    [Header("Settings")]
    public int chestCollected ;
    [Header("Chest Related")]
    [SerializeField] private ChestObjectContainer chestContainerPrefab;
    [SerializeField] private Transform chestContainerParent;

    [Header("Player Objects")]
    [SerializeField] private PlayerObject playerObjects;
    void Awake()
    {       
         if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Chest.onCollected += ChestCollectedCallback;
    }
    private void OnDestroy()
    {
        Chest.onCollected -= ChestCollectedCallback;
    }

  
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                TryOpenChest();
                break;
        }
    }
    private void TryOpenChest()
    {
        chestContainerParent.Clear();
        if (chestCollected > 0)
        {
           ShowObject();
        }
        else if (Player.instance.HasLeveledUp())
        {
            ConfigureUpgradeContainers();
        }
        else
        {
            GameManager.instance.WaveCompletedCallback();
        }
        }

        private void ShowObject()
    {
        chestCollected--;
        upgradeContainersParents.SetActive(false);

        ObjectDataSO[] objectDatas = ResourcesManager.Objects;
        ObjectDataSO randomObjectData = objectDatas[Random.Range(0, objectDatas.Length)];
        ChestObjectContainer chestInstance = Instantiate(chestContainerPrefab, chestContainerParent);
        chestInstance.Configure(randomObjectData);
        chestInstance.TakeButton.onClick.AddListener(() => TakeButtonCallback(randomObjectData));
        chestInstance.RecycleButton.onClick.AddListener(() => RecycleBUttonCallback(randomObjectData));
    }
    private void TakeButtonCallback(ObjectDataSO objectToTake)
    {
        playerObjects.AddObject(objectToTake);
        TryOpenChest();
        
    }

    private void RecycleBUttonCallback(ObjectDataSO objectToRecycle)
    {
       CurrencyManager.instance.AddCurrency(objectToRecycle.RecyclePrice);
        TryOpenChest();
    
        

    }


    [Button]    
    private void ConfigureUpgradeContainers()
    {
         upgradeContainersParents.SetActive(true);

        for (int i = 0; i < upgradeContainers.Length; i++)
        {
        
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);
            Sprite upgradeIcon = ResourcesManager.GetStatIcon(stat);

            string randomStatString = Enums.FormatStatName(stat);

            string buttonString;
            Action action = GetActionToPerform(stat, out buttonString);

            upgradeContainers[i].Configure(upgradeIcon, randomStatString, buttonString);


            upgradeContainers[i].Button.onClick.RemoveAllListeners();

            upgradeContainers[i].Button.onClick.AddListener(() => action?.Invoke());

            upgradeContainers[i].Button.onClick.AddListener(() => BonusSelectiodCallback());

        }
    }

    private void BonusSelectiodCallback()
    {
        GameManager.instance.WaveCompletedCallback();
    }

    private Action GetActionToPerform(Stat stat, out string buttonString)
    {
        buttonString = "";
        float value;
             
               

        switch (stat)
        {
            
            case Stat.Attack:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;



            case Stat.AttackSpeed:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.CriticalChance:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.CriticalPercent:
                value = Random.Range(1f, 5f);
                buttonString = "+" + value.ToString("F2") + "X";
                break;

            case Stat.MoveSpeed:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.MaxHealth:
                value = Random.Range(1, 10);
                buttonString = "+" + value;
                break;

            case Stat.Range:
                value = Random.Range(1f, 10f);
                 buttonString = "+" + value.ToString() ;
                break;

            case Stat.HealthRecoverySpeed:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.Armor:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.Luck:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.Dodge:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;

            case Stat.LifeSteal:
                value = Random.Range(1, 10);
                 buttonString = "+" + value.ToString() + "%";
                break;


            default:
                return () => Debug.Log("Invalid stat" );




        }

        //buttonString = Enums.FormatStatName(stat) + "\n" + buttonString;

        return () => playerStatsManager.AddPlayerStats(stat, value);
        
    }

      private void ChestCollectedCallback()
    {
        chestCollected ++;
        Debug.Log($"Chest collected! Total: {chestCollected}");
    }

    public bool HasCollectedChests()
    {
        return chestCollected > 0;
    }
}
