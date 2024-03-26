using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class HotspotMudarFrame : Hotspot
{
    public CuboFrame destino;
    public CuboFrame origem;

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
