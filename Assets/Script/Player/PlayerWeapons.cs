using UnityEngine;
using System.Collections.Generic;

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


    public bool TryAddWeapon(WeaponDataSO selectedWeaponData, int weaponLevel)
    {
        for (int i = 0; i < weaponPositions.Length; i++)
        {
           if(weaponPositions[i].Weapon != null)
            {
                continue;
            }
            weaponPositions[i].AssignWeapon(selectedWeaponData.Prefab, weaponLevel);
            return true;
        }
        return false;
    }

    public Weapon[] GetWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();
        foreach (WeaponPositions weaponPosition in weaponPositions)
        {
            if (weaponPosition.Weapon == null)
            continue;
            weapons.Add(weaponPosition.Weapon);
        }
        return weapons.ToArray();
    }
}