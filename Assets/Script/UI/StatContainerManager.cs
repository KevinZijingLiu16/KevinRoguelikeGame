using System.Collections.Generic;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager instance;
    [SerializeField] private StatsContainer statsContainerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateContainer(Dictionary<Stat, float> statsDictionary, Transform parent)
    {

        List<StatsContainer> statsContainers = new List<StatsContainer>();
        foreach(KeyValuePair<Stat, float> kvp in statsDictionary)
        {
            StatsContainer statsContainerInstance = Instantiate(statsContainerPrefab, parent);
            Sprite icon = ResourcesManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatStatName(kvp.Key);
            string statValue = kvp.Value.ToString("F1");
            statsContainerInstance.Configure(icon, statName, statValue);
        }

        LeanTween.delayedCall(Time.deltaTime * 2, () => ResizeText(statsContainers));
    }
    private void ResizeText(List<StatsContainer> statsContainers)
    {
        float minFontSize = 5000;
        for(int i = 0; i < statsContainers.Count; i++)
        {
            StatsContainer statsContainer = statsContainers[i];
            float fontSize = statsContainer.GetFontSize();
            if (fontSize < minFontSize)
            {
                minFontSize = fontSize;
            }
        }
       
       for(int i = 0; i < statsContainers.Count; i++)
        {
           statsContainers[i].SetFontSize(minFontSize);
            
        }
    }
   
    public static void GenerateStatContainer(Dictionary<Stat, float> statsDictionary, Transform parent)
    {
        instance.GenerateContainer(statsDictionary, parent);
    }
  
}
