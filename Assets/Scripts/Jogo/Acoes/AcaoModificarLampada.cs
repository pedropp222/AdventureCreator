using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Basico/Modificar Lampada")]
public class AcaoModificarLampada : JogoAcao
{
    public Light luz;
    public bool ligado;

    public override void Avaliar()
    {
        luz.enabled = ligado;

        sucesso = true;
        terminou = true;
    }
}
