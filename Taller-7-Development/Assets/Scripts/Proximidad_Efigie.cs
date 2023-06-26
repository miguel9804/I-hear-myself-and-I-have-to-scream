using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximidad_Efigie : MonoBehaviour
{
    [SerializeField]GameObject particulas;
    Light luz;
    float tiempoTransicion;
    bool enRango;

    private void Awake()
    {
        luz = particulas.GetComponentInChildren<Light>();
        enRango = false;
    }

    private void Update()
    {
        if (enRango)
        {
            if (tiempoTransicion < 1)
            {
                luz.intensity = 4 * tiempoTransicion;
                tiempoTransicion += Time.deltaTime;
            }
        }
        else
        {
            if (tiempoTransicion > 0)
            {
                luz.intensity = 4 * tiempoTransicion;
                tiempoTransicion -= Time.deltaTime;
            }
            else
            {
                particulas.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HeadCollision")
        {
            particulas.gameObject.SetActive(true);
            enRango = true;
            tiempoTransicion = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeadCollision")
        {
            tiempoTransicion = 1;
            enRango = false;
        }
    }
}
