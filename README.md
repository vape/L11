# L11
Lightweight localization library. Supports string and assets localization.

## Installation
Primary installation method is using unity's pacakge manager.

## Usage
1. Create main configuration file using `Assets/L11/Create Config` option.
2. Create `LocalePreset` for each language you need to support. You can do this with `Assets/Create/L11/Locale` menu option.
3. Add presets to configuration file

Each locale preset contains primary and external dictionaries. Primary is serialized along with preset, external can be created separataly and added to any preset.

### Code
Any string can be used as key and localized using extension method:
```csharp
string value1 = "key1".Localize();
string value2 = "key2".Localize(fallback: "No value")
```
#### Pluralization
Almost all languages can use pluralization feature based on [gettest plural rules](https://www.gnu.org/software/gettext/manual/gettext.html#Plural-forms).
```csharp
// keys:
// "coins": "{0} {0:Coins|Coin}"
string value = "coins".Localize(1); // '1 Coin'
string value = "coins".Localize(1234); // '1234 Coins' 
```

### Components
For now, it has just a few helper components:
#### TextMeshProLocalizer
When attached to object with TextMeshPro component, localizes it's text with selected key or fallback value
#### ImageLocalizer
When attached to object with Image component, will use sprite based on selected key or fallback asset

## Plugins
Supports sync with POEditor with plugin https://github.com/vape/L11-POEditor
