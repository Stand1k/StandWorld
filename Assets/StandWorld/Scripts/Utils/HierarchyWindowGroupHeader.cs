using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace StandWorld.Utils
{
    [InitializeOnLoad]
    public static class HierarchyWindowGroupHeader
    {
        static HierarchyWindowGroupHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject != null && gameObject.name.StartsWith("---", System.StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(selectionRect, Color.gray);
                EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("-", "").ToUpperInvariant());
            }
        }
    }
}
#endif