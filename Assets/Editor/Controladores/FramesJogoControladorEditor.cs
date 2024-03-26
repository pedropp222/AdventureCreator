using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FramesJogoControlador))]
public class FramesJogoControladorEditor : Editor
{
    private FramesJogoControlador este;

    bool instantiate = false;

    RaycastHit hit;

    private static float alturaFrame;

    private void OnEnable()
    {
        este = (FramesJogoControlador)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space(5f);

        GUILayout.Label("Prime 'C' e um frame sera criado no mundo");

        GUILayout.BeginHorizontal();

        GUILayout.Label("Altura da frame: ");

        alturaFrame = EditorGUILayout.FloatField(alturaFrame);


        GUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        Vector2 mousePos = Event.current.mousePosition;

        if (!Event.current.isMouse && !Event.current.isScrollWheel)
        {
            if (Event.current.keyCode == KeyCode.C && Event.current.type == EventType.KeyDown)
            {
                if (instantiate)
                {
                    instantiate = !instantiate;
                    if (Physics.Raycast(DarRay(mousePos), out hit))
                    {
                        GameObject go = new GameObject("FRAME_" + (este.listaCubos.Count + 1));

                        go.transform.SetParent(este.transform);

                        go.transform.position = hit.point + new Vector3(0f, alturaFrame, 0f);

                        IconManager.SetIcon(go.gameObject, LabelIcon.Red);

                        este.listaCubos.Add(go.AddComponent<CuboFrame>());

                        go.GetComponent<CuboFrame>().framesControlador = este;
                    }
                }
            }
            else if (Event.current.keyCode == KeyCode.C && Event.current.type == EventType.KeyUp)
            {
                instantiate = true;
            }
        }
    }

    Ray DarRay(Vector2 pos)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(pos);
        return ray;
    }
}

