
using System;
using WeaponsInventorySystem.Abstraction;

namespace WeaponsInventorySystem.Helpers
{
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