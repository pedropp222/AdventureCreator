using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecionarCamaraPlataforma : MonoBehaviour
{
    void Start()
    {
        //if (AdventureControlador.instancia.GetModoAtual() == AdventureControlador.ModoAtual.JOGO)
        //{
            if (Application.isMobilePlatform)
            {
                gameObject.AddComponent<MobileFirstPersonCamera>();
            }
            else
            {
                gameObject.AddComponent<MouseLook>();

            }
        //}
    }
}
