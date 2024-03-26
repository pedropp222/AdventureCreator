using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Hotspot/Ativar Hotspot")]
public class AcaoAtivarHotspot : JogoAcao
{
    public Hotspot hotspot;

    public override void Avaliar()
    {

        hotspot.SetAtivado(true);
        sucesso = true;
        terminou = true;
    }
}
