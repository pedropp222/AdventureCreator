using System;
using UnityEngine;

public class JogoAcao : MonoBehaviour
{
    [HideInInspector]
    public bool terminou;
    [HideInInspector]
    public bool sucesso;

    public virtual void Avaliar()
    {
        sucesso = true;
        terminou = true;
    }
}

public class AcaoAtributo : Attribute
{
    public string nomeBonito;
    public AcaoAtributo(string n)
    {
        nomeBonito = n;
    }
}
