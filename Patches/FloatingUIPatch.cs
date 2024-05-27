using HarmonyLib;
using AccessibleOrc.Scripts;
using Pineapler.Utils;
using UnityEngine;

namespace AccessibleOrc.Patches;

[HarmonyPatch]
public class FloatingUIPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(FloatingUIHolder), "Awake")]
    private static void InitFloatingUIPatch(FloatingUIHolder __instance) {
        Palette.holder = __instance;
        if (Plugin.Config.PaletteEnabled.Value) {
            Palette.Load();
            Palette.Apply();
        }
    }
    
}