using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsInventorySystem.Abstraction;
using WeaponsInventorySystem.Helpers;
using WeaponsInventorySystem.Inputs;

namespace WeaponsInventorySystem
{
	public sealed class Inventory : MonoBehaviour, IInventory
	{
		public static IInventory Current { get; private set; }

		[SerializeField]
		private List<GameObject> weapons;
		[SerializeField]
		private List<Weapon> i_weapons_list;
		[SerializeField]
		private int i_list_index;
		[SerializeField]
		private float i_change_speed;

		private bool i_is_changing_item;
		private ActionPredicates can_change_weapon_predicates = new ActionPredicates();
		private Transform i_weapons_keeper;

		public event EventHandler<InventoryWeaponEventArgs> OnBeginItemChange;
		public event EventHandler<InventoryWeaponEventArgs> OnEndItemChange;
		public event EventHandler<InventoryWeaponEventArgs> OnItemChanged;

		public event EventHandler<WeaponEventArgs> OnFire;
		public event EventHandler<WeaponEventArgs> OnBeginReload;
		public event EventHandler<WeaponEventArgs> OnReloaded;
		public event EventHandler<WeaponEventArgs> OnEndReload;

		private void RaiseBeginItemChangeEvent(InventoryWeaponEventArgs e)
		{
			if (OnBeginItemChange != null)
			{
				OnBeginItemChange(null, e);
			}
		}

		private void RaiseEndItemChangeEvent(InventoryWeaponEventArgs e)
		{
			if (OnEndItemChange != null)
			{
				OnEndItemChange(null, e);
			}
		}

		private void RaiseItemChangedEvent(InventoryWeaponEventArgs e)
		{
			if (OnItemChanged != null)
			{
				OnItemChanged(null, e);
			}
		}

		// monobehavior methods

		private void Awake()
		{

			Inventory.Current = this;

			can_change_weapon_predicates.AddPredicate(() => !i_is_changing_item);

			OnBeginItemChange += Inventory_OnBeginItemChange;
			OnEndItemChange += Inventory_OnEndItemChange;
			OnItemChanged += Inventory_OnItemChanged;

			i_weapons_keeper = GameObject.Find("Weapons").transform;

			for (int i = 0; i < weapons.Count; i++)
			{
				var weapon = GameObject.Instantiate(weapons[i]);
				weapon.SetActive(false);
				weapon.transform.parent = i_weapons_keeper;
				weapon.transform.localPosition = i_weapons_keeper.InverseTransformPoint(weapon.transform.position);
				i_weapons_list.Add(weapon.GetComponent<Weapon>());
			}
		}

		private void Start()
		{
			StartCoroutine(ChangeToItemAt(0));
		}

		private void Update()
		{

			if (can_change_weapon_predicates.CanExecuteAction())
			{
				if (Input.GetButtonUp(KeyboardInputManager.ITEM_1))
				{
					StartCoroutine(ChangeToItemAt(0));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_2))
				{
					StartCoroutine(ChangeToItemAt(1));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_3))
				{
					StartCoroutine(ChangeToItemAt(2));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_4))
				{
					StartCoroutine(ChangeToItemAt(3));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_5))
				{
					StartCoroutine(ChangeToItemAt(4));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_6))
				{
					StartCoroutine(ChangeToItemAt(5));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_7))
				{
					StartCoroutine(ChangeToItemAt(6));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_8))
				{
					StartCoroutine(ChangeToItemAt(7));
				}
				else if (Input.GetButtonUp(KeyboardInputManager.ITEM_9))
				{
					StartCoroutine(ChangeToItemAt(8));
				}
			}

			this.CurrentWeapon = i_weapons_list[i_list_index];

		}

		private IEnumerator ChangeToItemAt(int index)
		{
			if (index > InventorySize)
				yield break;

			i_is_changing_item = true;
			RaiseBeginItemChangeEvent(new InventoryWeaponEventArgs(i_weapons_list[i_list_index], i_weapons_list[index]));
			yield return new WaitForSeconds(1 / i_change_speed);
			RaiseItemChangedEvent(new InventoryWeaponEventArgs(i_weapons_list[i_list_index], i_weapons_list[index]));
			i_list_index = index;
			RaiseEndItemChangeEvent(new InventoryWeaponEventArgs(i_weapons_list[i_list_index], i_weapons_list[index]));
			i_is_changing_item = false;
		}

		private void Inventory_OnItemChanged(object sender, InventoryWeaponEventArgs e)
		{
			e.Current.WeaponGameObject.SetActive(false);
			e.New.WeaponGameObject.SetActive(true);
		}

		private void Inventory_OnEndItemChange(object sender, InventoryWeaponEventArgs e)
		{
		}

		private void Inventory_OnBeginItemChange(object sender, InventoryWeaponEventArgs e)
		{
		}

		#region IInventory Members

		public IWeapon CurrentWeapon
		{
			get; private set;
		}

		public IEnumerable<Weapon> WeaponsList
		{
			get { return i_weapons_list; }
		}

		public int InventorySize
		{
			get { return i_weapons_list.Count - 1; }
		}

		public bool IsChangingItem
		{
			get { return i_is_changing_item; }
		}

		public void AddWeapon(IWeapon weapon)
		{
			throw new NotImplementedException();
		}

		public void DropWeapon(IWeapon weapon)
		{
			throw new NotImplementedException();
		}

		public void DropWeapon(int index)
		{
			throw new NotImplementedException();
		}

		public bool WeaponExist(IWeapon weapon)
		{
			throw new NotImplementedException();
		}

		public IWeapon GetWeapon(string name)
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}