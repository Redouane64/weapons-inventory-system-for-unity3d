using System.Collections;
using UnityEngine;
using WeaponsInventorySystem.Helpers;

namespace WeaponsInventorySystem.Abstraction
{
    public interface IWeapon
    {
        string Name { get; }
        bool CanFire { get; }
        bool CanReload { get; }
        bool IsFiring { get; }
        bool IsReloading { get; }
		SightMode SightMode
		{
			get;
		}
        int MagazineSize { get; }
        int Magazine { get; }
        int Ammo { get; }
        float FireRate { get; }
        float FireRange { get; }
        float Damage { get; }
        GameObject WeaponGameObject { get; }

        void Fire();
		void ToggleSight();
        IEnumerator Reload();
        void AddAmmo(int amount);
    }
}
