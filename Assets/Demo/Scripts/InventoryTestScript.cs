using UnityEngine;
using System.Collections;
using System;
using WeaponsInventorySystem;
using WeaponsInventorySystem.Helpers;

public class InventoryTestScript : MonoBehaviour
{

	[SerializeField]
	private Inventory inventory;

	private void Awake()
	{
		if (inventory == null)
		{
			Debug.LogException(new NullReferenceException("Field 'inventory' must be initialized."));
		}
	}

	private void Inventory_OnEndItemChange(object sender, InventoryWeaponEventArgs e)
	{
		Debug.LogFormat("#Event: Inventory.OnEndItemChange\nCurrent Item name: '{0}'.\nNew Item name: '{1}'.", e.Current.Name, e.New.Name);
	}

	private void Inventory_OnItemChanged(object sender, InventoryWeaponEventArgs e)
	{
		Debug.LogFormat("#Event: Inventory.OnItemChanged\nCurrent Item name: '{0}'.\nNew Item name: '{1}'.", e.Current.Name, e.New.Name);
	}

	private void Inventory_OnBeginItemChange(object sender, InventoryWeaponEventArgs e)
	{
		Debug.LogFormat("#Event: Inventory.OnBeginItemChange\nCurrent Item name: '{0}'.\nNew Item name: '{1}'.", e.Current.Name, e.New.Name);
	}

	private void Start()
	{

	}

	private void Update()
	{

	}

	private void OnEnable()
	{
		if (inventory != null)
		{
			inventory.OnBeginItemChange += Inventory_OnBeginItemChange;
			inventory.OnItemChanged += Inventory_OnItemChanged;
			inventory.OnEndItemChange += Inventory_OnEndItemChange;
		}

	}

	private void OnDisable()
	{
		if (inventory != null)
		{
			inventory.OnBeginItemChange -= Inventory_OnBeginItemChange;
			inventory.OnItemChanged -= Inventory_OnItemChanged;
			inventory.OnEndItemChange -= Inventory_OnEndItemChange;
		}

	}


}
