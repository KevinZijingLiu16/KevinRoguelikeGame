using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] private Transform containerParent;

    [SerializeField] private ShopItemContainer shopItemContainerPrefab;

    [SerializeField] private Button lockButton;
    [SerializeField] private Sprite lockSprite, unlockSprite;



    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
        {
            Configure();
        }
       
    }

    private void Configure()
    {
        containerParent.Clear();
        int containerToAdd = 6;
        int weaponContainerCount = Random.Range(Mathf.Min(2, containerToAdd), containerToAdd);
        int objectContainerCount = containerToAdd - weaponContainerCount;
        for (int i = 0; i < weaponContainerCount; i++)
        {
          ShopItemContainer weaponContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
          //weaponContainerInstance.name = "WeaponContainer";
            WeaponDataSO randomWeapon = ResourcesManager.GetRandomWeapon();
            weaponContainerInstance.Configure(randomWeapon, Random.Range(0, 2));
          
          
        }
        for (int i = 0; i < objectContainerCount; i++)
        {
            ShopItemContainer objectContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
            //objectContainerInstance.name = "ObjectContainer";
            ObjectDataSO randomObject = ResourcesManager.GetRandomObject();

            objectContainerInstance.Configure(randomObject);
        }
    }
}
