using BepInEx.Configuration;

namespace MY_MOD_NAME;

public class Config(ConfigFile file) {
    public ConfigEntry<bool> ModEnabled { get; } = file.Bind("General", "Enabled", true, "Should the mod be enabled?");
}
