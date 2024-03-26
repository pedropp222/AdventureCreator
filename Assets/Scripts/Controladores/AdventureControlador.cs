using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureControlador : MonoBehaviour
{
    [HideInInspector]
    public ModoAtual modoAtual;

    [HideInInspector]
    public bool setupFeito = false;

    public SonsControlador sonsControlador;
    public FramesJogoControlador framesJogoControlador;
    public CursorControlador cursorControlador;

    public static AdventureControlador instancia;

    private void Awake()
    {
        instancia = this;
    }

    public ModoAtual GetModoAtual()
    {
        return modoAtual;
    }

    /*[MenuItem("AdventureCreator/Modo Cenario")]
    private static void MostrarCenario()
    {
        FindAnyObjectByType<FramesJogoControlador>(FindObjectsInactive.Include).gameObject.SetActive(false);
        FindAnyObjectByType<TiradorFotosPanoramicas>(FindObjectsInactive.Include).gameObject.SetActive(true);
        SetTag("Cenario",true);

        if (instancia == null)
        {
            instancia = FindAnyObjectByType<AdventureControlador>();
        }

        instancia.modoAtual = ModoAtual.CENARIO;
        Debug.Log("Modo Atual: " + instancia.modoAtual);
    }*/

    /*[MenuItem("AdventureCreator/Modo Jogo")]
    private static void EsconderCenario()
    {
        FindAnyObjectByType<FramesJogoControlador>(FindObjectsInactive.Include).gameObject.SetActive(true);
        FindAnyObjectByType<TiradorFotosPanoramicas>(FindObjectsInactive.Include).gameObject.SetActive(false);
        SetTag("Cenario", false);

        if (instancia == null)
        {
            instancia = FindAnyObjectByType<AdventureControlador>();
        }

        instancia.modoAtual = ModoAtual.JOGO;
        Debug.Log("Modo Atual: " + instancia.modoAtual);

    }*/  

    private static void SetTag(string tag, bool estado)
    {
        foreach(GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.CompareTag(tag))
            {
                go.SetActive(estado);
            }
        }
    }

    public enum ModoAtual
    {
        NONE,
        CENARIO,
        JOGO
    }

}
