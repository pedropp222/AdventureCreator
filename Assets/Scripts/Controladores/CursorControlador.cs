using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControlador : MonoBehaviour
{
    [SerializeField] private string cursorNormal;
    [SerializeField] private string cursorInteragir;

    [SerializeField] private Text textoCursor;

    public static CursorControlador instancia;

    private void Awake()
    {
        instancia = this;
        CursorNormal();
    }

    public void CursorInteragir()
    {
        textoCursor.text = cursorInteragir;
    }

    public void CursorInteragirCustom(string valor)
    {
        textoCursor.text = valor;
    }

    public void CursorNormal()
    {
        textoCursor.text = cursorNormal;
    }
}
