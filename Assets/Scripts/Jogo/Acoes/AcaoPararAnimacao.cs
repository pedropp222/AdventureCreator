using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[AcaoAtributo("Animacao/Parar Animacao")]
public class AcaoPararAnimacao : JogoAcao
{
    public Animation animacaoObjeto;

    public ModoParagem modoParagem;

    public override void Avaliar()
    {
        if (modoParagem == ModoParagem.IMEDIATO)
        {
            animacaoObjeto.Stop();
        }
        else
        {
            animacaoObjeto.wrapMode = WrapMode.Once;
        }

        sucesso = true;
        terminou = true;
    }
}

public enum ModoParagem
{
    IMEDIATO,
    NORMAL
}


