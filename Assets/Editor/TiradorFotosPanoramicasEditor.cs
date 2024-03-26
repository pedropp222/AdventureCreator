using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(TiradorFotosPanoramicas))]
public class TiradorFotosPanoramicasEditor : Editor
{
    TiradorFotosPanoramicas este = null;

    private void OnEnable()
    {
        este = (TiradorFotosPanoramicas)target;
    }

    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying)
        {
            if (GUILayout.Button("Tirar Fotos"))
            {
                este.TirarFotos();
            }
        }
        else
        {
            GUILayout.Label("Inicia o jogo para poder tirar fotos");
            if (GUILayout.Button("CRIAR OS CUBOS FRAMES"))
            {
                este.CriarCubos();
            }
        }

        DrawDefaultInspector();
    }
}
