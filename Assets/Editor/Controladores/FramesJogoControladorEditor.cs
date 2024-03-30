using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FramesJogoControlador))]
public class FramesJogoControladorEditor : Editor
{
    private FramesJogoControlador este;

    bool instantiate = false;

    RaycastHit hit;

    private static float alturaFrame;

    private bool criarHotspotsAuto = false;

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

        GUILayout.BeginHorizontal();

        GUILayout.Label("Criar Hotspots Auto: ");

        criarHotspotsAuto = EditorGUILayout.Toggle(criarHotspotsAuto);

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

                        if (criarHotspotsAuto)
                        {
                            GameObject hot = Instantiate(este.hotspotPrefab,go.transform);
                            hot.transform.localPosition = Vector3.zero;

                            HotspotMudarFrame hf = hot.AddComponent<HotspotMudarFrame>();
                            hf.origem = go.GetComponent<CuboFrame>();

                            if (este.listaCubos.Count > 1)
                            {
                                hf.SetDestino(este.listaCubos[este.listaCubos.Count-2]);
                                hf.origem = este.listaCubos[este.listaCubos.Count-1];

                                hf.Posicionar();

                                if (este.listaCubos[este.listaCubos.Count-2].EncontrarHotspotDestino(hf.origem) == null)
                                {
                                    GameObject hot2 = Instantiate(este.hotspotPrefab, este.listaCubos[este.listaCubos.Count - 2].transform);
                                    hot2.transform.localPosition = Vector3.zero;

                                    HotspotMudarFrame hf2 = hot2.AddComponent<HotspotMudarFrame>();

                                    hf2.SetDestino(este.listaCubos[este.listaCubos.Count - 1]);
                                    hf2.origem = este.listaCubos[este.listaCubos.Count - 2];

                                    hf2.Posicionar();

                                    este.listaCubos[este.listaCubos.Count - 2].AdicionarHotspot(hf2);
                                }
                            }

                            go.GetComponent<CuboFrame>().AdicionarHotspot(hf);
                        }
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

