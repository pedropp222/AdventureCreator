using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Logica/Avaliar")]
public class AcaoLogica : JogoAcao
{
    [SerializeField]
    public List<Condicao> conditionsLista = new List<Condicao>();

    public List<JogoAcao> acoesTrue = new List<JogoAcao>();
    public List<JogoAcao> acoesFalse = new List<JogoAcao>();

    public ControladorVariaveis ctr;
    private bool emCurso = false;

    public enum TipoSe
    {
        VARIAVEL,
        OBJETO,
        COMPONENTE
    }

    public enum VariavelComparar
    {
        IGUAL_A,
        NAO_IGUAL,
        MAIOR,
        MENOR,
        MAIOR_OU_IGUAL,
        MENOR_OU_IGUAL
    }

    public enum ObjetoOpcoes
    {
        EXISTE,
        NAO_EXISTE
    }

    public enum Componente_Opcoes
    {
        ATIVADO,
        DESATIVADO
    }

    private void Start()
    {
        ctr = FindObjectOfType<ControladorVariaveis>();
    }

    public override void Avaliar()
    {
        foreach (Condicao t in conditionsLista)
        {
            if (t.tipo == (int)TipoSe.VARIAVEL)
            {
                if (t.comparacao == (int)VariavelComparar.IGUAL_A)
                {
                    if (ctr.listaVariaveis[t.variavelID].valor == t.valorComparar)
                    {
                        sucesso = true;
                        continue;
                    }
                    else
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (t.comparacao == (int)VariavelComparar.NAO_IGUAL)
                {
                    if (ctr.listaVariaveis[t.variavelID].valor == t.valorComparar)
                    {
                        sucesso = false;
                        break;
                    }
                    else
                    {
                        sucesso = true;
                        continue;
                    }
                }

                if (ctr.listaVariaveis[t.variavelID].tipoVariavel == TipoVariavel.NUMERICO)
                {

                    if (t.comparacao == (int)VariavelComparar.MAIOR)
                    {
                        if (int.Parse(t.valorComparar) > int.Parse(ctr.listaVariaveis[t.variavelID].valor))
                        {
                            sucesso = true;
                            continue;
                        }
                        else
                        {
                            sucesso = false;
                            break;
                        }
                    }

                    if (t.comparacao == (int)VariavelComparar.MAIOR_OU_IGUAL)
                    {
                        if (int.Parse(t.valorComparar) >= int.Parse(ctr.listaVariaveis[t.variavelID].valor))
                        {
                            sucesso = true;
                            continue;
                        }
                        else
                        {
                            sucesso = false;
                            break;
                        }
                    }

                    if (t.comparacao == (int)VariavelComparar.MENOR)
                    {
                        if (int.Parse(t.valorComparar) < int.Parse(ctr.listaVariaveis[t.variavelID].valor))
                        {
                            sucesso = true;
                            continue;
                        }
                        else
                        {
                            sucesso = false;
                            break;
                        }
                    }

                    if (t.comparacao == (int)VariavelComparar.MENOR_OU_IGUAL)
                    {
                        if (int.Parse(t.valorComparar) <= int.Parse(ctr.listaVariaveis[t.variavelID].valor))
                        {
                            sucesso = true;
                            continue;
                        }
                        else
                        {
                            sucesso = false;
                            break;
                        }
                    }
                }
            }
            else if (t.tipo == (int)TipoSe.OBJETO)
            {
                if (t.comparacao == (int)ObjetoOpcoes.EXISTE)
                {
                    if (t.objeto.activeSelf)
                    {
                        sucesso = true;
                        continue;
                    }
                    else
                    {
                        sucesso = false;
                        break;
                    }
                }
                else if (t.comparacao == (int)ObjetoOpcoes.NAO_EXISTE)
                {
                    if (!t.objeto.activeSelf)
                    {
                        sucesso = true;
                        continue;
                    }
                    else
                    {
                        sucesso = false;
                        break;
                    }
                }
            }
            else if (t.tipo == (int)TipoSe.COMPONENTE)
            {
                if (t.comparacao == (int)Componente_Opcoes.ATIVADO)
                {
                    if (t.componente.enabled)
                    {
                        sucesso = true;
                        continue;
                    }
                    else
                    {
                        sucesso = false;
                        break;
                    }
                }
                else if (t.comparacao == (int)Componente_Opcoes.DESATIVADO)
                {
                    if (!t.componente.enabled)
                    {
                        sucesso = true;
                        continue;
                    }
                    else
                    {
                        sucesso = false;
                        break;
                    }
                }
            }
        }

        //todas as condiçoes avaliadas, iniciar açoes

        if (!emCurso)
        {
            StartCoroutine(ExecutarAcoes());
        }
    }

    IEnumerator ExecutarAcoes()
    {
        emCurso = true;

        List<JogoAcao> listaAcoes;

        if (sucesso)
        {
            listaAcoes = acoesTrue;
        }
        else
        {
            listaAcoes = acoesFalse;
        }

        foreach (JogoAcao acao in listaAcoes)
        {
            acao.Avaliar();

            yield return new WaitUntil(() => acao.terminou);

            if (!acao.sucesso)
            {
                break;
            }
        }

        //resetar acoes

        foreach (JogoAcao acao in listaAcoes)
        {
            acao.terminou = false;
            acao.sucesso = false;
        }

        emCurso = false;

        //tudo feito
        terminou = true;
        sucesso = true;

        yield return null;
    }
}

[System.Serializable]
public class Condicao
{
    public int tipo;
    public int comparacao;

    public int variavelID;

    public string valorComparar;

    public GameObject objeto;

    public MonoBehaviour componente;
}

