
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AcaoAlterarVariavel))]
public class AcaoAlterarVariavelEditor : Editor
{
    SerializedProperty varId;
    SerializedProperty varValor;
    SerializedProperty operacao;

    string[] vars;

    private void OnEnable()
    {
        varId = serializedObject.FindProperty("variavelId");
        varValor = serializedObject.FindProperty("valorAlterar");
        operacao = serializedObject.FindProperty("operacao");

        vars = FindAnyObjectByType<ControladorVariaveis>().listaVariaveis.Select(x => x.nome+" ("+x.tipoVariavel.ToString()+")").ToArray();
    }

    public override void OnInspectorGUI()
    {
        if (vars.Length == 0)
        {
            EditorGUILayout.HelpBox("Erro: Nao existem variaveis",MessageType.Warning);
        }
        else
        {
            EditorGUI.BeginChangeCheck();

            varId.intValue = EditorGUILayout.Popup(new GUIContent("Variavel"),varId.intValue, vars);

            EditorGUILayout.PropertyField(operacao);

            EditorGUILayout.PropertyField(varValor);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
        }
    }
}