
using System;
using WeaponsInventorySystem.Abstraction;

namespace WeaponsInventorySystem.Helpers
{
	public enum FireMode { Auto, Semi };

	public class WeaponEventArgs : EventArgs
	{
		public WeaponEventArgs(IWeapon weapon)
		{
			Weapon = weapon;
		}

		public IWeapon Weapon { get; private set; }
	}

	public class WeaponSightEventArgs : EventArgs
	{
		public WeaponSightEventArgs(bool isAiming)
		{
			IsAiming = isAiming;
		}

		public bool IsAiming { get; private set; }
	}

	public class InventoryWeaponEventArgs : EventArgs
	{
		private IWeapon _current;
		private IWeapon _new;

		public InventoryWeaponEventArgs(IWeapon current, IWeapon @new)
		{
			this._current = current;
			this._new = @new;
		}

		public IWeapon Current
		{
			get
			{
				return _current;
			}
		}

		public IWeapon New
		{
			get
			{
				return _new;
			}
		}
	}
}