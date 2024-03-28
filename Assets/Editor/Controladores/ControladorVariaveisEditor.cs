using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ControladorVariaveis))]
public class ControladorVariaveisEditor : Editor
{
    private SerializedProperty listaVariaveis;

    string[] bools = { "false", "true" };

    List<bool> variaveisMostrar;

    bool varsFold = true;

    private void OnEnable()
    {
        listaVariaveis = serializedObject.FindProperty("listaVariaveis");

        variaveisMostrar = new List<bool>();

        for(int i = 0; i < listaVariaveis.arraySize;i++)
        {
            variaveisMostrar.Add(true);
        }
    }

    public override void OnInspectorGUI()
    {
        varsFold = EditorGUILayout.Foldout(varsFold, "Variaveis");

        EditorGUI.BeginChangeCheck();

        if (varsFold)
        {
            for (int i = 0; i < listaVariaveis.arraySize; i++)
            {
                SerializedProperty var = listaVariaveis.GetArrayElementAtIndex(i);

                variaveisMostrar[i] = EditorGUILayout.Foldout(variaveisMostrar[i], (i+1)+" - "+var.FindPropertyRelative("nome").stringValue);

                if (variaveisMostrar[i])
                {
                    EditorGUILayout.PropertyField(var.FindPropertyRelative("nome"));

                    if (((TipoVariavel)var.FindPropertyRelative("tipoVariavel").enumValueIndex) == TipoVariavel.TEXTO)
                    {
                        EditorGUILayout.PropertyField(var.FindPropertyRelative("valor"));
                    }
                    else if (((TipoVariavel)var.FindPropertyRelative("tipoVariavel").enumValueIndex) == TipoVariavel.BOOLEANO)
                    {
                        if (!bool.TryParse(var.FindPropertyRelative("valor").stringValue, out _))
                        {
                            var.FindPropertyRelative("valor").stringValue = "false";
                        }

                        var.FindPropertyRelative("valor").stringValue = EditorGUILayout.Toggle("Valor ",bool.Parse(var.FindPropertyRelative("valor").stringValue)).ToString();
                    }
                    else
                    {
                        if (!int.TryParse(var.FindPropertyRelative("valor").stringValue,out _))
                        {
                            var.FindPropertyRelative("valor").stringValue = "0";
                        }

                        var.FindPropertyRelative("valor").stringValue = EditorGUILayout.IntField("Valor ", int.Parse(var.FindPropertyRelative("valor").stringValue)).ToString();
                    }

                    EditorGUILayout.PropertyField(var.FindPropertyRelative("tipoVariavel"));

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    GUILayout.Space(10f);
                }
            }          
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}