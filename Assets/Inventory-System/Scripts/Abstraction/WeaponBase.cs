#undef DEBUG

using System;
using System.Collections;
using UnityEngine;
using WeaponsInventorySystem.Helpers;
using WeaponsInventorySystem.Inputs;

namespace WeaponsInventorySystem.Abstraction
{
	public abstract class WeaponBase : MonoBehaviour, IWeapon
	{
		[SerializeField]
		protected string w_name;
		[SerializeField]
		protected FireMode w_fire_mode;
		[SerializeField]
		protected SightMode w_sight_mode;
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
		[SerializeField]
		protected bool w_can_change_sightmode;

		private float firetime = 0;

		// can fire predicats 
		protected ActionPredicates can_fire_predicates = new ActionPredicates();
		protected ActionPredicates can_reload_predicates = new ActionPredicates();
		protected ActionPredicates can_change_sightmod_predicates = new ActionPredicates();

		// events
		internal static event EventHandler<WeaponEventArgs> OnFire;
		internal static event EventHandler<WeaponEventArgs> OnBeginReload;
		internal static event EventHandler<WeaponEventArgs> OnEndReload;
		internal static event EventHandler<WeaponSightModeChangedEventArgs> OnSightModeChanged;

		protected void RaiseFireEvent()
		{
			if (OnFire != null)
			{
				OnFire(null, new WeaponEventArgs(this));
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

		protected void RaiseSightModeChangedEvent()
		{
			if (OnSightModeChanged != null)
			{
				OnSightModeChanged(this, new WeaponSightModeChangedEventArgs(this, w_sight_mode));
			}
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

			can_change_sightmod_predicates.AddPredicate(() => !w_reloading);
			can_change_sightmod_predicates.AddPredicate(() => !Inventory.Current.IsChangingItem);
		}

		protected virtual void Update()
		{
			w_can_fire = can_fire_predicates.CanExecuteAction();
			w_can_reload = can_reload_predicates.CanExecuteAction();
			w_can_change_sightmode = can_change_sightmod_predicates.CanExecuteAction();
		}


		// weapon methods 

		public virtual void Fire()
		{
			if (Time.time > firetime && (w_can_fire))
			{
				w_firing = true;
				--w_magazine;
				firetime = Time.time + w_fire_rate;
				RaiseFireEvent();

	            Debug.Log("FIRE: " + w_firing.ToString(), this);

			}
		}

		public virtual IEnumerator Reload()
		{

	        Debug.Log("Reloading...");

			if (!(w_can_reload))
			{

	            Debug.Log("You cannot Reload.");

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

	        Debug.Log("Finished Reloading.");

		}

		public virtual void AddAmmo(int amount)
		{
			throw new NotImplementedException();
		}

		public virtual void ToggleSight()
		{
			if (Input.GetButtonUp(KeyboardInputManager.AIM_KEYNAME)
				&& can_change_sightmod_predicates.CanExecuteAction())
			{
				SetSight((SightMode == SightMode.Aim) ? SightMode.Normal : SightMode.Aim);
			}

		}

		public void SetSight(SightMode mode)
		{
			w_sight_mode = mode;
			RaiseSightModeChangedEvent();
		}

		public void ResetSight()
		{
			SetSight(SightMode.Normal);
		}

		// Properties

		public GameObject WeaponGameObject
		{
			get
			{
				return this.gameObject;
			}
		}

		public string Name
		{
			get
			{
				return w_name;
			}
		}

		public bool CanFire
		{
			get
			{
				return w_can_fire;
			}
		}

		public bool CanReload
		{
			get
			{
				return w_can_reload;
			}
		}

		public bool IsFiring
		{
			get
			{
				return w_firing;
			}
		}

		public bool IsReloading
		{
			get
			{
				return w_reloading;
			}
		}

		public int MagazineSize
		{
			get
			{
				return MagazineSize;
			}
		}

		public int Magazine
		{
			get
			{
				return w_magazine;
			}
		}

		public int Ammo
		{
			get
			{
				return w_ammo;
			}
		}

		public float FireRate
		{
			get
			{
				return w_fire_rate;
			}
		}

		public float FireRange
		{
			get
			{
				return w_fire_range;
			}
		}

		public float Damage
		{
			get
			{
				return w_damage;
			}
		}

		public SightMode SightMode
		{
			get
			{
				return w_sight_mode;
			}
		}
	}
}