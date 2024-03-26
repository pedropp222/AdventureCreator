﻿using System.Collections;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    protected ControladorAcoes controladorAcoes;

    [SerializeField]
    protected bool ativado = true;

    public bool customStringMouseEnter;
    public string cursorValor;

    protected bool aExecutar;

    bool hover = false;

    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        controladorAcoes = GetComponent<ControladorAcoes>();
    }

    private void OnMouseDown()
    {
       ExecutarMouseDown();
    }

    private void OnMouseEnter()
    {
        ExecutarMouseEnter();
        hover = true;
    }

    private void OnMouseExit()
    {
        ExecutarMouseExit();
        hover = false;
    }

    public void SetAtivado(bool valor)
    {
        ativado = valor;
        if (hover)
        {
            ExecutarMouseEnter();
        }
    }

    protected virtual void ExecutarMouseDown() 
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
        }
        aExecutar = false;
        yield return null;
    }

    protected virtual void ExecutarMouseEnter() 
    {
        if (ativado)
        {
            if (customStringMouseEnter)
            {
                CursorControlador.instancia.CursorInteragirCustom(cursorValor);
            }
            else
            {
                CursorControlador.instancia.CursorInteragir();
            }
        }
    }
    protected virtual void ExecutarMouseExit() 
    {
        if (ativado)
        {
            CursorControlador.instancia.CursorNormal();
        }
    }
}

