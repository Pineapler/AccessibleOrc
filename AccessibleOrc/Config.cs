using AccessibleOrc.Scripts;
using BepInEx.Configuration;

namespace AccessibleOrc;

public class Config(ConfigFile file) {
    public ConfigEntry<bool> ModEnabled { get; } = file.Bind("General", "Enabled", true, "Should the mod be enabled?");

    public ConfigEntry<ColorblindType> ColorblindProfile { get; } = file.Bind("General", "ColorblindnessProfile", ColorblindType.Default, "Adjust HUD colors to be more visible with color vision deficiency.");
}
