using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampadaPiscar : MonoBehaviour
{
    private Light lampada;

    private void Start()
    {
        lampada= GetComponent<Light>();
    }
    private void FixedUpdate()
    {
        lampada.intensity = (1f + Mathf.Sin(Time.time*5f)) + (1f + Mathf.Sin(Time.time * 15f));
    }
}
