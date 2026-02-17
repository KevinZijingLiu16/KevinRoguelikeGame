using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;


public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containerParent;
    [SerializeField] private WeaponSelectionContainer weaponSelectionContainerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WEAPONSELECTION:
                Configure();
                break;
        }
    }
    private void Configure()
    {
        containerParent.Clear();
        for (int i=0; i < 3; i++)
        {
            GenerateWeaponSelectionContainer();
        }
    }

    private void GenerateWeaponSelectionContainer()
    {
      WeaponSelectionContainer weaponSelectionContainerInstance = Instantiate(weaponSelectionContainerPrefab, containerParent);
    }
}
