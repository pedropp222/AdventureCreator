using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAcoes : MonoBehaviour
{
    public List<JogoAcao> listaAcoes = new List<JogoAcao>();

    public bool emCurso { get; private set; }
    public bool sucesso { get; private set; }
    public AcaoTipo tipoAcao;

    public void Executar()
    {
        if(!emCurso)
        {
            StartCoroutine(ExecutarAcoes());
        }
        else
        {
            Debug.Log("Acoes estao a executar...");
        }
    }

    IEnumerator ExecutarAcoes()
    {
        emCurso = true;
        sucesso = true;

        foreach (JogoAcao acao in listaAcoes)
        {
            acao.Avaliar();

            yield return new WaitUntil(() => acao.terminou);

            if (!acao.sucesso)
            {
                sucesso = false;
                break;
            }
        }

        //reiniciar acoes

        foreach (JogoAcao acao in listaAcoes)
        {
            acao.terminou = false;
            acao.sucesso = false;
        }

        emCurso = false;
        yield return null;
    }

    public enum AcaoTipo
    {
        ENTRAR_FRAME,
        SAIR_FRAME,
        HOTSPOT_CLICK
    }

}
