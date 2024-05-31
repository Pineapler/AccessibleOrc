# AccessibleOrc

Accessibility features for Orc Massage

## Color vision deficiency

![In game screenshot](Media/In_Game.png "In-game screenshot")

Replaces the color of some UI elements with a color palette that is more distinguishable for people with colorblindness.

We consulted two people with color vision deficiency. The modded color palette is optimized for Protanopia and should also work for Deuteranopia and Tritanopia. 

### Palette 

| Type             | Bad                                  | Level 1                              | Level 2                              | Level 3                              | Good                                 | too much                                  |
| ---------------- | ------------------------------------ | ------------------------------------ | ------------------------------------ | ------------------------------------ | ------------------------------------ | ----------------------------------------- |
| Symbol           | ⬤                                    | ⬤                                    | ⬤                                    | ⬤                                    | **♥**                                | **✗**                                  |


| Original | ![Media/Default_Large](Media/Default_Large) |
| --- | --- |
| Modded | ![Modded_Large](Media/Palette_Large) |


### Custom palettes

If the modded color palette is not suitable to you, you may wish to use your own palette.

You use your own palette by creating a new image named `Custom.png` file in the `Orc Massage\BepInEx\plugins\AccessibleOrc\Palettes` folder.

1. Create a 6x1 pixel image using an image editor of your choice (e.g. Aseprite, Photoshop, MS paint)
2. Use the pencil tool to choose your colors
3. Save the image to `Orc Massage\BepInEx\Plugins\AccessibleOrc\Palettes\Custom.png`

