using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private WeaponPositions[] weaponPositions;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(WeaponDataSO selectedWeaponData, int weaponLevel)
    {
        //Debug.Log("PlayerWeapons: Adding weapon " + selectedWeaponData.Name + " at level " + weaponLevel);
        weaponPositions[Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeaponData.Prefab, weaponLevel);
    }
}
