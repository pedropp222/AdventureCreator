using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Outros/Esperar")]
public class AcaoEsperar : JogoAcao
{
    public float tempo;

    public override void Avaliar()
    {
        StartCoroutine(Espera());
    }

    private IEnumerator Espera()
    {
        yield return new WaitForSeconds(tempo);

        sucesso = true;
        terminou = true;
    }
}
