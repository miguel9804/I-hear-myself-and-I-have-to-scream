using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzlelManager : MonoBehaviour
{
    [SerializeField] GameObject transform1;
    [SerializeField] GameObject transform2;
    [SerializeField] GameObject transform3;
    [SerializeField] GameObject transform4;
    [SerializeField] GameObject transform5;
    [SerializeField] int contadorPiezasColocadas;
    [SerializeField] float tiempoTransicionInicial;
    [SerializeField] Image imagenFade;
    [SerializeField] AnimationCurve curvaControlLuzFinal;
    [SerializeField] AnimationCurve curvaControlLuzFeedback;
    [SerializeField] GameObject guiaEfigie;
    [SerializeField] ParticleSystem particulasGuia;
    AudioSource audioFeedback;
    ParticleSystem particulas;
    Renderer renderer;
    
    float visibilidadGuia;
    public bool juegoCompletado;
    private bool desactivarGuia;
    bool feedback;
    float tiempoTransicion;
    float tiempoLuz;
    Color colorInicial;
    Light pointLight;
    

    void Awake()
    {
        visibilidadGuia = 1;
        desactivarGuia = false;
        contadorPiezasColocadas = 0;
        juegoCompletado = false;
        tiempoTransicion = tiempoTransicionInicial;
        colorInicial = imagenFade.color;
        tiempoLuz = 0;
        pointLight = GetComponentInChildren<Light>();
        audioFeedback = GetComponent<AudioSource>();
        particulas = GetComponentInChildren<ParticleSystem>();
        renderer = guiaEfigie.gameObject.GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        if(juegoCompletado && tiempoTransicion > 0)
        {
            pointLight.intensity = curvaControlLuzFinal.Evaluate(tiempoLuz / 5) * 10;
            tiempoLuz = tiempoLuz < 5 ? tiempoLuz + Time.deltaTime : 5;
            tiempoTransicion -= Time.deltaTime;
            imagenFade.color = Color.Lerp(colorInicial, Color.black, (tiempoTransicionInicial - tiempoTransicion)/tiempoTransicionInicial);
        }
        else if(juegoCompletado && tiempoTransicion <= 0)
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
        if(feedback && !juegoCompletado)
        {
            pointLight.intensity = curvaControlLuzFeedback.Evaluate(tiempoLuz);
            tiempoLuz = tiempoLuz < 1 ? tiempoLuz + Time.deltaTime : 1;
        }
        DisableGuide(desactivarGuia);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
        switch(other.gameObject.tag)
        {
            case "Esfinge1":
                transform1.gameObject.SetActive(true);
                Destroy(other.gameObject);
                contadorPiezasColocadas++;
                PlacingFeedback();
                desactivarGuia = true;
                break;
            case "Esfinge2":
                transform2.gameObject.SetActive(true);
                Destroy(other.gameObject);
                contadorPiezasColocadas++;
                PlacingFeedback();
                desactivarGuia = true;
                break;
            case "Esfinge3":
                transform3.gameObject.SetActive(true);
                Destroy(other.gameObject);
                contadorPiezasColocadas++;
                PlacingFeedback();
                desactivarGuia = true;
                break;
            case "Esfinge4":
                transform4.gameObject.SetActive(true);
                Destroy(other.gameObject);
                contadorPiezasColocadas++;
                PlacingFeedback();
                desactivarGuia = true;
                break;
            case "Esfinge5":
                transform5.gameObject.SetActive(true);
                Destroy(other.gameObject);
                contadorPiezasColocadas++;
                PlacingFeedback();
                desactivarGuia = true;
                break;
        }
        if(contadorPiezasColocadas == 5)
        {
            juegoCompletado = true;
        }
    }

    private void PlacingFeedback()
    {
        audioFeedback.PlayOneShot(audioFeedback.clip);
        tiempoLuz = 0;
        feedback = true;
        particulas.Play();
    }

    private void DisableGuide(bool desactivacion)
    {
        if (desactivacion && visibilidadGuia > 0)
        {
            renderer.material.SetFloat("_Visibilidad", Mathf.Lerp(0, 1, visibilidadGuia));
            visibilidadGuia -= Time.deltaTime/1.5f;
            //particulasGuia.Stop();
        }
        else if(visibilidadGuia <= 0)
        {
            guiaEfigie.gameObject.SetActive(false);
            
        }
    }

}
