using System.Collections;
using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    bool CanFire { get; }
    bool CanReload { get; }
    bool IsFire { get; }
    bool IsReloading { get; }
    int MagazineSize { get; }
    int Magazine { get; }
    int Ammo { get; }
    float FireRate { get; }
    float FireRange { get; }
    float Damage { get; }
    GameObject WeaponGameObject { get; }

    void Fire();
    IEnumerator Reload();
    void AddAmmo (int amount);
}

