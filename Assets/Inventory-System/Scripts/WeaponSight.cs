using System;
using UnityEngine;
using WeaponsInventorySystem.Abstraction;
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
		private Weapon weapon;

        void Awake()
        {
            ResetSight();

			weapon = GetComponent<Weapon>();
			weapon.OnSightModeChanged += new EventHandler<WeaponSightModeChangedEventArgs>(WeaponSightToggled);
        }

		private void WeaponSightToggled(object sender, WeaponSightModeChangedEventArgs e)
		{
			isAiming = e.Mode == SightMode.Aim;
		}

		void Update()
        {

			if (isAiming)
			{
				Aim();
			}
			else
			{
				Normal();
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
        }

        public Vector3 AimPosition { get { return w_aim_position; } set { w_aim_position = value; } }

        public Vector3 NormalPosition { get { return w_normal_position; } set { w_normal_position = value; } }
    }
}