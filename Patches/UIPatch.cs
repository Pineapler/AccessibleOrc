using AccessibleOrc.Scripts;
using HarmonyLib;
using UnityEngine;

namespace AccessibleOrc.Patches;

[HarmonyPatch]
public class UIPatch {

    [HarmonyPrefix]
    [HarmonyPatch(typeof(MassageFloatingUI), "SetMaterialColor")]
    private static void MassageFloatingUI_ApplyLut(ref Color _color) {
        _color = Luts.Lookup(_color);
    }
}