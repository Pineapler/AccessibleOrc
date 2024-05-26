using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace AccessibleOrc.Scripts;

public static class Luts {
    
    public enum LutType {
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

    private static LutType _currentLut = LutType.None;
    
    public static LutType CurrentLut {
        get => _currentLut;
        set => _currentLut = value;
    }

    
    public static void LoadTextures() {
        string current = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string deps = Path.Combine(current, "RuntimeDeps", "Luts");
        
        _identity = new CpuTexture3D();
        _identity.LoadImage(Path.Combine(deps, "identity.png"));

        _deuteranopia = new CpuTexture3D();
        _deuteranopia.LoadImage(Path.Combine(deps, "deuteranopia.png"));

        _protanopia = new CpuTexture3D();
        _protanopia.LoadImage(Path.Combine(deps, "protanopia.png"));
        
        _tritanopia = new CpuTexture3D();
        _tritanopia.LoadImage(Path.Combine(deps, "tritanopia.png"));

        Loaded = true;
    }
    
    

    public static Color Lookup(Color color) {
        if (!Plugin.Config.ModEnabled.Value) return color;
        
        Debug.Log($"{_currentLut}");
        
        CpuTexture3D lut = _currentLut switch {
            LutType.None => _identity,
            LutType.Deuteranopia => _deuteranopia,
            LutType.Protanopia => _protanopia,
            LutType.Tritanopia => _tritanopia,
            _ => _identity
        };

        return lut.Sample(color);
    }

    public static void OnColorblindConfigChanged(object sender, EventArgs args) {
        ConfigEntry<LutType> configEntry = (ConfigEntry<LutType>)sender;
        CurrentLut = configEntry.Value;
    }

}