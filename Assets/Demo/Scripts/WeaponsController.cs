using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsInventorySystem;
using WeaponsInventorySystem.Abstraction;
using WeaponsInventorySystem.Helpers;

public class WeaponsController : MonoBehaviour {


	void Awake()
	{
		if (Inventory.Current != null)
		{
			foreach (var weapon in Inventory.Current.WeaponsList)
			{
				weapon.OnFire += new EventHandler<WeaponEventArgs>(OnWeaponFires);
				weapon.OnBeginReload += new EventHandler<WeaponEventArgs>(OnWeaponBeginReload);
				weapon.OnEndReload += new EventHandler<WeaponEventArgs>(OnWeaponEndReload);
				weapon.OnSightModeChanged += new EventHandler<WeaponSightModeChangedEventArgs>(OnWeaponSightModeChanged);
			}
		}

		
	}

	private void OnWeaponSightModeChanged(object sender, WeaponSightModeChangedEventArgs e)
	{

	}

	private void OnWeaponEndReload(object sender, WeaponEventArgs e)
	{

	}

	private void OnWeaponBeginReload(object sender, WeaponEventArgs e)
	{

	}

	private void OnWeaponFires(object sender, WeaponEventArgs e)
	{
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Inventory.Current != null && Inventory.Current.CurrentWeapon != null)
		{
			if (Inventory.Current.CurrentWeapon.IsFiring)
			{
				Inventory.Current.CurrentWeapon.WeaponGameObject.GetComponent<Animation>().
				Play("Shoot", PlayMode.StopAll);
			}
		}

	}
}
