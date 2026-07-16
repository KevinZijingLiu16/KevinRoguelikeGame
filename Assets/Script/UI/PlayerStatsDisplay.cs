using System;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerStatsDisplay : MonoBehaviour, IPlayerStatsDependency
{
   [Header("Elements")]
    [SerializeField] private Transform playerStatsContainerParent;

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        int index = 0;
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            StatsContainer statsContainer = playerStatsContainerParent.GetChild(index).GetComponent<StatsContainer>();
            statsContainer.gameObject.SetActive(true);

            Sprite statIcon = ResourcesManager.GetStatIcon(stat);
            float statValue = playerStatsManager.GetStatValue(stat);
            statsContainer.Configure(statIcon, Enums.FormatStatName(stat), statValue, true);
            index++;
        }

        for (int i = index; i < playerStatsContainerParent.childCount; i++)
        {
            playerStatsContainerParent.GetChild(i).gameObject.SetActive(false);
        }

    }

}
