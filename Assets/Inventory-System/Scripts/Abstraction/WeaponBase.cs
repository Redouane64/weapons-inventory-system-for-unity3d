#undef DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponsInventorySystem.Helpers;

namespace WeaponsInventorySystem.Abstraction
{
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
        protected ActionPredicates can_fire_predicates = new ActionPredicates();
        protected ActionPredicates can_reload_predicates = new ActionPredicates();

        // events
		public event EventHandler<WeaponEventArgs> OnFires;
		public event EventHandler<WeaponEventArgs> OnBeginReload;
		public event EventHandler<WeaponEventArgs> OnEndReload;

		protected void RaiseFireEvent()
        {
			if(OnFires != null)
			{
				OnFires(null, new WeaponEventArgs(this));
			}
        }

        protected void RaiseBeginReloadEvent()
        {
            if (OnBeginReload != null)
                OnBeginReload(null, new WeaponEventArgs(this));
        }

        protected void RaiseEndReloadEvent()
        {
            if (OnEndReload != null)
                OnEndReload(null, new WeaponEventArgs(this));
        }

        // Monobehaviour methods

        protected virtual void Awake()
        {
            can_fire_predicates.AddPredicate(() => w_magazine > 0);
            can_fire_predicates.AddPredicate(() => !w_reloading);
            can_fire_predicates.AddPredicate(() => !Inventory.Current.IsChangingItem);

            can_reload_predicates.AddPredicate(() => !w_reloading);
            can_reload_predicates.AddPredicate(() => w_ammo > 0);
            can_reload_predicates.AddPredicate(() => !Inventory.Current.IsChangingItem);
        }

        protected virtual void Update()
        {
            w_can_fire = can_fire_predicates.CanExecuteAction();
            w_can_reload = can_reload_predicates.CanExecuteAction();
        }


        // weapon methods 

        public void Fire()
        {
            if (Time.time > firetime && (w_can_fire))
            {
                w_firing = true;
                --w_magazine;
                firetime = Time.time + w_fire_rate;
                RaiseFireEvent();
#if DEBUG
            Debug.Log("FIRE: " + w_firing.ToString(), this);
#endif
            }
        }

        public IEnumerator Reload()
        {
#if DEBUG
        Debug.Log("Reloading...");
#endif
            if (!(w_can_reload))
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

        public GameObject WeaponGameObject
        {
            get { return this.gameObject; }
        }

        public string Name
        {
            get { return w_name; }
        }

        public bool CanFire
        {
            get { return w_can_fire; }
        }

        public bool CanReload
        {
            get { return w_can_reload; }
        }

        public bool IsFiring
        {
            get { return w_firing; }
        }

        public bool IsReloading
        {
            get { return w_reloading; }
        }

        public int MagazineSize
        {
            get { return MagazineSize; }
        }

        public int Magazine
        {
            get { return w_magazine; }
        }

        public int Ammo
        {
            get { return w_ammo; }
        }

        public float FireRate
        {
            get { return w_fire_rate; }
        }

        public float FireRange
        {
            get { return w_fire_range; }
        }

        public float Damage
        {
            get { return w_damage; }
        }

    }
}