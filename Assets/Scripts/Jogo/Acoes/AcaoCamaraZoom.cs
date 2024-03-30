using System.Collections;
using UnityEngine;

[AcaoAtributo("Camara/Fazer Zoom")]
public class AcaoCamaraZoom : JogoAcao
{
    public float fovAlvo;

    private Camera camara;

    public override void Avaliar()
    {
        if (camara == null)
        {
            camara = Camera.main;
        }

        StartCoroutine(Zoom());
    }

    private IEnumerator Zoom()
    {
        float fovInicial = camara.fieldOfView;

        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime / 0.35f;

            camara.fieldOfView = Mathf.Lerp(fovInicial, fovAlvo, t * t * (3f - 2f * t));

            yield return new WaitForEndOfFrame();
        }

        camara.fieldOfView = fovAlvo;

        terminou = true;
        sucesso = true;
    }
}