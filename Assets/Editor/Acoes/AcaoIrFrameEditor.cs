using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;


[CustomEditor(typeof(AcaoIrFrame))]
public class AcaoIrFrameEditor : Editor
{
    private string[] frames;

    private FramesJogoControlador frameContr;

    SerializedProperty frameDestino;
    SerializedProperty tempoEspera;

    int frameSel;

    private void OnEnable()
    {
        if (frameContr == null)
        {
            frameContr = FindAnyObjectByType<FramesJogoControlador>();
        }

        frameSel = 0;
        frames = frameContr.listaCubos.Select(x => x.name).ToArray();

        frameDestino = serializedObject.FindProperty("frameDestino");
        tempoEspera = serializedObject.FindProperty("tempoEspera");

        if (frameDestino.objectReferenceValue != null)
        {
            frameSel = frameContr.listaCubos.FindIndex((x) => x.name == frameDestino.objectReferenceValue.name);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        frameSel = EditorGUILayout.Popup("Frame Destino: ", frameSel, frames);
        tempoEspera.floatValue = EditorGUILayout.FloatField("Tempo Espera: ",tempoEspera.floatValue); 

        if (EditorGUI.EndChangeCheck())
        {
            frameDestino.objectReferenceValue = frameContr.listaCubos[frameSel];

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}

