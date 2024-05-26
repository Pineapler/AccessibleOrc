using HarmonyLib;
using AccessibleOrc.Scripts;
using Pineapler.Utils;
using UnityEngine;

namespace AccessibleOrc.Patches;

[HarmonyPatch]
public class EntryPointPatch {
    // [HarmonyPostfix]
    // [HarmonyPatch(typeof(GameManager), "Start")]
    // private static void InsertEntryPoint(GameManager __instance) {
    // }
    
}