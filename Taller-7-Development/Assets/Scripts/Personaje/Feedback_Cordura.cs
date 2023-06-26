using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback_Cordura : MonoBehaviour
{
    [SerializeField] ParticleSystem particulas;
    [SerializeField] Image frameCordura;
    Vector2 tamañoFrameCorduraInicial;
    Vector2 tamañoFrameCorduraAtlernativo;
    Vector2 tamañoFrameCorduraOriginal;

    [SerializeField] AnimationCurve curvaControlFrame;
    [SerializeField]float duracionCurva;
    [SerializeField]int nivelCordura; // 1.5 - nivel 1 "Cordura normal", 1 - nivel 2 "Cordura bajando", 0.85 - nivel 3 "cordura crítica", 4 es un estado sin cambios
    float t;

    EcoVision_Effect ecoVision;


    private void Awake()
    {
        tamañoFrameCorduraOriginal = frameCordura.rectTransform.sizeDelta;
        t = 0;
        ecoVision = GetComponent<EcoVision_Effect>();
    }
    private void Update()
    {
        switch (nivelCordura)
        {
            case 1:
                duracionCurva = 3f;
                ecoVision.NuevaDuracion(3f / 2);
                frameCordura.gameObject.SetActive(false);
                particulas.Stop();
                break;
            case 2:
                duracionCurva = 2.3f;
                ecoVision.NuevaDuracion(2.3f/2);
                frameCordura.gameObject.SetActive(true);
                tamañoFrameCorduraInicial = tamañoFrameCorduraOriginal * 1.15f;
                tamañoFrameCorduraAtlernativo = tamañoFrameCorduraInicial * 1.35f;
                particulas.Stop();
                nivelCordura = 4;
                break;
            case 3:
                duracionCurva = 1.7f;
                ecoVision.NuevaDuracion(1.7f/2);
                particulas.Play();
                tamañoFrameCorduraInicial = tamañoFrameCorduraOriginal;
                nivelCordura = 4;
                break;
            case 4:
                frameCordura.rectTransform.sizeDelta = Vector2.Lerp(tamañoFrameCorduraInicial, tamañoFrameCorduraAtlernativo, curvaControlFrame.Evaluate(t / duracionCurva));
                t = t < duracionCurva ? t + Time.deltaTime : 0;
                break;
            
        }
    }

    public void NuevoNivelCordura(int nuevoNivel)
    {
        nivelCordura = nuevoNivel;
    }


}
