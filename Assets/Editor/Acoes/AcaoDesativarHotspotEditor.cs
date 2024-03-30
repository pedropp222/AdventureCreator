using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AcaoDesativarHotspot))]
public class AcaoDesativarHotspotEditor : Editor
{
    SerializedProperty hotspot;
    SerializedProperty todos;
    SerializedProperty este;

    GUIContent esteCont;
    GUIContent todosCont;
    GUIContent hotCont;

    private void OnEnable()
    {
        hotspot = serializedObject.FindProperty("hotspot");
        todos = serializedObject.FindProperty("todos");
        este = serializedObject.FindProperty("este");

        esteCont = new GUIContent("Este Hotspot");
        todosCont = new GUIContent("Todos os Hotspot da Frame");
        hotCont = new GUIContent("Selecionar Hotspot");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        if (!este.boolValue)
        {
            EditorGUILayout.PropertyField(todos,todosCont);
        }

        if (!todos.boolValue)
        {
            EditorGUILayout.PropertyField(este,esteCont);
        }

        if (!este.boolValue && !todos.boolValue)
        {
            EditorGUILayout.PropertyField(hotspot,hotCont);
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}