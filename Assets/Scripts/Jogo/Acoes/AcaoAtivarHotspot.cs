using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Hotspot/Ativar Hotspot")]
public class AcaoAtivarHotspot : JogoAcao
{
    public Hotspot hotspot;

    public bool todos;
    public bool este;

    public override void Avaliar()
    {
        if (este)
        {
            GetComponent<Hotspot>().SetAtivado(true);
        }
        else if (todos)
        {
            GetComponent<Hotspot>().origem.listaHotspots.ForEach(i => { i.SetAtivado(true); });
        }
        else
        {
            hotspot.SetAtivado(true);
        }
        sucesso = true;
        terminou = true;
    }
}
