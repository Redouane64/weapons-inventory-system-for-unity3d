using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeaponsInventorySystem;
using WeaponsInventorySystem.Helpers;

namespace WeaponsInventorySystem.Abstraction
{
	public interface IInventory
	{
		IWeapon CurrentWeapon { get; }
		IEnumerable<Weapon> WeaponsList { get; }
		int InventorySize { get; }
		bool IsChangingItem { get; }

		void AddWeapon(IWeapon weapon);
		void DropWeapon(IWeapon weapon);
		void DropWeapon(int index);
		bool WeaponExist(IWeapon weapon);
		IWeapon GetWeapon(string name);

		event EventHandler<InventoryWeaponEventArgs> OnBeginItemChange;
		event EventHandler<InventoryWeaponEventArgs> OnEndItemChange;
		event EventHandler<InventoryWeaponEventArgs> OnItemChanged;
		event EventHandler<WeaponEventArgs> OnFire;
		event EventHandler<WeaponEventArgs> OnBeginReload;
		event EventHandler<WeaponEventArgs> OnReloaded;
		event EventHandler<WeaponEventArgs> OnEndReload;

	}
}
