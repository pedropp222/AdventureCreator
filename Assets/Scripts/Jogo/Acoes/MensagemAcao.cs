using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[AcaoAtributo("Debug/Mensagem")]
public class MensagemAcao : JogoAcao
{
    public string mensagem;

    public override void Avaliar()
    {

        Debug.Log(mensagem);

        terminou = true;
        sucesso = true;
    }
}
