
using System;

namespace WeaponsInventorySystem.Helpers
{
	public class WeaponSightEventArgs : EventArgs
	{
		public WeaponSightEventArgs(bool isAiming)
		{
			IsAiming = isAiming;
		}

		public bool IsAiming { get; private set; }
	}
}