using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ControladorVariaveis))]
public class ControladorVariaveisEditor : Editor
{
    private SerializedProperty listaVariaveis;

    ReorderableList lista;

    private void OnEnable()
    {
        listaVariaveis = serializedObject.FindProperty("listaVariaveis");
        lista = new ReorderableList(serializedObject,listaVariaveis);
        lista.drawElementCallback += DrawElementNovo;
    }

    void DrawElementNovo(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty var = listaVariaveis.GetArrayElementAtIndex(index);
        EditorGUI.PropertyField(rect,var);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(listaVariaveis);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}