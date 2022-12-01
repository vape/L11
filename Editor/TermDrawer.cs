using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace L11.Editor
{
    [CustomPropertyDrawer(typeof(Term))]
    public class TermDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(Term.Value));
            var contextProperty = property.FindPropertyRelative(nameof(Term.Context));
            var tooltipString = string.IsNullOrWhiteSpace(contextProperty.stringValue) ? "No context" : contextProperty.stringValue;

            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                EditorGUI.PropertyField(position, valueProperty, label);
                EditorGUI.LabelField(position, new GUIContent("", tooltipString));

                if (changeScope.changed)
                {
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}
