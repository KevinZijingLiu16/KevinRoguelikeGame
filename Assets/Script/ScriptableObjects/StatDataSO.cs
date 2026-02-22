using UnityEngine;

[CreateAssetMenu(fileName = "StatIcon", menuName = "ScriptableObjects/StatIcon", order = 0)]
public class StatIconDataSO : ScriptableObject
{
   [field: SerializeField] public StatIcon[] StatIcons { get; private set; }
}

[System.Serializable]
public struct StatIcon
{
    public Stat stat;
    public Sprite icon;
}
