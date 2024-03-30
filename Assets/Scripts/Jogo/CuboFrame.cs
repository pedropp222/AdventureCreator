using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CuboFrame : MonoBehaviour
{
    public List<Hotspot> listaHotspots = new List<Hotspot>();

    public FramesJogoControlador framesControlador;

    ControladorAcoes[] controladorAcoes;

    private void Awake()
    {
        controladorAcoes = GetComponents<ControladorAcoes>();
    }

    public void AdicionarHotspot(Hotspot hotspot)
    {
        listaHotspots.Add(hotspot);
    }

    public void RemoverHotspot(Hotspot hotspot)
    {
        if (!listaHotspots.Remove(hotspot))
        {
            Debug.LogWarning("WARN: O hotspot nao existia na lista");
        }
    }

    public Hotspot EncontrarHotspotDestino(CuboFrame destino)
    {
        return listaHotspots.Find((x) => x is HotspotMudarFrame l && l.destino == destino);
    }

    public void OnEntrouFrame()
    {
        foreach(ControladorAcoes ctrl in controladorAcoes)
        {
            if (ctrl.tipoAcao == ControladorAcoes.AcaoTipo.ENTRAR_FRAME)
            {
                ctrl.Executar();
            }
        }
    }

    public void OnSaiuFrame()
    {
        foreach (ControladorAcoes ctrl in controladorAcoes)
        {
            if (ctrl.tipoAcao == ControladorAcoes.AcaoTipo.SAIR_FRAME)
            {
                ctrl.Executar();
            }
        }
    }
}
