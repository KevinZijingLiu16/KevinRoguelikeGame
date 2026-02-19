using UnityEngine;

public class WeaponPositions : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public void AssignWeapon(Weapon weapon, int weaponLevel)
    {
        Weapon = Instantiate(weapon,transform);

        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.identity;

        Weapon.UpgradeTo(weaponLevel);
    }

    
}
