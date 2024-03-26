using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

class ListaAcoesEditor : Editor
{
    public List<Type> tiposAcoes;

    public List<string> opcoesFrames;

    public List<Editor> editoresAcoes;

    public bool fatalError = false;

    public ListaAcoesEditor()
    {
        tiposAcoes = new List<Type>();
        opcoesFrames = new List<string>();
        editoresAcoes = new List<Editor>();

        //1º da lista e vazio
        tiposAcoes.Add(null);
    }

    public void Inicializar(List<JogoAcao> listaAcoes)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract) continue;
                if (!type.IsSubclassOf(typeof(JogoAcao))) continue;
                tiposAcoes.Add(type);
            }
        }

        CriarEditores(listaAcoes);
    }

    public void ResetEditor()
    {
        tiposAcoes.Clear();
        opcoesFrames.Clear();
        editoresAcoes.Clear();

        //1º da lista e vazio
        tiposAcoes.Add(null);
    }

    public void ApagarEditor(int i)
    {
        editoresAcoes[i].ResetTarget();
        editoresAcoes.RemoveAt(i);
    }

    public void CriarEditores(List<JogoAcao> listaAcoes)
    {
        if (editoresAcoes.Count != 0)
        {
            for (int i = 0; i < editoresAcoes.Count; i++)
            {
                editoresAcoes[i].ResetTarget();
            }
        }

        editoresAcoes.Clear();

        for (int i = 0; i < listaAcoes.Count; i++)
        {
            try
            {
                Editor curr = CreateEditor(listaAcoes[i]);
                curr.CreateInspectorGUI();

                editoresAcoes.Add(curr);
            }
            catch
            {
                Debug.LogError("Erro a adicionar editor " + listaAcoes[i].ToString());
                fatalError = true;
            }
        }
    }
}