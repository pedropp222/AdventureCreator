using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hotspot),true)]
public class HotspotEditor : Editor
{
    SerializedProperty ativado;
    SerializedProperty usarCursor;
    SerializedProperty cursorId;

    CursorControlador contr;
    string[] cursoresList;

    private void OnEnable()
    {
        ativado = serializedObject.FindProperty("ativado");
        usarCursor = serializedObject.FindProperty("usarCursorCustom");
        cursorId = serializedObject.FindProperty("cursorId");

        contr = FindAnyObjectByType<CursorControlador>();

        if (contr != null && contr.cursoresExtra.Count != 0)
        {
            cursoresList = contr.cursoresExtra.Select(x => x.nome+" ("+x.valorTexto+")").ToArray();
        }
        else
        {
            cursoresList = new string[0];
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(ativado,new GUIContent("Ativado: "));
        EditorGUILayout.PropertyField(usarCursor, new GUIContent("Usar Outro Cursor: "));

        if (usarCursor.boolValue)
        {
            if (cursoresList.Length > 0)
            {
                if (cursorId.intValue == -1)
                {
                    cursorId.intValue = 0;
                }

                cursorId.intValue = EditorGUILayout.Popup(cursorId.intValue, cursoresList);
            }
            else
            {
                EditorGUILayout.LabelField("Nao existem cursores. Vai ao CursorControlador para criar novos cursores.");
                cursorId.intValue = -1;
            }
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Duplicar Hotspot"))
        {
            Object bo = Instantiate(serializedObject.targetObject,((Hotspot)serializedObject.targetObject).transform.parent);

            Hotspot obj = (Hotspot)bo;

            obj.name = "Hotspot_NOVO";

            ((Hotspot)serializedObject.targetObject).transform.parent.GetComponent<CuboFrame>().AdicionarHotspot(obj);
        }
        if (GUILayout.Button("Apagar Hotspot"))
        {
            ((Hotspot)serializedObject.targetObject).transform.parent.GetComponent<CuboFrame>().RemoverHotspot((Hotspot)serializedObject.targetObject);
            DestroyImmediate(((Hotspot)serializedObject.targetObject).gameObject);
            return;
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
