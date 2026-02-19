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
    [SerializeField] private PlayerWeapons playerWeapons;

    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapons;

    private WeaponDataSO selectedWeapon;
    private int initialWeaponLevel;

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
            case GameState.GAME:
            if(selectedWeapon == null)
            {
                Debug.LogWarning("No weapon selected, cannot add to player");
                return;
            }
            playerWeapons.AddWeapon(selectedWeapon, initialWeaponLevel);
            selectedWeapon = null;
            initialWeaponLevel = 0;
            break;  
            case GameState.WEAPONSELECTION:
                Configure();
                break;
        }
    }
    [NaughtyAttributes.Button]
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
        WeaponDataSO weaponData = starterWeapons[Random.Range(0, starterWeapons.Length)];

        int level = Random.Range(0,4);
      

      weaponSelectionContainerInstance.Configure(weaponData.Sprite, weaponData.Name, level);

      weaponSelectionContainerInstance.Button.onClick.RemoveAllListeners();
        weaponSelectionContainerInstance.Button.onClick.AddListener(() => WeaponSelectedCallback(weaponSelectionContainerInstance, weaponData, level));

    }

    private void WeaponSelectedCallback(WeaponSelectionContainer containerInstance,WeaponDataSO weaponData, int level)
    {

        selectedWeapon = weaponData;
          initialWeaponLevel = level;
       foreach (WeaponSelectionContainer container in containerParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
            if(container == containerInstance)
            {
                container.Select();
            }
            else
            {
                container.Deselect();
            }
        }

        
    }
}
