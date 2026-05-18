# DownfallU
An unofficial port of the Downfall mod from Slay the Spire 1 to Slay the Spire 2.

Archived now as I'm currently developing https://github.com/lamali292/Downfall

## Setup
1. Init `Directory.Build.props`:

<Project>
    <PropertyGroup>
        <!-- Megadot current version is 4.5.1, and the game won't load your .pck if the Godot version used is newer. -->
        <!-- MegaDot / Godot 4.5.1 mono executable used for export-pack when publishing -->
        <GodotPath>PATH_TO_GODOT/Godot_v4.5.1-stable_mono_win64.exe</GodotPath>

        <!-- If sts2 is not found automatically, uncomment the following and set it manually -->
        <!-- <Sts2Path>?/steamapps/common/Slay the Spire 2</Sts2Path> -->
    </PropertyGroup>
</Project>

2. Run `./build.sh`