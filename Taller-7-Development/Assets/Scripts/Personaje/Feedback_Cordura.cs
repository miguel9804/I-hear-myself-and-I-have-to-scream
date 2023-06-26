using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback_Cordura : MonoBehaviour
{
    [SerializeField] ParticleSystem particulas;
    [SerializeField] Image frameCordura;
    Vector2 tama�oFrameCorduraInicial;
    Vector2 tama�oFrameCorduraAtlernativo;
    Vector2 tama�oFrameCorduraOriginal;

    [SerializeField] AnimationCurve curvaControlFrame;
    [SerializeField]float duracionCurva;
    [SerializeField]int nivelCordura; // 1.5 - nivel 1 "Cordura normal", 1 - nivel 2 "Cordura bajando", 0.85 - nivel 3 "cordura cr�tica", 4 es un estado sin cambios
    float t;

    EcoVision_Effect ecoVision;


    private void Awake()
    {
        tama�oFrameCorduraOriginal = frameCordura.rectTransform.sizeDelta;
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
                tama�oFrameCorduraInicial = tama�oFrameCorduraOriginal * 1.15f;
                tama�oFrameCorduraAtlernativo = tama�oFrameCorduraInicial * 1.35f;
                particulas.Stop();
                nivelCordura = 4;
                break;
            case 3:
                duracionCurva = 1.7f;
                ecoVision.NuevaDuracion(1.7f/2);
                particulas.Play();
                tama�oFrameCorduraInicial = tama�oFrameCorduraOriginal;
                nivelCordura = 4;
                break;
            case 4:
                frameCordura.rectTransform.sizeDelta = Vector2.Lerp(tama�oFrameCorduraInicial, tama�oFrameCorduraAtlernativo, curvaControlFrame.Evaluate(t / duracionCurva));
                t = t < duracionCurva ? t + Time.deltaTime : 0;
                break;
            
        }
    }

    public void NuevoNivelCordura(int nuevoNivel)
    {
        nivelCordura = nuevoNivel;
    }


}
