using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{

    [Header("Player Elements")]
[SerializeField] private PlayerObject playerObjects;
[SerializeField] private PlayerWeapons playerWeapon;

    [Header("Inventory Elements")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;


    public void GameStateChangedCallback(GameState gameState)
    {
       if (gameState == GameState.SHOP)
       {
          Configure();
       }
    }
    private void Configure()
    {
       inventoryItemsParent.Clear();

       Weapon[] weapons = playerWeapon.GetWeapons();
        for (int i = 0; i < weapons.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
          
            container.Configure(weapons[i], () => ShowItemInfo(container) );
        }




       ObjectDataSO[] objectDatas = playerObjects.Objects.ToArray();
       for (int i = 0; i < objectDatas.Length; i++)
       {
           InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
       
           container.Configure(objectDatas[i],() => ShowItemInfo(container) );
        
       }

    
    }

    private void ShowItemInfo(InventoryItemContainer container)
 
    {
       if (container.Weapon != null)
       {
          ShowWeaponInfo(container.Weapon);
       }
       else if (container.ObjectData != null)
       {
          ShowObjectInfo(container.ObjectData);
       }
    }
    private void ShowWeaponInfo(Weapon weapon)
    {
       Debug.Log(weapon.WeaponData.Name);
    }
    private void ShowObjectInfo(ObjectDataSO objectData)
    {
       Debug.Log(objectData.Name);
    }
}