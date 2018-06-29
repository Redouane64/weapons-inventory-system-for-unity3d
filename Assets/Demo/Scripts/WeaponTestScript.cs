using UnityEngine;
using System;
using WeaponsInventorySystem;
using WeaponsInventorySystem.Helpers;

public class WeaponTestScript : MonoBehaviour
{

    private int accumulator = 0;

    private void OnEnable()
    {
        if (Inventory.Current != null)
		{
			Inventory.Current.OnFire += new EventHandler<WeaponEventArgs>(OnFire);
			Inventory.Current.OnBeginReload += Weapon_OnBeginReload;
			Inventory.Current.OnEndReload += Weapon_OnEndReload;
		}
    }

	private void OnDisable()
	{
		if (Inventory.Current != null)
		{
			Inventory.Current.OnFire -= new EventHandler<WeaponEventArgs>(OnFire);
			Inventory.Current.OnBeginReload -= Weapon_OnBeginReload;
			Inventory.Current.OnEndReload -= Weapon_OnEndReload;
		}
	}

	private void Awake()
	{
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
