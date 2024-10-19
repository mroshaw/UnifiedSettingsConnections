# UnifiedSettingsDynamicRes
An implementation of Unity 6 DLSS and FSR2 settings for the [Unified Settings asset by Kamgam](https://assetstore.unity.com/packages/tools/gui/settings-game-options-unified-menu-240015).

## Requirements

Developed and tested on Unity 6. DLSS requires a supported nVidia GPU.

## Installation

1. Copy the Scripts and Resources folders to your existing Assets\Kamgam\SettingsGenerator folder.
2. In your SettingsProvider Settings, add two new Groups:
   1. For DLSS:
      - ID - `unitydlss`
      - Connection Object: `UnityDLSSConnection`
   2. For FSR2:
      - ID - `unityfsr2`
      - Connection Object: `UnityFSR2Connection`
3. In your UI, drop a `DropdownUGUI` or `DropdownUGUIWithLabel` and set the resolver ID to `unitydlss` or `unityfsr2`

## Support

Provided "AS IS". Please consult the source code and Unified Settings user guides.
