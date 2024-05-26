using UnityEngine;

namespace AccessibleOrc.Scripts;

// Assumes cubic texture dimensions
public class CpuTexture3D {
    public Color[] colors;
    public int width;
    public bool filtered;

    public CpuTexture3D() { }

    
    public bool LoadImage(string filename) {
        bool ok;
        Texture2D tempTex = new Texture2D(2, 2);
        
        ok = tempTex.LoadImage(System.IO.File.ReadAllBytes(filename));
        if (!ok) return false;

        width = tempTex.height;
        colors = tempTex.GetPixels();

        return true;
    }
    

    public Color Sample(float u, float v, float w) {
        u = Mathf.Clamp01(u);
        v = Mathf.Clamp01(v);
        w = Mathf.Clamp01(w);

        u *= width;
        v *= width;
        w *= width;

        int u_floor = Mathf.FloorToInt(u);
        int v_floor = Mathf.FloorToInt(v);
        int w_floor = Mathf.FloorToInt(w);

        int idx_floor = FlattenIndex(u_floor, v_floor, w_floor);
        
        int u_ceil = Mathf.CeilToInt(u);
        int v_ceil = Mathf.CeilToInt(v);
        int w_ceil = Mathf.CeilToInt(w);

        int idx_ceil = FlattenIndex(u_ceil, v_ceil, w_ceil);

        float u_t = u % 1;
        float v_t = v % 1;
        float w_t = w % 1;

        float t = (u_t + v_t + w_t) / 3;

        Color color = Color.Lerp(colors[idx_floor], colors[idx_ceil], t);
        return color;
    }
    

    public Color Sample(Color color) {
        return Sample(color.r, color.g, color.b);
    }


    public int FlattenIndex(int u, int v, int w) {
        int accumulator = 0;

        accumulator += w * width * width;
        accumulator += v * width;
        accumulator += u;
        
        return accumulator;
    }
    
}