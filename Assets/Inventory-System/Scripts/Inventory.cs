#undef DEBUG

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public sealed class Inventory : MonoBehaviour , IInventory
{
    public static IInventory Current { get; private set; }

    [SerializeField]
    private List<Weapon> i_weapons_list;
    [SerializeField]
    private int i_list_index;
    [SerializeField]
    private float i_change_speed;

    private bool i_is_changing_item;

    private List<Func<bool>> can_change_weapon_predicats = new List<Func<bool>>();

    // events
    public event InventoryEventHandler OnBeginItemChange;
    public event InventoryEventHandler OnEndItemChange;
    public event InventoryEventHandler OnItemChanged;

    private void RaiseBeginItemChangeEvent(IWeapon current, IWeapon next)
    {
        if (OnBeginItemChange != null) OnBeginItemChange(current, next);
    }

    private void RaiseEndItemChangeEvent(IWeapon current, IWeapon next)
    {
        if (OnEndItemChange != null) OnEndItemChange(current, next);
    }

    private void RaiseItemChangedEvent(IWeapon current, IWeapon next)
    {
        if (OnItemChanged != null) OnItemChanged(current, next);
    }

    // monobehavior methods

    private void Awake () {

        Inventory.Current = this;

        can_change_weapon_predicats.Add(() => !i_is_changing_item);

        OnBeginItemChange += Inventory_OnBeginItemChange;
        OnEndItemChange += Inventory_OnEndItemChange;
        OnItemChanged += Inventory_OnItemChanged;

    }

    private void Start () {
        StartCoroutine(ChangeToItemAt(0));
    }

    private void Update () {

        if (CheckCanChangeWeapon())
        {
            if(Input.GetButtonUp(KeyboardInputManager.ITEM_1))
            {
                StartCoroutine(ChangeToItemAt(1));
            }
            else if(Input.GetButtonUp(KeyboardInputManager.ITEM_2))
            {
                StartCoroutine(ChangeToItemAt(2));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_3))
            {
                StartCoroutine(ChangeToItemAt(3));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_4))
            {
                StartCoroutine(ChangeToItemAt(4));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_5))
            {
                StartCoroutine(ChangeToItemAt(5));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_6))
            {
                StartCoroutine(ChangeToItemAt(6));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_7))
            {
                StartCoroutine(ChangeToItemAt(7));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_8))
            {
                StartCoroutine(ChangeToItemAt(8));
            }
            else if (Input.GetButtonUp(KeyboardInputManager.ITEM_9))
            {
                StartCoroutine(ChangeToItemAt(9));
            }
        }        
    }

    // inventory methods

    private IEnumerator ChangeToItemAt(int index)
    {
        if (index > InventorySize)
            yield break;

        i_is_changing_item = true;
        RaiseBeginItemChangeEvent(i_weapons_list[i_list_index], i_weapons_list[index]);
        yield return new WaitForSeconds(1 / i_change_speed);
        RaiseEndItemChangeEvent(i_weapons_list[i_list_index], i_weapons_list[index]);
        RaiseItemChangedEvent(i_weapons_list[i_list_index], i_weapons_list[index]);
        i_list_index = index;
        i_is_changing_item = false;
    }

    //

    private bool CheckCanChangeWeapon()
    {
        for (int i = 0; i < can_change_weapon_predicats.Count; i++) if (!can_change_weapon_predicats[i]()) return false;
        return true;
    }

    private void Inventory_OnItemChanged(IWeapon current, IWeapon @new)
    {
#if DEBUG
        Debug.Log("Item Changed.");
#endif   
        current.WeaponGameObject.SetActive(false);
        @new.WeaponGameObject.SetActive(true);
    }

    private void Inventory_OnEndItemChange(IWeapon current, IWeapon @new)
    {
#if DEBUG
        Debug.Log("Item Change Ends.");
#endif
    }

    private void Inventory_OnBeginItemChange(IWeapon current, IWeapon @new)
    {
#if DEBUG
        Debug.Log("Item Change  Begins.");
#endif
    }


    #region IInventory Members

    public IWeapon CurrentWeapon {
        get; private set;
    }

	public IEnumerable<Weapon> WeaponsList {
		get { return i_weapons_list.ToArray(); }
    }

    public int InventorySize {
        get { return i_weapons_list.Count - 1; }
    }

    public bool IsChangingItem {
        get { return i_is_changing_item; }
    }

    public void AddWeapon (IWeapon weapon) {
        throw new NotImplementedException();
    }

    public void DropWeapon (IWeapon weapon) {
        throw new NotImplementedException();
    }

    public void DropWeapon (int index) {
        throw new NotImplementedException();
    }

    public bool WeaponExist (IWeapon weapon) {
        throw new NotImplementedException();
    }

    public IWeapon GetWeapon (string name) {
        throw new NotImplementedException();
    }

    #endregion

    // helpers

    public delegate void InventoryEventHandler(IWeapon current, IWeapon @new);
}
