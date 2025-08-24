using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainers();
                break;
        }
    }
    [Button]    
    private void ConfigureUpgradeContainers()
    {
        for (int i = 0; i < upgradeContainers.Length; i++)
        {
        
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);

            string randomStatString = Enums.FormatStatName(stat);
            upgradeContainers[i].Configure(null, randomStatString, Random.Range(0,100).ToString());

            Action action = GetActionToPerform(stat);

            upgradeContainers[i].Button.onClick.RemoveAllListeners();

            upgradeContainers[i].Button.onClick.AddListener(() => action?.Invoke());

        }
    }

    private Action GetActionToPerform(Stat stat)
    {
        switch (stat)
        { 
            case Stat.Attack:
                return () => Debug.Log("Improving Attack by " + 5);

                case Stat.AttackSpeed:
                    return () => Debug.Log("Improving AttackSpeed by " + 5);

                case Stat.CriticalChance:
                    return () => Debug.Log("Improving CriticalChance by " + 5);

            default:
                return () => Debug.Log("Invalid stat" );




        }
    }


}
