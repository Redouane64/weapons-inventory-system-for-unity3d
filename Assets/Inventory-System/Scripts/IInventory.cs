using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IInventory
{
    IWeapon CurrentWeapon { get; }
	IEnumerable<Weapon> WeaponsList { get; }
    int InventorySize { get; }
    bool IsChangingItem { get; }

    void AddWeapon (IWeapon weapon);
    void DropWeapon (IWeapon weapon);
    void DropWeapon (int index);
    bool WeaponExist (IWeapon weapon);
    IWeapon GetWeapon (string name);
}
