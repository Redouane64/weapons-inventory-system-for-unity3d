using UnityEngine;
using WeaponsInventorySystem.Abstraction;
using WeaponsInventorySystem.Helpers;
using WeaponsInventorySystem.Inputs;

namespace WeaponsInventorySystem
{
    public class Weapon : WeaponBase
    {

        protected override void Update()
        {
            base.Update();

            // fire
            if ((w_fire_mode == FireMode.Semi ? Input.GetButtonDown(KeyboardInputManager.FIRE_KEYNAME) : Input.GetButton(KeyboardInputManager.FIRE_KEYNAME)))
            {
                Fire();
            }
            else
            {
                w_firing = false;
            }

            // reload
            if (Input.GetButtonUp(KeyboardInputManager.RELOAD_KEYNAME))
            {
                StartCoroutine(Reload());
            }

			// Toggle sight mode
			ToggleSight();
        }

        private void OnDisable()
        {
			SetSight(SightMode.Normal);
        }
    }
}