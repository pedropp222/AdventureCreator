using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Debug/Acao Sem Sucesso")]
public class AcaoSemSucesso : JogoAcao
{
    public override void Avaliar()
    {
        terminou = true;
        sucesso = false;
    }
}
