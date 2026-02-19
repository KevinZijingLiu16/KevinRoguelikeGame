using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Palette", menuName = "ScriptableObjects/New Palette", order = 0)]

public class PaletteSO : ScriptableObject
{
    [field: SerializeField] public Color[] levelColors { get; private set; }
    [field: SerializeField] public Color[] levelOutlineColors { get; private set; }
}
