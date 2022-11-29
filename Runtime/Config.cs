using UnityEngine;

namespace L11
{
    public class Config : ScriptableObject
    {
        private const string FileName = "L11 Config";

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Assets/L11/Create Config")]
        public static void CreateConfig()
        {
            var asset = Resources.Load<Config>(FileName);

            if (asset == null)
            {
                var dir = System.IO.Path.Combine("Assets", "Resources");
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                var path = System.IO.Path.Combine(dir, $"{FileName}.asset");
                asset = ScriptableObject.CreateInstance<Config>();

                UnityEditor.AssetDatabase.CreateAsset(asset, path);
                UnityEditor.AssetDatabase.ImportAsset(path);
            }
            
            UnityEditor.Selection.SetActiveObjectWithContext(asset, null);
        }
#endif

        internal static Config Locate()
        {
            return Resources.Load<Config>(FileName);
        }

        public string DefaultLocaleId;
        public LocalePreset[] Locales;
    }
}
