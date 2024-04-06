using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AcaoLogica))]
public class AcaoLogicaEditor : Editor
{
    ControladorVariaveis ctrVars;

    List<string> listaVars;

    AcaoLogica este;

    public List<Condicao> conditions;

    ListaAcoesEditor listDrawerTrue;
    ListaAcoesEditor listDrawerFalse;

    int sel = 0;

    private void OnEnable()
    {
        try
        {
            listaVars = new List<string>();

            ctrVars = FindObjectOfType<ControladorVariaveis>();

            for (int i = 0; i < ctrVars.listaVariaveis.Count; i++)
            {
                listaVars.Add(ctrVars.listaVariaveis[i].nome);
            }

            este = (AcaoLogica)target;

            conditions = este.conditionsLista;

            listDrawerTrue = ScriptableObject.CreateInstance<ListaAcoesEditor>();
            listDrawerFalse = ScriptableObject.CreateInstance<ListaAcoesEditor>();

            listDrawerTrue.Inicializar(este.acoesTrue);
            listDrawerFalse.Inicializar(este.acoesFalse);


        }
        catch (System.Exception c)
        {
            Debug.LogError(c.Message);

            if (target == null)
            {
                DestroyImmediate(this);
            }
        }
    }

    void AtualizarValor(int index, int i)
    {
        conditions[index].valorComparar = ctrVars.listaVariaveis[i].valor;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Cria uma condicao logica",MessageType.Info);

        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Criar condiçao"))
        {
            conditions.Add(new Condicao());
            este.conditionsLista = conditions;
        }

        for (int k = 0; k < conditions.Count; k++)
        {

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("X"))
            {
                conditions.RemoveAt(k);
                OnInspectorGUI();
                return;
            }

            conditions[k].tipo = (int)(AcaoLogica.TipoSe)EditorGUILayout.EnumPopup((AcaoLogica.TipoSe)conditions[k].tipo);

            //var conditional
            if (conditions[k].tipo == 0)
            {
                if (listaVars.Count == 0)
                {
                    EditorGUILayout.HelpBox("ERRO: Não existem variaveis!",MessageType.Error);

                }
                else
                {

                    EditorGUI.BeginChangeCheck();
                    conditions[k].variavelID = EditorGUILayout.Popup(conditions[k].variavelID, listaVars.ToArray());
                    if (EditorGUI.EndChangeCheck())
                    {
                        AtualizarValor(k, conditions[k].variavelID);
                        GUILayout.EndHorizontal();
                        return;
                    }
                    conditions[k].comparacao = (int)(AcaoLogica.VariavelComparar)EditorGUILayout.EnumPopup((AcaoLogica.VariavelComparar)conditions[k].comparacao);

                    try
                    {
                        if (ctrVars.listaVariaveis[conditions[k].variavelID].tipoVariavel == TipoVariavel.BOOLEANO)
                        {
                            conditions[k].valorComparar = EditorGUILayout.Toggle(bool.Parse(conditions[k].valorComparar)).ToString();
                        }
                        else if (ctrVars.listaVariaveis[conditions[k].variavelID].tipoVariavel == TipoVariavel.NUMERICO)
                        {
                            conditions[k].valorComparar = EditorGUILayout.IntField(int.Parse(conditions[k].valorComparar)).ToString();
                        }
                        else
                        {
                            conditions[k].valorComparar = EditorGUILayout.TextField(conditions[k].valorComparar);
                        }
                    }
                    catch
                    {
                        conditions[k].valorComparar = ctrVars.listaVariaveis[conditions[k].variavelID].valor;
                    }

                }
            }
            else if (conditions[k].tipo == 1)
            {
                //objeto conditional
                //EditorGUILayout.PropertyField(objeto);

                conditions[k].objeto = (GameObject)EditorGUILayout.ObjectField(conditions[k].objeto, typeof(GameObject), true);
                conditions[k].comparacao = (int)(AcaoLogica.ObjetoOpcoes)EditorGUILayout.EnumPopup((AcaoLogica.ObjetoOpcoes)conditions[k].comparacao);

            }
            else if (conditions[k].tipo == 2)
            {
                //componente conditional
                //conditions[k].componente = (Behaviour)EditorGUILayout.ObjectField(conditions[k].componente, typeof(Behaviour), true);
                conditions[k].comparacao = (int)(AcaoLogica.Componente_Opcoes)EditorGUILayout.EnumPopup((AcaoLogica.Componente_Opcoes)conditions[k].comparacao);
            }
            GUILayout.EndHorizontal();
        }

        //açoes para true ou false

        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(10f);
        EditorGUILayout.Separator();
        EditorGUILayout.HelpBox("SE FOR VERDADEIRO:", MessageType.Warning);
        EditorGUI.indentLevel++;
        GUILayout.BeginVertical();
        for (int i = 0; i < este.acoesTrue.Count; i++)
        {
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            AcaoAtributo x = (AcaoAtributo)este.acoesTrue[i].GetType().GetCustomAttributes(false)[0];
            EditorGUILayout.LabelField("Ação " + i, x.nomeBonito);

            GUILayout.Space(10f);


            try
            {
                listDrawerTrue.editoresAcoes[i].OnInspectorGUI();
            }
            catch (Exception c)
            {
                EditorGUILayout.HelpBox("Erro ao apresentar açao: " + listDrawerTrue.editoresAcoes[i].name + " - " + c.Message, MessageType.Error);
            }


            GUILayout.Space(5f);

            if (GUILayout.Button("Apagar", GUILayout.Width(150f)))
            {
                ApagarAcao(true, i);
            }
            GUILayout.EndVertical();
        }

        GUILayout.Space(15f);
        GUILayout.Label("Nova Ação:");

        foreach (Type tipo in listDrawerTrue.tiposAcoes)
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

            listDrawerTrue.opcoesFrames.Add(nomeFinal);
        }


        EditorGUI.BeginChangeCheck();
        sel = EditorGUILayout.Popup(sel, listDrawerTrue.opcoesFrames.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            if (sel != 0)
            {
                NovaAcao(true, sel);
                sel = 0;
            }
        }

        GUILayout.EndVertical();
        EditorGUI.indentLevel--;
        GUILayout.Space(10f);
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUI.indentLevel++;
        EditorGUILayout.HelpBox("SE FOR FALSO:", MessageType.Warning);

        GUILayout.BeginVertical();
        for (int i = 0; i < este.acoesFalse.Count; i++)
        {
            GUILayout.BeginVertical();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            AcaoAtributo x = (AcaoAtributo)este.acoesFalse[i].GetType().GetCustomAttributes(false)[0];
            EditorGUILayout.LabelField("Ação " + i, x.nomeBonito);

            GUILayout.Space(10f);


            try
            {
                listDrawerFalse.editoresAcoes[i].OnInspectorGUI();
            }
            catch (Exception c)
            {
                EditorGUILayout.HelpBox("Erro ao apresentar açao: " + listDrawerFalse.editoresAcoes[i].name + " - " + c.Message, MessageType.Error);
            }


            GUILayout.Space(5f);

            if (GUILayout.Button("Apagar", GUILayout.Width(150f)))
            {
                ApagarAcao(false, i);
            }
            GUILayout.EndVertical();
        }

        GUILayout.Space(15f);
        GUILayout.Label("Nova Ação:");

        foreach (Type tipo in listDrawerFalse.tiposAcoes)
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

            listDrawerFalse.opcoesFrames.Add(nomeFinal);
        }


        EditorGUI.BeginChangeCheck();
        sel = EditorGUILayout.Popup(sel, listDrawerFalse.opcoesFrames.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            if (sel != 0)
            {
                NovaAcao(false, sel);
                sel = 0;
            }
        }

        GUILayout.EndVertical();
        EditorGUI.indentLevel--;

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            EditorUtility.SetDirty(este);
        }
    }

    public void NovaAcao(bool t, int i)
    {
        JogoAcao tc = null;

        if (t)
        {
            tc = (JogoAcao)este.gameObject.AddComponent(listDrawerTrue.tiposAcoes[i]);
        }
        else
        {
            tc = (JogoAcao)este.gameObject.AddComponent(listDrawerFalse.tiposAcoes[i]);
        }

        tc.hideFlags = HideFlags.HideInInspector;

        if (t)
        {
            este.acoesTrue.Add(tc);
            listDrawerTrue.CriarEditores(este.acoesTrue);
        }
        else
        {
            este.acoesFalse.Add(tc);
            listDrawerFalse.CriarEditores(este.acoesFalse);
        }
    }

    public void ApagarAcao(bool t, int x)
    {
        if (t)
        {
            listDrawerTrue.ApagarEditor(x);
            DestroyImmediate(este.acoesTrue[x]);
            este.acoesTrue.RemoveAt(x);
        }
        else
        {
            listDrawerFalse.ApagarEditor(x);
            DestroyImmediate(este.acoesFalse[x]);
            este.acoesFalse.RemoveAt(x);
        }
    }
}