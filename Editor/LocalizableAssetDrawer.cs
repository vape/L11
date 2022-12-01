using UnityEditor;
using UnityEngine;

namespace L11.Editor
{
    [CustomPropertyDrawer(typeof(LocalizableAsset), true)]
    public class LocalizableAssetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(LocalizableAsset.Value));

            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                if (fieldInfo.FieldType == typeof(LocalizableAsset<>))
                {
                    EditorGUI.ObjectField(position, valueProperty, fieldInfo.FieldType.GenericTypeArguments[0], label);
                }
                else
                {
                    EditorGUI.ObjectField(position, valueProperty, label);
                }

                if (changeScope.changed)
                {
                    HandleTextureAssigned(valueProperty);

                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        private void HandleTextureAssigned(SerializedProperty property)
        {
            var tex2d = property.objectReferenceValue as Texture2D;
            if (tex2d != null && !AssetDatabase.IsSubAsset(tex2d))
            {
                property.objectReferenceValue = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(tex2d));
            }
        }
    }
}
