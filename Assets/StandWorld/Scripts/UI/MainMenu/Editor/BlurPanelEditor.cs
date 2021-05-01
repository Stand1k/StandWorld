using StandWorld.UI.MainMenu;
using UnityEditor;
using UnityEditor.UI;

namespace StandWorld.UI.MainMenu
{
    [CustomEditor((typeof(BlurPanel)))]
    public class BlurPanelEditor : ImageEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("time"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"));

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
