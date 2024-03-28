using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(AdventureControlador))]
public class AdventureControladorEditor : Editor
{

    private AdventureControlador este;

    private void OnEnable()
    {
        este = (AdventureControlador)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!este.setupFeito)
        {
            EditorGUILayout.Space(10f);

            if (GUILayout.Button("Inicializar Adventure Creator"))
            {
                Setup();
            }
        }
    }

    private void Setup()
    {
        bool existeSonsContr = false;
        bool existeFramesJogoContr = false;
        bool existeCursorContr = false;
        bool existeGestorVar = false;

        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.GetComponent<FramesJogoControlador>() != null)
            {
                existeFramesJogoContr = true;
            }
            else if (go.GetComponent<SonsControlador>() != null)
            {
                existeSonsContr = true;
            }
            else if (go.GetComponent<CursorControlador>() != null)
            {
                existeCursorContr = true;
            }
            else if (go.GetComponent<ControladorVariaveis>() != null)
            {
                existeGestorVar = true;
            }

            if (existeSonsContr && existeFramesJogoContr && existeCursorContr && existeGestorVar)
            {
                break;
            }
        }

        if (!existeSonsContr)
        {
            GameObject go = new GameObject("SonsControlador");
            este.sonsControlador = go.AddComponent<SonsControlador>();
        }
        if (!existeFramesJogoContr)
        {
            GameObject go = new GameObject("FramesJogoControlador");
            este.framesJogoControlador = go.AddComponent<FramesJogoControlador>();
        }
        if (!existeCursorContr)
        {
            GameObject go = new GameObject("CursorControlador");
            este.cursorControlador = go.AddComponent<CursorControlador>();
        }
        if (!existeGestorVar)
        {
            GameObject go = new GameObject("VariaveisControlador");
            este.controladorVariaveis = go.AddComponent<ControladorVariaveis>();
        }

        este.setupFeito = true;
    }
}

