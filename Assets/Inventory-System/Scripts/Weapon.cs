using UnityEngine;
using System.Collections;
using System;

public sealed class Weapon : WeaponBase
{

    protected override void Update()
    {
        // fire
        if ((w_fire_mode == FireMode.Semi ? Input.GetButtonDown(KeyboardInputManager.FIRE_KEYNAME) : Input.GetButton(KeyboardInputManager.FIRE_KEYNAME)))
        {
            Fire();
        }

        // reload
        if (Input.GetButtonUp(KeyboardInputManager.RELOAD_KEYNAME))
        {
            StartCoroutine(Reload());
        }

    }

}
