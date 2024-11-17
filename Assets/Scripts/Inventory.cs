using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> keys = new List<int>();
    public List<Weapon> weapons = new List<Weapon>();

    private int EquipedWeapon = 0;
    public Weapon CurrentWeapon;

    public void Equip(int index)
    {
        if (weapons.Count - 1 < index)
        {
            return;
        }

        weapons[index].gameObject.SetActive(!weapons[index].gameObject.activeSelf);

        if (index != EquipedWeapon)
        {
            weapons[EquipedWeapon].gameObject.SetActive(false);
        }
        else if (CurrentWeapon != null)
        {
            CurrentWeapon = null;
            return;
        }

        CurrentWeapon = weapons[index];
    }
}
