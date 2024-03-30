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
            GetComponent<Hotspot>().SetAtivado(false);
        }
        else if (todos)
        {
            GetComponent<Hotspot>().origem.listaHotspots.ForEach(i => { i.SetAtivado(false); });
        }
        else
        {
            hotspot.SetAtivado(false);
        }
        sucesso = true;
        terminou = true;
    }
}