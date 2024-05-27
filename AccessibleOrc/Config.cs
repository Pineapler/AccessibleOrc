using AccessibleOrc.Scripts;
using BepInEx.Configuration;

namespace AccessibleOrc;

public class Config(ConfigFile file) {
    public ConfigEntry<bool> ModEnabled { get; } = file.Bind("General", "Enabled", true, "Should the mod be enabled?");

    public ConfigEntry<Luts.ColorblindType> ColorblindType { get; } = file.Bind("General", "Colorblindness", Luts.ColorblindType.None, "Adjust HUD colors to be more visible with color vision deficiency.");
}
