using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ControladorVariaveis : MonoBehaviour
{
    public List<Variavel> listaVariaveis = new List<Variavel>();
}

[System.Serializable]
public class Variavel
{
    public string nome;
    public string valor;
    public TipoVariavel tipoVariavel;
}

public enum TipoVariavel
{
    NUMERICO,
    BOOLEANO,
    TEXTO
}