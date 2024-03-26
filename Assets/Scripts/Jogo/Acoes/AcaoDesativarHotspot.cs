using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Hotspot/Desativar Hotspot")]
public class AcaoDesativarHotspot : JogoAcao
{
    public Hotspot hotspot;

    public override void Avaliar()
    {

        hotspot.SetAtivado(false);
        sucesso = true;
        terminou = true;
    }
}
