#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField]
    protected string w_name;
    [SerializeField]
    protected FireMode w_fire_mode;
    [SerializeField]
    protected int w_magazine_size;
    [SerializeField]
    protected int w_magazine;
    [SerializeField]
    protected int w_ammo;
    [SerializeField]
    protected float w_fire_rate;
    [SerializeField]
    protected float w_fire_range;
    [SerializeField]
    protected float w_damage;
    [SerializeField]
    protected float w_reload_time;

    [SerializeField]
    protected bool w_firing;
    [SerializeField]
    protected bool w_reloading;
    [SerializeField]
    protected bool w_can_fire;
    [SerializeField]
    protected bool w_can_reload;

    private float firetime = 0;

    // can fire predicats 
    protected List<Func<bool>> can_fire_predicats = new List<Func<bool>>();
    protected List<Func<bool>> can_reload_predicats = new List<Func<bool>>();

    // events
    public event WeaponEventHandler OnFire;
    public event WeaponEventHandler OnBeginReload;
    public event WeaponEventHandler OnEndReload;

    protected void RaiseFireEvent()
    {
        if (OnFire != null)
            OnFire(this);
    }

    protected void RaiseBeginReloadEvent()
    {
        if (OnBeginReload != null)
            OnBeginReload(this);
    }

    protected void RaiseEndReloadEvent()
    {
        if (OnEndReload != null)
            OnEndReload(this);
    }

    // Monobehaviour methods

    protected virtual void Awake()
    {
        can_fire_predicats.Add(() => w_magazine > 0);
        can_fire_predicats.Add(() => !w_reloading);
        can_fire_predicats.Add(() => !Inventory.Current.IsChangingItem);

        can_reload_predicats.Add(() => !w_reloading);
        can_reload_predicats.Add(() => w_ammo > 0);
        can_reload_predicats.Add(() => !Inventory.Current.IsChangingItem);
    }

    protected virtual void Update()
    {
        
    }

    // weapon methods 

    protected bool CheckCanFire()
    {
        for(int i = 0; i < can_fire_predicats.Count; i++) if (!can_fire_predicats[i]()) return false;
        return true;
    }

    protected bool CheckCanReload()
    {
        for (int i = 0; i < can_reload_predicats.Count; i++) if (!can_reload_predicats[i]()) return false;
        return true;
    }

    public void Fire()
    {
        if (Time.time > firetime && (w_can_fire = CheckCanFire()))
        {
            w_firing = true;
            --w_magazine;
            firetime = Time.time + w_fire_rate;
            RaiseFireEvent();
#if DEBUG
            Debug.Log("FIRE: " + w_firing.ToString(), this);
#endif
        }
        else
        {
            w_firing = false;
        }
    }

    public IEnumerator Reload()
    {
#if DEBUG
        Debug.Log("Reloading...");
#endif
        if (!(w_can_reload = CheckCanReload()))
        {
#if DEBUG
            Debug.Log("You cannot Reload.");
#endif
            yield break;
        }
        w_reloading = true;
        RaiseBeginReloadEvent();
        yield return new WaitForSeconds(w_reload_time);

        if (w_magazine > 0)
        {
            w_ammo += w_magazine;
        }

        if (w_ammo < w_magazine_size)
        {
            w_magazine = w_ammo;
            w_ammo = 0;
            //yield break;
        }
        else
        {
            w_magazine = w_magazine_size;
            w_ammo -= w_magazine_size;
            RaiseEndReloadEvent();
        }
        w_reloading = false;
#if DEBUG
        Debug.Log("Finished Reloading.");
#endif
    }

    public void AddAmmo(int amount)
    {
        throw new NotImplementedException();
    }

    // Properties

    public GameObject WeaponGameObject {
        get { return this.gameObject; }
    }

    public string Name {
        get { return w_name; }
    }

    public bool CanFire {
        get { return w_can_fire; }
    }

    public bool CanReload {
        get { return w_can_reload; }
    }

    public bool IsFire {
        get { return w_firing; }
    }

    public bool IsReloading {
        get { return w_reloading; }
    }

    public int MagazineSize {
        get { return MagazineSize; }
    }

    public int Magazine {
        get { return w_magazine; }
    }

    public int Ammo {
        get { return w_ammo; }
    }

    public float FireRate {
        get { return w_fire_rate; }
    }

    public float FireRange {
        get { return w_fire_range; }
    }

    public float Damage {
        get { return w_damage; }
    }

    // helpers

    public enum FireMode { Auto, Semi };

    public delegate void WeaponEventHandler(IWeapon weapon);

}



