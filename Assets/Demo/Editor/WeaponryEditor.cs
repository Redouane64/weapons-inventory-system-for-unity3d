using UnityEngine;
using System.Collections;
using UnityEditor;
using WeaponsInventorySystem;

public class WeaponeryEditor : Editor
{

    private static Transform selectedTransform = null;

    [MenuItem("FPS Weaponry/Set As Normal Sight")]
    public static void SetAsDefaultPosition()
    {
        if (!(selectedTransform = Selection.activeTransform) && Selection.activeTransform.CompareTag("Game Weapon"))
            return;
        selectedTransform.GetComponent<WeaponSight>().NormalPosition = (selectedTransform.localPosition);
    }

    [MenuItem("FPS Weaponry/Set As Aim Sight")]
    public static void SetAsIronSightPosition()
    {
        if (!(selectedTransform = Selection.activeTransform) && Selection.activeTransform.CompareTag("Game Weapon"))
            return;
        selectedTransform.GetComponent<WeaponSight>().AimPosition = (selectedTransform.localPosition);
    }

    [MenuItem("FPS Weaponry/Set To Normal Sight")]
    public static void SetToDefaultPosition()
    {
        if (!(selectedTransform = Selection.activeTransform) && Selection.activeTransform.CompareTag("Game Weapon"))
            return;
        selectedTransform.localPosition = selectedTransform.GetComponent<WeaponSight>().NormalPosition;
    }

    [MenuItem("FPS Weaponry/Set To Aim Sight")]
    public static void SetToIronSightPosition()
    {
        if (!(selectedTransform = Selection.activeTransform) && Selection.activeTransform.CompareTag("Game Weapon"))
            return;
        selectedTransform.localPosition = selectedTransform.GetComponent<WeaponSight>().AimPosition;
    }

}
