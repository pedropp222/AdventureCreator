using System.Collections;
using UnityEngine;

[AcaoAtributo("Frames/Ir Para Outra Frame")]
public class AcaoIrFrame : JogoAcao
{
    public CuboFrame frameDestino;
    public float tempoEspera;

    public override void Avaliar()
    {
        if (tempoEspera <= 0f)
        {
            AdventureControlador.instancia.framesJogoControlador.IrPara(frameDestino, false);
            sucesso = true;
            terminou = true;
        }
        else
        {
            StartCoroutine(EsperarFrame());
        }
    }

    private IEnumerator EsperarFrame()
    {
        yield return new WaitForSeconds(tempoEspera);

        AdventureControlador.instancia.framesJogoControlador.IrPara(frameDestino, false);
        sucesso = true;
        terminou = true;

        yield return null;
    }
}