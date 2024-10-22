# Unified Settings Plus
A collection of "Connections" scripts that provide settings management for a number of properties, for use with the [Unified Settings asset by Kamgam](https://assetstore.unity.com/packages/tools/gui/settings-game-options-unified-menu-240015).

The following connections are currently available:

| Feature           | Connection Name               | BIRP | URP  | HDRP | Type   | Setting ID          |
| ----------------- | ----------------------------- | ---- | ---- | ---- | ------ | ------------------- |
| Unity 6 HDRP DLSS | UnityDLSSConnection           | ❌    | ❌    | ✔️    | Option | unitydlss           |
| Unity 6 HDRP FSR2 | UnityFSR2Connection           | ❌    | ❌    | ✔️    | Option | unityfsr2           |
| SSGI (toggle)     | CameraSSGIToggleConnection    | ❌    | ❌    | ✔️    | Bool   | cameratogglessgi    |
| Bloom (toggle)    | CameraBloomToggleConnection   | ❌    | ❌    | ✔️    | Bool   | cameratogglebloom   |
| Shadows (toggle)  | CameraShadowsToggleConnection | ❌    | ❌    | ✔️    | Bool   | cameratoggleshadows |
| GPU Instancer     | GPUInstancerConnection        | ✔️    | ✔️    | ✔️    | Option | gpuinstancer        |

The "Camera" connections are implementations of existing "Volume" based connections that instead use Camera "Custom Frame Settings". I've found these to be more reliable and most consistent than the volume based settings, but your experience may differ.

Over time, I may implement BIRP and URP versions of these, but my primary use case at the moment is HDRP.

## Installation

1. Copy the Scripts and Resources folders to your existing Assets\Kamgam\SettingsGenerator folder.

2. In your SettingsProvider Settings, add new entries for the connections that you want to use:

   ID - `setting ID from table above`

   Connection Object: `Scriptable object instance Connection Name from table above`

3. In your UI, drop a `DropdownUGUI` / `DropdownUGUIWithLabel` or `ToggleUGUI` prefab instance, and set the resolver ID to the corresponding `Setting ID`.

## Support

Provided "AS IS". Please consult the source code and Unified Settings user guides.
