
using System;
using WeaponsInventorySystem.Abstraction;

namespace WeaponsInventorySystem.Helpers
{
	public class WeaponSightModeChangedEventArgs : EventArgs
	{
		private readonly IWeapon weapon;
		private readonly SightMode sightMode;

		public WeaponSightModeChangedEventArgs(IWeapon weapon, SightMode sightMode)
		{
			this.weapon = weapon;
			this.sightMode = sightMode;
		}

		public SightMode Mode
		{
			get
			{
				return sightMode;
			}
		}

		public IWeapon Weapon
		{
			get
			{
				return weapon;
			}
		}
	}
}