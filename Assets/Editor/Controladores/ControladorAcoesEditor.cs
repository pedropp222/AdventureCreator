using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ControladorAcoes))]
public class ControladorAcoesEditor : Editor
{
    List<JogoAcao> listaAcoes;
    ControladorAcoes este = null;

    int sel = -1;

    //SerializedProperty tipo;

    //Controlador ctr;

    ListaAcoesEditor listaDesenhador;

    private void OnEnable()
    {
        if (Selection.activeTransform == null)
        {
            return;
        }

        listaDesenhador = ScriptableObject.CreateInstance<ListaAcoesEditor>();

        //ctr = FindObjectOfType<Controlador>();

        este = (ControladorAcoes)target;

        listaAcoes = este.listaAcoes;

        //tipo = serializedObject.FindProperty("tipoAcao");

        listaDesenhador.Inicializar(listaAcoes);
    }

    private void OnDisable()
    {
        ScriptableObject.DestroyImmediate(listaDesenhador);
    }

    private void OnDestroy()
    {
        ScriptableObject.DestroyImmediate(listaDesenhador);
    }

    public override void OnInspectorGUI()
    {
        if (este == null)
        {
            return;
        }

        if (listaDesenhador.fatalError)
        {
            GUILayout.Label("Erro ao criar editores");
            return;
        }

        este.tipoAcao = (ControladorAcoes.AcaoTipo)EditorGUILayout.EnumPopup("Tipo de Acao: ",este.tipoAcao);

        if (este.tipoAcao == ControladorAcoes.AcaoTipo.HOTSPOT_CLICK)
        {
            GUILayout.Label("Estas ações vão ser executadas ao CLICAR neste hotspot");
        }
        else if (este.tipoAcao == ControladorAcoes.AcaoTipo.ENTRAR_FRAME)
        {
            GUILayout.Label("Estas ações vão ser executadas ao ENTRAR nesta frame");
        }
        else if (este.tipoAcao == ControladorAcoes.AcaoTipo.SAIR_FRAME)
        {
            GUILayout.Label("Estas ações vão ser executadas ao SAIR desta frame");
        }

        GUILayout.Label("Lista de Acoes:");

        EditorGUI.indentLevel += 2;

        listaDesenhador.opcoesFrames.Clear();

        sel = 0;

        if (listaAcoes.Count != listaDesenhador.editoresAcoes.Count)
        {
            return;
        }

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < listaAcoes.Count; i++)
        {
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            AcaoAtributo x = (AcaoAtributo)listaAcoes[i].GetType().GetCustomAttributes(false)[0];
            EditorGUILayout.LabelField("Ação " + i, x.nomeBonito);

            GUILayout.Space(10f);


            try
            {
                listaDesenhador.editoresAcoes[i].OnInspectorGUI();
            }
            catch (Exception c)
            {
                EditorGUILayout.HelpBox("Erro ao apresentar açao: " + listaDesenhador.editoresAcoes[i].name + " - " + c.Message, MessageType.Error);
            }


            GUILayout.Space(5f);
            if (GUILayout.Button("Apagar", GUILayout.Width(150f)))
            {
                ApagarAcao(i);
            }
            GUILayout.EndVertical();
        }

        EditorGUI.indentLevel -= 2;

        GUILayout.Space(15f);
        GUILayout.Label("Nova Ação:");

        foreach (Type tipo in listaDesenhador.tiposAcoes)
        {
            string nomeFinal = "Seleciona uma ação...";

            if (tipo != null)
            {
                nomeFinal = tipo.Name;
                foreach (object attr in tipo.GetCustomAttributes(false))
                {
                    if (attr is AcaoAtributo n)
                    {
                        nomeFinal = n.nomeBonito;
                        break;
                    }
                }
            }

            listaDesenhador.opcoesFrames.Add(nomeFinal);
        }


        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }


        EditorGUI.BeginChangeCheck();
        sel = EditorGUILayout.Popup(sel, listaDesenhador.opcoesFrames.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            if (sel != 0)
            {
                NovaAcao(sel);
                sel = 0;
            }
        }
    }

    public void ApagarAcao(int x)
    {
        listaDesenhador.ApagarEditor(x);
        DestroyImmediate(listaAcoes[x]);
        listaAcoes.RemoveAt(x);
        este.listaAcoes = listaAcoes;
    }

    public void NovaAcao(int i)
    {
        JogoAcao tc = (JogoAcao)este.gameObject.AddComponent(listaDesenhador.tiposAcoes[i]);

        tc.hideFlags = HideFlags.HideInInspector;

        listaAcoes.Add(tc);
        listaDesenhador.CriarEditores(listaAcoes);
        este.listaAcoes = listaAcoes;
    }
}
