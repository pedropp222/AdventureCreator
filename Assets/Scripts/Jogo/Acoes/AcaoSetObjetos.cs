using UnityEngine;

[AcaoAtributo("Basicos/Ativar ou Desativar Objetos")]
public class AcaoSetObjetos : JogoAcao
{
    public Objetos[] listaObjetos;

    public override void Avaliar()
    {
        foreach (Objetos x in listaObjetos)
        {
            x.objeto.SetActive(x.ativar);
        }

        sucesso = true;
        terminou = true;
    }
}

[System.Serializable]
public class Objetos
{
    public GameObject objeto;
    public bool ativar;
}