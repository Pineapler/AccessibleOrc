using System;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using Pineapler.Utils;
using UnityEngine;

namespace AccessibleOrc.Scripts;

public static class Luts {

    public static bool Loaded { get; private set; }
    
    private static CpuTexture3D _identity;
    private static CpuTexture3D _deuteranopia;
    private static CpuTexture3D _protanopia;
    private static CpuTexture3D _tritanopia;

    private static ColorblindType _currentColorblindType = ColorblindType.Default;

    private static Color[] _colorPalette;
    
    public static ColorblindType CurrentColorblindType {
        get => _currentColorblindType;
        set => _currentColorblindType = value;
    }

    
    public static void LoadTextures() {
        string current = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string deps = Path.Combine(current, "RuntimeDeps", "Luts");
        
        _identity = new CpuTexture3D();
        _identity.LoadImage(Path.Combine(deps, "identity.png"));

        _deuteranopia = new CpuTexture3D();
        _deuteranopia.LoadImage(Path.Combine(deps, "deuteranopia_dalto.png"));

        _protanopia = new CpuTexture3D();
        _protanopia.LoadImage(Path.Combine(deps, "protanopia_dalto.png"));
        
        _tritanopia = new CpuTexture3D();
        _tritanopia.LoadImage(Path.Combine(deps, "tritanopia_dalto.png"));


        Texture2D palette = new Texture2D(6, 1);
        palette.LoadImage(File.ReadAllBytes(Path.Combine(current, "RuntimeDeps", "Palettes", "Default.png")));
        _colorPalette = palette.GetPixels();
        
        LookupColors(_colorPalette, ColorblindType.Deuteranopia);
        LookupColors(_colorPalette, ColorblindType.Protanopia);
        LookupColors(_colorPalette, ColorblindType.Tritanopia);

        Loaded = true;
    }
    
    

    public static Color Lookup(Color color) {
        if (!Plugin.Config.ModEnabled.Value) return color;

        CpuTexture3D lut = _currentColorblindType switch {
            ColorblindType.Default => _identity,
            ColorblindType.Deuteranopia => _deuteranopia,
            ColorblindType.Protanopia => _protanopia,
            ColorblindType.Tritanopia => _tritanopia,
            _ => _identity
        };

        return lut.Sample(color);
    }

    public static void LookupColors(Color[] colors, ColorblindType type) {
        ColorblindType tempType = _currentColorblindType;

        _currentColorblindType = type;
        Log.Info(type);
        foreach (Color color in colors) {
            Color lookedUp = Lookup(color);
            int r = Mathf.RoundToInt(lookedUp.r * 255);
            int g = Mathf.RoundToInt(lookedUp.g * 255);
            int b = Mathf.RoundToInt(lookedUp.b * 255);
            Log.Info($"{r:D3}, {g:D3}, {b:D3}");
        }

        _currentColorblindType = tempType;
}


    public static void OnColorblindLutTypeChanged(object sender, EventArgs args) {
        ConfigEntry<ColorblindType> configEntry = (ConfigEntry<ColorblindType>)sender;
        CurrentColorblindType = configEntry.Value;
    }
    

}