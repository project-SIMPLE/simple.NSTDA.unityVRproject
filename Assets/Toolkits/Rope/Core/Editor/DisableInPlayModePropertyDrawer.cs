using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace RopeToolkit
{
    [CustomPropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class BeginLockInPlayModeDecoratorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var playing = Application.isPlaying;
            if (playing)
            {
                GUI.enabled = false;
            }

            var ranges = fieldInfo.GetCustomAttributes(typeof(RangeAttribute), true);
            var range = ranges != null && ranges.Length > 0 ? ranges[0] as RangeAttribute : null;
            if (range != null && property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUI.Slider(position, property, range.min, range.max);
            }
            else if (range != null && property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            if (playing)
            {
                GUI.enabled = true;
            }
        }
    }
}
