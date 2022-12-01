using L11.Localizers;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace L11.Editor.Localizers
{
    [CustomEditor(typeof(BaseLocalizer), true)]
    public class BaseLocalizerEditor : UnityEditor.Editor
    {
        private LocaleKeyDropdown keysDropdown;

        public override void OnInspectorGUI()
        {
            var target = this.target as BaseLocalizer;
            var keySource = LocalizerKeySource.Terms;

            var keySourceAttribute = Attribute.GetCustomAttribute(target.GetType(), typeof(LocalizerUseKeysFromAttribute)) as LocalizerUseKeysFromAttribute;
            if (keySourceAttribute != null)
            {
                keySource = keySourceAttribute.Source;
            }

            var rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            DrawKeyField(target, rect, keySource);

            OnCustomLocalizerGUI();
        }

        protected virtual void OnCustomLocalizerGUI()
        {
            var iter = serializedObject.GetIterator();
            var enterChild = true;

            while (iter.NextVisible(enterChild))
            {
                enterChild = false;

                switch (iter.name)
                {
                    case "m_Script":
                    case "key":
                        continue;

                    default:
                        EditorGUILayout.PropertyField(iter);
                        break;
                }
            }
        }

        private void OnKeySelected(string key)
        {
            serializedObject.FindProperty("key").stringValue = key;
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawKeyField(BaseLocalizer target, Rect rect, LocalizerKeySource keySource)
        {
            if (keysDropdown == null)
            {
                var config = Config.Locate();
                var keys = new HashSet<string>();

                FetchKeys(config, keys, keySource);
                keysDropdown = new LocaleKeyDropdown(keys, new AdvancedDropdownState(), OnKeySelected);
            }

            var dropdownRect = EditorGUI.PrefixLabel(rect, new GUIContent("Key"));
            var contentString = string.IsNullOrWhiteSpace(target.Key) ? "None" : target.Key;

            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(contentString), FocusType.Keyboard))
            {
                keysDropdown.Show(dropdownRect);
            }
        }

        private void FetchKeys(Config config, HashSet<string> keys, LocalizerKeySource source)
        {
            switch (source)
            {
                case LocalizerKeySource.Terms:
                    {
                        foreach (var locale in config.Locales)
                        {
                            foreach (var term in locale.PrimaryTerms)
                            {
                                keys.Add(term.Key);
                            }

                            foreach (var externalDicts in locale.ExternalTermDictionaries)
                            {
                                foreach (var externalTerm in externalDicts.Terms)
                                {
                                    keys.Add(externalTerm.Key);
                                }
                            }
                        }
                    }
                    break;
                case LocalizerKeySource.Assets:
                    {
                        foreach (var locale in config.Locales)
                        {
                            foreach (var asset in locale.PrimaryAssets)
                            {
                                keys.Add(asset.Key);
                            }

                            foreach (var externalAssets in locale.ExternalAssetDictionaries)
                            {
                                foreach (var externalAsset in externalAssets.Assets)
                                {
                                    keys.Add(externalAsset.Key);
                                }
                            }
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException($"Fetching keys from {source} is not implemented");
            }
        }
    }
}
