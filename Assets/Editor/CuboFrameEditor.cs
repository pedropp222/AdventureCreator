using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CuboFrame))]
public class CuboFrameEditor : Editor
{
    private CuboFrame este;

    private EstadoAtual estadoAtual;

    private GameObject hotspotEdit;
    private Hotspot hotspot;

    private FramesJogoControlador frameContr;

    private TipoHotspot tipoHotspot;

    private string[] destinoStrs;

    int destinoSel;
    int ultimoSel;

    float escalaX = 0f;
    float escalaY = 0f;

    float posX = 0f;
    float posY = 0f;

    Vector3 posNormal;

    private void OnEnable()
    {
        este = (CuboFrame)target;

        hotspotEdit = null;
        hotspot = null;
        estadoAtual = EstadoAtual.NENHUM;
        destinoSel = 0;
        ultimoSel = -1;

        if (frameContr == null)
        {
            frameContr = FindAnyObjectByType<FramesJogoControlador>();
        }

        destinoStrs = frameContr.listaCubos.Select(e => e.name).ToArray();
    }

    private void OnDisable()
    {
        if (estadoAtual == EstadoAtual.CRIAR_HOTSPOT)
        {
            este.AdicionarHotspot(hotspot);
            hotspot = null;
            hotspotEdit = null;
            estadoAtual = EstadoAtual.NENHUM;
        }
    }

    private void MudarTipoHotspot()
    {
        if (hotspotEdit != null)
        {
            escalaX = 1f;
            escalaY = 1f;

            posX = 0f;
            posY = 0f;

            if (hotspotEdit.GetComponent<Hotspot>()!=null)
            {
                DestroyImmediate(hotspotEdit.GetComponent<Hotspot>());
            }

            if (tipoHotspot == TipoHotspot.IR_OUTRO_FRAME)
            {
                hotspot = hotspotEdit.AddComponent<HotspotMudarFrame>();
                RefreshHotspotDestino();
            }
            else if (tipoHotspot == TipoHotspot.NORMAL)
            {
                hotspot = hotspotEdit.AddComponent<Hotspot>();
            }

            RefreshHotspotNome();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Centrar camara neste frame"))
        {
            var view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                view.AlignViewToObject(este.transform);
            }
        }

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (estadoAtual == EstadoAtual.NENHUM)
        {
            if (GUILayout.Button("Criar Hotspot"))
            {
                hotspotEdit = Instantiate(frameContr.hotspotPrefab, este.transform);
                hotspotEdit.transform.localPosition = Vector3.zero;
                //hotspot = hotspotEdit.GetComponent<HotspotMudarFrame>();
                escalaX = 1f;
                escalaY = 1f;

                posX = 0f;
                posY = 0f;

                estadoAtual = EstadoAtual.CRIAR_HOTSPOT;
                tipoHotspot = TipoHotspot.IR_OUTRO_FRAME;
                hotspot = hotspotEdit.AddComponent<HotspotMudarFrame>();
                RefreshHotspotNome();
            }

            GUILayout.Space(5f);

            if (GUILayout.Button("Apagar Todos Hotspots"))
            {
                este.listaHotspots.ForEach(x => DestroyImmediate(x.gameObject));
                este.listaHotspots.Clear();
            }


        }
        else if (estadoAtual == EstadoAtual.CRIAR_HOTSPOT)
        {

            GUILayout.Label("A editar novo hotspot");

            GUILayout.Space(10f);

            EditorGUI.BeginChangeCheck();
            tipoHotspot = (TipoHotspot)EditorGUILayout.EnumPopup("Tipo de Hotspot: ",tipoHotspot);
            if (EditorGUI.EndChangeCheck())
            {
                MudarTipoHotspot();
            }


            if (tipoHotspot == TipoHotspot.IR_OUTRO_FRAME)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Destino: ");

                EditorGUI.BeginChangeCheck();

                destinoSel = EditorGUILayout.Popup(destinoSel, destinoStrs);

                if (ultimoSel != destinoSel)
                {
                    ultimoSel = destinoSel;

                    RefreshHotspotDestino();
                    RefreshHotspotNome();
                }

                GUILayout.EndHorizontal();

                GUILayout.Label("Escala X");
                escalaX = EditorGUILayout.Slider(escalaX, 0.1f, 2f);
                GUILayout.Label("Escala Y");
                escalaY = EditorGUILayout.Slider(escalaY, 0.1f, 2f);

                GUILayout.Label("Mover X");
                posX = EditorGUILayout.Slider(posX, -2f, 2f);
                GUILayout.Label("Mover Y");
                posY = EditorGUILayout.Slider(posY, -2f, 2f);


                if (EditorGUI.EndChangeCheck())
                {
                    hotspotEdit.transform.localScale = new Vector3(escalaX, escalaY, 0.05f);
                    hotspotEdit.transform.localPosition = posNormal + hotspot.transform.right * posX + hotspot.transform.up * posY;
                }
            }

            if (GUILayout.Button("Aplicar"))
            {
                este.AdicionarHotspot(hotspot);
                hotspot = null;
                hotspotEdit = null;
                estadoAtual = EstadoAtual.NENHUM;
                tipoHotspot = TipoHotspot.IR_OUTRO_FRAME;
            }
            else if (GUILayout.Button("Cancelar"))
            {
                DestroyImmediate(hotspotEdit);
                hotspot = null;
                hotspotEdit = null;
                estadoAtual = EstadoAtual.NENHUM;
                tipoHotspot = TipoHotspot.IR_OUTRO_FRAME;
            }
        }
    }

    private void RefreshHotspotDestino()
    {
        ((HotspotMudarFrame)hotspot).SetDestino(frameContr.listaCubos[destinoSel]);

        ((HotspotMudarFrame)hotspot).Posicionar();

        posNormal = hotspotEdit.transform.localPosition;
    }

    void RefreshHotspotNome()
    {
        if (hotspotEdit != null)
        {
            if (tipoHotspot == TipoHotspot.NORMAL)
            {
                hotspotEdit.name = "Hotspot_Normal";
            }
            else
            {
                hotspotEdit.name = "Hotspot_IR_FRAME_" + ((HotspotMudarFrame)hotspot).destino?.name;
            }
        }

    }

    enum EstadoAtual
    {
        NENHUM,
        CRIAR_HOTSPOT
    }

    enum TipoHotspot
    {
        IR_OUTRO_FRAME,
        NORMAL
    }


}
