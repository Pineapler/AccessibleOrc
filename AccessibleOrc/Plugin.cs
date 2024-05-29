using Pineapler.Utils;
using System.Reflection;
using AccessibleOrc.Scripts;
using BepInEx;
using HarmonyLib;

namespace AccessibleOrc;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin {
    // ==========================================================
    // GAME CONFIGURATION
    public const string PluginGuid = "com.pineapler.accessibleorc";
    public const string PluginName = "AccessibleOrc";
    public const string PluginVersion = "1.0.0";
    // ==========================================================

    public static new Config Config { get; private set; }
    
    private void Awake() {
        Config = new Config(base.Config);
        
        Log.SetSource(Logger);
        Log.Info($"Plugin {PluginGuid} is starting");

        Config.PaletteEnabled.SettingChanged += Palette.OnPaletteEnabledChanged;

        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}
