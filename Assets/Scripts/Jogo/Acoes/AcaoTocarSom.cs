using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Sons/Tocar um Som")]
public class AcaoTocarSom : JogoAcao
{
    public AudioClip audioClip;
    public float volume;

    public override void Avaliar()
    {
        AdventureControlador.instancia.sonsControlador.TocarSom(audioClip, volume);
        sucesso = true;
        terminou = true;
    }
}
