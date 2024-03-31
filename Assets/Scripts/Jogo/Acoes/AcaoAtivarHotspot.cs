using UnityEngine;

[AcaoAtributo("Hotspot/Ativar Hotspot")]
public class AcaoAtivarHotspot : JogoAcao
{
    public Hotspot hotspot;

    public bool todos;
    public bool este;

    public override void Avaliar()
    {
        if (este)
        {
            if (GetComponent<Hotspot>()!=null)
            {
                GetComponent<Hotspot>().SetAtivado(true);
            }
            else
            {
                Debug.LogWarning("A acao AtivarHotspot com opcao 'este' nao pode ser usada numa frame, mas sim num Hotspot");
            }
        }
        else if (todos)
        {
            //mesma logica tem que ser aplicada caso a acao seja executada num hotspot ou numa frame
            if (GetComponent<Hotspot>() != null)
            {
                GetComponent<Hotspot>().origem.listaHotspots.ForEach(i => { i.SetAtivado(true); });
            }
            else
            {
                GetComponent<CuboFrame>().listaHotspots.ForEach(i => { i.SetAtivado(true); });
            }
        }
        else
        {
            hotspot.SetAtivado(true);
        }
        sucesso = true;
        terminou = true;
    }
}
