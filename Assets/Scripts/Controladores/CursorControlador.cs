using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControlador : MonoBehaviour
{
    [SerializeField] private Cursor cursorNormal;
    [SerializeField] private Cursor cursorInteragir;

    [SerializeField] private Text textoCursorCanvas;
    [SerializeField] private Image imagemCursorCanvas;

    [SerializeField] private TipoCursor tipoCursor;

    [SerializeField] public List<Cursor> cursoresExtra = new List<Cursor>();

    public static CursorControlador instancia;

    private void Awake()
    {
        instancia = this;
        SetTipoCursor(tipoCursor);
        CursorNormal();
    }

    public void CursorInteragir()
    {
        ApresentarCursor(cursorInteragir);
    }

    public void CursorInteragirCustom(int id)
    {
        if (id < 0 || id >= cursoresExtra.Count)
        {
            Debug.LogError("ERRO: Tentou colocar um cursor que nao existe: " + id);
        }
        else
        {
            ApresentarCursor(cursoresExtra[id]);
        }
    }

    public void CursorNormal()
    {
        ApresentarCursor(cursorNormal);
    }

    public void SetTipoCursor(TipoCursor tipo)
    {
        tipoCursor = tipo;

        if (tipoCursor == TipoCursor.TEXTO)
        {
            imagemCursorCanvas.enabled = false;
            textoCursorCanvas.enabled = true;
        }
        else
        {
            textoCursorCanvas.enabled = false;
            imagemCursorCanvas.enabled = true;
        }
    }

    private void ApresentarCursor(Cursor cursor)
    {
        if (tipoCursor == TipoCursor.TEXTO)
        {
            textoCursorCanvas.text = cursor.valorTexto;
        }
        else
        {
            imagemCursorCanvas.sprite = cursor.valorImagem;
        }
    }

    public enum TipoCursor
    {
        TEXTO,
        IMAGENS
    }
}

[System.Serializable]
public class Cursor
{
    public string nome;
    public string valorTexto;
    public Sprite valorImagem;
}