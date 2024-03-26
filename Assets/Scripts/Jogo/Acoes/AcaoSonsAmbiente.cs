using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Sons/Sons Ambiente")]
public class AcaoSonsAmbiente : JogoAcao
{
    public AudioClip somClip;
    public float volume;

    public float fadeTempo;

    public bool desligarTodosOsOutrosSonsAmbiente;

    private SonsControlador sons;

    public override void Avaliar()
    {
        if (sons == null)
        {
            sons = AdventureControlador.instancia.sonsControlador;
        }

        if (desligarTodosOsOutrosSonsAmbiente)
        {
            sons.DesligarTudo();
        }

        sons.FadeSom(somClip, fadeTempo, volume);

        sucesso = true;
        terminou = true;
    }
}
