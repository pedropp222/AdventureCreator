using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AcaoTocarAnimacao))]
public class TocarAnimacaoAcaoEditor : Editor
{
    SerializedProperty animacaoComponente;
    SerializedProperty nomeAnimacao;
    SerializedProperty esperarQueAnimacaoTermine;
    SerializedProperty loop;

    List<string> listaAnimacao;
    int animacaoIndex;

    private void OnEnable()
    {
        animacaoComponente = serializedObject.FindProperty("animacaoComponente");
        nomeAnimacao = serializedObject.FindProperty("nomeAnimacao");
        esperarQueAnimacaoTermine = serializedObject.FindProperty("esperarQueAnimacaoTermine");
        loop = serializedObject.FindProperty("loop");
        listaAnimacao = new List<string>();

        if (animacaoComponente.objectReferenceValue != null)
        {
            RefreshListaAnimacao();
        }
        else
        {
            animacaoIndex = -1;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        animacaoComponente.objectReferenceValue = EditorGUILayout.ObjectField("Objeto Animacao: ",animacaoComponente.objectReferenceValue, typeof(Animation),true);

        if (EditorGUI.EndChangeCheck())
        {
            if (animacaoComponente.objectReferenceValue != null)
            {
                RefreshListaAnimacao();
            }
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        EditorGUI.BeginChangeCheck();

        if (animacaoComponente.objectReferenceValue != null )
        {
            if (listaAnimacao.Count == 0)
            {
                animacaoIndex = -1;
            }
            else
            {
                animacaoIndex = EditorGUILayout.Popup(animacaoIndex, listaAnimacao.ToArray());
            }

            if (!loop.boolValue)
            {
                esperarQueAnimacaoTermine.boolValue = EditorGUILayout.Toggle("Esperar que animacao termine: ", esperarQueAnimacaoTermine.boolValue);
            }

            loop.boolValue = EditorGUILayout.Toggle("Fazer Loop: ",loop.boolValue);
        }

        if (EditorGUI.EndChangeCheck())
        {
            nomeAnimacao.stringValue = listaAnimacao[animacaoIndex];

            if (loop.boolValue)
            {
                esperarQueAnimacaoTermine.boolValue = false;
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

    }

    private void RefreshListaAnimacao()
    {
        listaAnimacao.Clear();
        foreach(AnimationState an in (Animation)animacaoComponente.objectReferenceValue)
        {
            listaAnimacao.Add(an.name);
        }

        animacaoIndex = listaAnimacao.IndexOf(nomeAnimacao.stringValue);
    }
}