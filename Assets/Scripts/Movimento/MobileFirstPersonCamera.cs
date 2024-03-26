using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFirstPersonCamera : MonoBehaviour
{
    public float velocidadeX = 2.0f;
    public float velocidadeY = 2.0f;

    public float rotacaoLimiteX = 70.0f;

    private bool isRotating = false;

    GameObject cam;

    private void Awake()
    {
        cam = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                float xRot = touch.deltaPosition.y * velocidadeY * Time.deltaTime;
                float yRot = touch.deltaPosition.x * velocidadeX * Time.deltaTime;

                //characterRotacao *= Quaternion.Euler(0f, yRot, 0f);
                //camaraRotacao *= Quaternion.Euler(-xRot, 0f, 0f);

                cam.transform.Rotate(new Vector3(xRot, 0f, 0f));
                transform.Rotate(new Vector3(0f, -yRot, 0f));

                cam.transform.localRotation = LimitarEixoX(cam.transform.localRotation);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
    }

    private Quaternion LimitarEixoX(Quaternion r)
    {
        r.x /= r.w;
        r.y /= r.w;
        r.z /= r.w;
        r.w = 1.0f;

        float anguloX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(r.x);

        anguloX = Mathf.Clamp(anguloX, -rotacaoLimiteX, rotacaoLimiteX);

        r.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * anguloX);

        return r;
    }
}