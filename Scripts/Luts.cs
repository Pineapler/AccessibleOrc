using System;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;

namespace AccessibleOrc.Scripts;

public static class Luts {
    
    public enum ColorblindType {
        None,
        Deuteranopia,
        Protanopia,
        Tritanopia,
    }

    public static bool Loaded { get; private set; }
    
    private static CpuTexture3D _identity;
    private static CpuTexture3D _deuteranopia;
    private static CpuTexture3D _protanopia;
    private static CpuTexture3D _tritanopia;

    private static ColorblindType _currentColorblindType = ColorblindType.None;
    
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
        
        // _deuteranopiaDalto = new CpuTexture3D();
        // _deuteranopiaDalto.LoadImage(Path.Combine(deps, "deuteranopia_dalto.png"));
        //
        // _protanopiaDalto = new CpuTexture3D();
        // _protanopiaDalto.LoadImage(Path.Combine(deps, "protanopia_dalto.png"));
        //
        // _tritanopiaDalto = new CpuTexture3D();
        // _tritanopiaDalto.LoadImage(Path.Combine(deps, "tritanopia_dalto.png"));

        Loaded = true;
    }
    
    

    public static Color Lookup(Color color) {
        if (!Plugin.Config.ModEnabled.Value) return color;

        CpuTexture3D lut = _currentColorblindType switch {
            ColorblindType.None => _identity,
            ColorblindType.Deuteranopia => _deuteranopia,
            ColorblindType.Protanopia => _protanopia,
            ColorblindType.Tritanopia => _tritanopia,
            _ => _identity
        };

        return lut.Sample(color);
    }

    
    public static void OnColorblindLutTypeChanged(object sender, EventArgs args) {
        ConfigEntry<ColorblindType> configEntry = (ConfigEntry<ColorblindType>)sender;
        CurrentColorblindType = configEntry.Value;
    }

}