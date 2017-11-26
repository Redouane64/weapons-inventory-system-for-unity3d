
using System;
using WeaponsInventorySystem.Abstraction;

namespace WeaponsInventorySystem.Helpers
{
	public class WeaponEventArgs : EventArgs
	{
		public WeaponEventArgs(IWeapon weapon)
		{
			Weapon = weapon;
		}

		public IWeapon Weapon { get; private set; }
	}
}