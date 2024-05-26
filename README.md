# Orc Massage Mod Template

## Setting up your mod info

1. Find and replace all `MY_MOD_NAME`, e.g. "OrcDance" (note: also rename directories and files)
2. Find and replace all `MY_LOWERCASE_MOD_NAME`, e.g. "orcdance"
3. Find and replace all `MY_LOWERCASE_USERNAME`, e.g. "pineapler"

## Setting up game assemblies

Game specific assemblies (Assembly-CSharp.dll, etc.) should not be tracked by version control. You must provide
your own copy of this file.

1. Copy `<game_install_dir>/OrcMassage_Data/Managed/Assembly-CSharp.dll` into `<mod_dir>/Libs`

If your mod requires access to other non-default Unity code, you will also need to copy the dll from the game directory 
and reference it in your project.
For example, if you needed access to Cinemachine:

1. Copy `<game_install_dir>/OrcMassage_Data/Managed/Cinemachine.dll` into `<mod_dir>/Libs`
2. Open your .csproj file and add a new entry under the comment `<!-- ADD ASSEMBLY REFERENCES HERE -->`
```xml
<Reference Include="Cinemachine">
    <HintPath>Libs\Cinemachine.dll</HintPath>
</Reference>
```

## Installing BepInEx

If your installation of Orc Massage has not yet been modded, you will need to install [BepInEx](https://github.com/BepInEx/BepInEx/releases).

Simply extract the zip folder into your `<game_install_dir>`.

I also recommend enabling the following setting in `<game_install_dir>/BepInEx/config/BepInEx.cfg`:
```toml
[Logging.Console]
Enabled = true
```

## Running your mod

Using your IDE of choice (or by running `dotnet build` from the terminal), build your project.
It should output a file: `bin/Debug/netstandard2.1/MY_MOD_NAME.dll`.

Copy this file into `<game_install_dir>/BepInEx/plugins/MY_MOD_NAME/MY_MOD_NAME.dll`, creating the directory if needed.

Run the game, and it should launch with your mod.

NOTE: I have included a Run configuration for JetBrains Rider that performs these steps automatically. 
Set the following environment variable for this configuration to work:
- `ORC_MASSAGE_DIR` - same as `<game_install_dir>`
