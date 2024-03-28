using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FramesJogoControlador : MonoBehaviour
{
    public List<CuboFrame> listaCubos = new List<CuboFrame>();

    public GameObject camaraControlador;

    public GameObject hotspotPrefab;

    private CuboFrame frameAtual;

    bool aMover = false;

    private void Start()
    {
        if (camaraControlador == null)
        {
            camaraControlador = FindAnyObjectByType<SelecionarCamaraPlataforma>().gameObject;
        }

        if (listaCubos.Count > 0)
        {
            IrPara(listaCubos[0], true);

            for (int i = 1; i < listaCubos.Count; i++)
            {
                listaCubos[i].gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Nao existem frames");
        }
    }

    public void IrPara(CuboFrame destino, bool instantaneo)
    {
        if (!aMover)
        {
            StartCoroutine(MoverCamara(destino, instantaneo));
        }
    }

    public bool ExisteCubo(string name)
    {
        return listaCubos.Count != 0 && listaCubos.Any((x) => x.name == name);
    }

    public void AdicionarCubo(CuboFrame cubo)
    {
        listaCubos.Add(cubo);
        cubo.framesControlador = this;
    }

    IEnumerator MoverCamara(CuboFrame destino, bool instantaneo)
    {
        aMover = true;
        destino.gameObject.SetActive(true);
        Vector3 posInicial = camaraControlador.transform.position;

        if (frameAtual != null)
        {
            frameAtual.OnSaiuFrame();
        }

        if (!instantaneo)
        {
            float t = 0f;

            bool final = false;


            float distancia = Vector3.Distance(camaraControlador.transform.position, destino.transform.position);
            float distAtual;

            while (t < 1f)
            {
                Vector3 vec = Vector3.Lerp(posInicial, destino.transform.position, t);

                distAtual = Vector3.Distance(posInicial, vec);

                camaraControlador.transform.position = vec;

                t += 0.1f * Time.fixedDeltaTime;

                if (distAtual > 0.5f && final == false)
                {
                    final = true;
                    t = (distancia - 0.5f) / distancia;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        camaraControlador.transform.position = destino.transform.position;

        if (frameAtual != null)
        {
            frameAtual.gameObject.SetActive(false);
        }

        frameAtual = destino;
        aMover = false;
        frameAtual.OnEntrouFrame();
    }
}
