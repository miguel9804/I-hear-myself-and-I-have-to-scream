using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaCarga : MonoBehaviour
{
    [SerializeField] Image[] puntos;
    float appearingCD,loadingCounter;
    int contador;


    void Start()
    {
        appearingCD = 0.5f;
        contador = 0;
        StartCoroutine(CargarEscenaASync());
    }

    private void Update()
    {
        appearingCD -= Time.deltaTime;
        
        if(appearingCD <= 0)
        {
            if(contador == 3)
            {
                contador = 0;
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < puntos.Length; i++)
            {
                if (!puntos[i].gameObject.activeSelf)
                {
                    puntos[i].gameObject.SetActive(true);
                    contador++;
                    break;
                }
            }
            appearingCD = 0.5f;
        }
        
    }

    IEnumerator CargarEscenaASync()
    {
        yield return new WaitForSeconds(1.5f);
        AsyncOperation cargarEscena = SceneManager.LoadSceneAsync(2);
        yield return new WaitForEndOfFrame();
    }
}
