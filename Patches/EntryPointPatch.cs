using HarmonyLib;
using MY_MOD_NAME.Scripts;
using Pineapler.Utils;
using UnityEngine;

namespace MY_MOD_NAME.Patches;

[HarmonyPatch]
public class EntryPointPatch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(GameManager), "Start")]
    private static void InsertEntryPoint(GameManager __instance) {
        Log.Info("Inserting Scene entry point");
        var entryPoint = new GameObject("MY_MOD_NAME Entry Point").AddComponent<EntryPoint>();
    }
    
}