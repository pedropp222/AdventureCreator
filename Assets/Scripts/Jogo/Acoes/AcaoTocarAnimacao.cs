using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[AcaoAtributo("Animacao/Tocar Animacao")]
public class AcaoTocarAnimacao : JogoAcao
{
    public Animation animacaoComponente;
    public string nomeAnimacao;

    public bool esperarQueAnimacaoTermine;
    public bool loop;

    public override void Avaliar()
    {
        if (!esperarQueAnimacaoTermine)
        {
            animacaoComponente.Stop();
            animacaoComponente.wrapMode = loop ? WrapMode.Loop : WrapMode.Once;
            animacaoComponente.Play(nomeAnimacao);
            sucesso = true;
            terminou = true;
        }
        else
        {
            StartCoroutine(AnimacaoCoroutine());
        }
    }

    private IEnumerator AnimacaoCoroutine()
    {
        animacaoComponente.Stop();
        animacaoComponente.Play(nomeAnimacao);

        while(animacaoComponente.IsPlaying(nomeAnimacao))
        {
            yield return new WaitForEndOfFrame();
        }

        sucesso = true;
        terminou = true;
    }
}
