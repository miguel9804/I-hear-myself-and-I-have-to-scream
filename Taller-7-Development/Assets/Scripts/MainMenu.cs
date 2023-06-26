using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void EscenaJuego()
    {
        SceneManager.LoadScene("PantallaCarga");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Ha salido");
    }

    
}
