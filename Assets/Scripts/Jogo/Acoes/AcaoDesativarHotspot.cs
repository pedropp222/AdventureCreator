using UnityEngine;

[AcaoAtributo("Hotspot/Desativar Hotspot")]
public class AcaoDesativarHotspot : JogoAcao
{
    public Hotspot hotspot;

    public bool todos;
    public bool este;

    public override void Avaliar()
    {
        if (este)
        {
            if (GetComponent<Hotspot>() != null)
            {
                GetComponent<Hotspot>().SetAtivado(false);
            }
            else
            {
                Debug.LogWarning("A acao DesativarHotspot com opcao 'este' nao pode ser usada numa frame, mas sim num Hotspot");
            }
        }
        else if (todos)
        {
            //Mesma logica esta a ser aplicada no AcaoAtivarHotspot
            if (GetComponent<Hotspot>() != null)
            {
                GetComponent<Hotspot>().origem.listaHotspots.ForEach(i => { i.SetAtivado(false); });
            }
            else
            {
                GetComponent<CuboFrame>().listaHotspots.ForEach(i => { i.SetAtivado(false); });
            }
        }
        else
        {
            hotspot.SetAtivado(false);
        }
        sucesso = true;
        terminou = true;
    }
}