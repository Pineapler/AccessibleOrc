using BepInEx.Configuration;

namespace AccessibleOrc;

public class Config(ConfigFile file) {
    public ConfigEntry<bool> PaletteEnabled { get; } = file.Bind("General", "UseColorblindPalette", true, "Should the floating UI use the color palette provided in the plugins directory?");

}
