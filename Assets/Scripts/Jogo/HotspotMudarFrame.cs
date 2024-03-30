using System.Collections;
using UnityEngine;

public class HotspotMudarFrame : Hotspot
{
    public CuboFrame destino;

    private void OnDrawGizmos()
    {
        if (destino != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,destino.transform.position);
        }
    }

    protected override void ExecutarMouseDown()
    {
        if (!aExecutar && ativado)
        {
            StartCoroutine(FazerAcoes());
        }
    }

    private IEnumerator FazerAcoes()
    {
        aExecutar = true;
        if (controladorAcoes != null)
        {
            controladorAcoes.Executar();

            yield return new WaitUntil(() => !controladorAcoes.emCurso);

            if (controladorAcoes.sucesso)
            {
                origem.framesControlador.IrPara(destino, false);
            }
        }
        else
        {
            origem.framesControlador.IrPara(destino, false);
        }
        aExecutar = false;
        yield return null;
    }

    public void SetDestino(CuboFrame dest)
    {
        destino = dest;
    }

    public void Posicionar()
    {
        transform.localPosition = Vector3.zero;

        transform.LookAt(destino.transform, Vector3.up);
        transform.Translate(transform.forward * 3f, Space.World);
    }
}
