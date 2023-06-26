using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class EcoVision_Effect : MonoBehaviour
{
    [SerializeField] AnimationCurve curvaControlLatidos;
    [SerializeField] AnimationCurve curvaControlPasos;
    [SerializeField] float duracion;
    [SerializeField] float intensidadMax;
    [SerializeField] AudioSource audioLatidos;
    [SerializeField] AudioSource audioPasos;

    float tiempoLatidos;
    [SerializeField]float frecuenciaPasos;
    float suavizadoLuzPasos;
    bool reiniciandoPaso = false;
    [SerializeField]Light luzLatidos;
    [SerializeField]Light luzActivable;
    [SerializeField]Light luzPasos;

    private void Awake()
    {
        tiempoLatidos = 0;
        suavizadoLuzPasos = 0;
        frecuenciaPasos = 0;
    }

    private void Update()
    {
        tiempoLatidos = tiempoLatidos < duracion ? tiempoLatidos + Time.deltaTime : 0;
        if(tiempoLatidos == 0)
        {
            audioLatidos.PlayOneShot(audioLatidos.clip);
        }
        luzLatidos.intensity = curvaControlLatidos.Evaluate(tiempoLatidos / duracion) * intensidadMax;

        if (Input.GetKeyDown(KeyCode.K) || InputBridge.Instance.BButtonDown)
        {
            luzActivable.intensity = 2f;
        }
        luzActivable.intensity = luzActivable.intensity > 0 ? luzActivable.intensity - (Time.deltaTime*2/4) : 0;

        luzPasos.intensity = luzPasos.intensity > 0 ? luzPasos.intensity - Time.deltaTime : 0;
        frecuenciaPasos = frecuenciaPasos > 0 ? frecuenciaPasos - Time.deltaTime : 0;
    }

    public void NuevaDuracion(float nuevaDuracion)
    {
        duracion = nuevaDuracion;
    }

    public void VisiondePasos()
    {
        if(frecuenciaPasos<=0 && reiniciandoPaso)
        {
            reiniciandoPaso = false;
            suavizadoLuzPasos = 0;
            audioPasos.PlayOneShot(audioPasos.clip);
        }
        else if(frecuenciaPasos <= 0 && !reiniciandoPaso)
        {
            luzPasos.intensity = Mathf.Lerp(luzPasos.intensity, 1.75f, suavizadoLuzPasos);
            suavizadoLuzPasos += Time.deltaTime;
        }
        if(suavizadoLuzPasos >= 0.25f && !reiniciandoPaso)
        {
            frecuenciaPasos = 0.25f;
            reiniciandoPaso = true;
        }
    }
}
