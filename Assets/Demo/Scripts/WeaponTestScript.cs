using UnityEngine;
using System.Collections;
using System;
using WeaponsInventorySystem;
using WeaponsInventorySystem.Abstraction;
using WeaponsInventorySystem.Helpers;

public class WeaponTestScript : MonoBehaviour
{

    [SerializeField]
    private Weapon weapon;

    private int accumulator = 0;

    private void OnEnable()
    {
        if (weapon == null)
		{
			Debug.LogException(new NullReferenceException("Field 'weapon' must be initialized."));
		}
		else
        {
			weapon.OnFire += new EventHandler<WeaponEventArgs>(OnFire);
            weapon.OnBeginReload += Weapon_OnBeginReload;
            weapon.OnEndReload += Weapon_OnEndReload;
        }
    }

	private void OnFire(object sender, WeaponEventArgs e)
	{
		Debug.Log(++accumulator);
	}

	private void Weapon_OnEndReload(object sender, WeaponEventArgs e)
    {
        // Trrr-
    }

    private void Weapon_OnBeginReload(object sender, WeaponEventArgs e)
    {
        // Rrrt-chak 
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
