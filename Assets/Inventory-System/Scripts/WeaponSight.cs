using System;
using UnityEngine;
using WeaponsInventorySystem.Helpers;
using WeaponsInventorySystem.Inputs;

namespace WeaponsInventorySystem
{
    public class WeaponSight : MonoBehaviour
    {
        [SerializeField]
        private Vector3 w_aim_position;
        [SerializeField]
        private Vector3 w_normal_position;

        [SerializeField]
        private float speed = 8f;
        private bool isAiming = false;
        private ActionPredicates can_aim_predicates = new ActionPredicates();

		public event EventHandler<WeaponSightEventArgs> WeaponSightModeChanged;

        void Awake()
        {
            ResetSight();

			WeaponSightModeChanged += new EventHandler<WeaponSightEventArgs>(OnWeaponSightChangedHandler);

            can_aim_predicates.AddPredicate(() => !Inventory.Current.CurrentWeapon.IsReloading);
            can_aim_predicates.AddPredicate(() => !Inventory.Current.IsChangingItem);
        }

		void Update()
        {
            if (Input.GetButtonUp(KeyboardInputManager.AIM_KEYNAME)
                && can_aim_predicates.CanExecuteAction())
			{
				isAiming = !isAiming;
			}
			// TO DO
			OnWeaponSightModeChanged();
		}

		private void OnWeaponSightChangedHandler(object sender, WeaponSightEventArgs e)
		{
			if (e.IsAiming)
			{
				Aim();
			}
			else
			{
				Normal();
			}
		}

		protected void OnWeaponSightModeChanged()
		{
			if(WeaponSightModeChanged != null)
			{
				WeaponSightModeChanged(this, new WeaponSightEventArgs(isAiming));
			}
		}

        private void Aim()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, w_aim_position, speed * Time.deltaTime);
        }

        private void Normal()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, w_normal_position, speed * Time.deltaTime);
        }

        public void ResetSight()
        {
            this.transform.localPosition = w_normal_position;
            isAiming = false;
        }

        public Vector3 AimPosition { get { return w_aim_position; } set { w_aim_position = value; } }

        public Vector3 NormalPosition { get { return w_normal_position; } set { w_normal_position = value; } }
    }
}