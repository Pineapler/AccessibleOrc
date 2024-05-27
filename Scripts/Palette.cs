using System;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using Pineapler.Utils;
using UnityEngine;

namespace AccessibleOrc.Scripts;

public static class Palette {

    public static Color[] colors;
    public static Color dangerColor;
    
    public static Color[] colorsOriginal;
    public static Color dangerColorOriginal;

    public static FloatingUIHolder holder;

    private static bool _hasPreviouslyApplied = false;
    
    public static void Load() {
        string current = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string palettePath = Path.Combine(current, "Palettes", "Palette.png");
        
        Texture2D srcTex = new Texture2D(6, 1);
        srcTex.LoadImage(File.ReadAllBytes(palettePath));

        if (srcTex.width != 6 || srcTex.height != 1) {
            Log.Error("Palette texture has invalid dimensions. Please make sure it's 6x1 pixels.");
        }

        
        // Unmarshall our palette format
        Color[] tempColors = srcTex.GetPixels();
        dangerColor = tempColors[5];
        colors = [tempColors[4], tempColors[3], tempColors[2], tempColors[1], tempColors[0]];
    }

    
    public static void Apply() {
        if (!_hasPreviouslyApplied) {
            _hasPreviouslyApplied = true;
            dangerColorOriginal = holder.dangerColor;
            colorsOriginal = holder.Colors;
        }

        holder.dangerColor = dangerColor;
        holder.Colors = colors;
    }

    
    public static void ResetPalette() {
        if (!_hasPreviouslyApplied) return;
        holder.dangerColor = dangerColorOriginal;
        holder.Colors = colorsOriginal;
    }


    public static void OnPaletteEnabledChanged(object sender, EventArgs args) {
        ConfigEntry<bool> configEntry = (ConfigEntry<bool>)sender;

        if (holder == null) return;
        if (configEntry.Value == false) {
            ResetPalette();
        }
        else {
            Load();
            Apply();
        }
    }
}