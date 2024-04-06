using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AcaoAtributo("Variavel/Alterar o Valor de Uma Variavel")]
public class AcaoAlterarVariavel : JogoAcao
{
    public int variavelId;
    public string valorAlterar;
    public TipoOperacao operacao;

    private ControladorVariaveis contr;

    public override void Avaliar()
    {
        contr = AdventureControlador.instancia.controladorVariaveis;

        if (contr.listaVariaveis.Count == 0)
        {
            Debug.LogError("Erro, tentou avaliar variaveis mas nao existem variaveis!");
            sucesso = false;
            terminou = true;
            return;
        }

        Variavel var = contr.listaVariaveis[variavelId];

        string msg = "Variavel '" + var.nome + "' alterada: " + var.valor + " -> ";

        if (operacao == TipoOperacao.IGUAL_A)
        {
            var.valor = valorAlterar;
        }
        else if (operacao == TipoOperacao.SOMAR)
        {
            int val = int.Parse(var.valor) + int.Parse(valorAlterar);

            var.valor = val.ToString();          
        }

        msg += var.valor;

        Debug.Log(msg);

        sucesso = true;
        terminou = true;
    }

    public enum TipoOperacao
    {
        IGUAL_A,
        SOMAR,
        SUBTRAIR
    }

}
