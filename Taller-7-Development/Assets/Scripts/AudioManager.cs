using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class AudioManager : MonoBehaviour
{

    
    [SerializeField]
    AudioGroup[] audioGroups;
    [SerializeField]
    Cordura corduraScript;
    //Elecicion por nivel de cordura
    // 0 = Default | 1 = Miedo | 2 = Desquicio| 3 = Locura 
    int cordura;
    bool playingScream;
    float timer;
    
    //
    private void Start()
    {
        timer = 0;
        playingScream = false;

       
        
    }



    // Update is called once per frame
    void Update()
    {
        if(corduraScript.seguro == false)
            for (int i = 0; i < audioGroups.Length; i++)
            {
                audioGroups[i].Stop();
            }

        cordura = corduraScript.Cordura_;
        if (cordura > 75 && audioGroups[0].changingVolume == false && corduraScript.seguro == false)
        {
            audioGroups[1].Stop();
            audioGroups[2].Stop();
            audioGroups[3].Stop();
            audioGroups[0].Play();
            Debug.Log("aumentando 0");
        }
        if (cordura <= 75 && cordura > 60 && audioGroups[1].changingVolume == false && corduraScript.seguro == false)
        {
            audioGroups[0].Stop();
            audioGroups[2].Stop();
            audioGroups[3].Stop();
            audioGroups[1].Play();
            Debug.Log("aumentando 1");
        }
        if (cordura <= 60 && cordura > 35 && audioGroups[2].changingVolume == false && corduraScript.seguro == false)
        {
            audioGroups[0].Stop();
            audioGroups[1].Stop();
            audioGroups[3].Stop();
            audioGroups[2].Play();
            Debug.Log("aumentando 2");
        }
        if (cordura <= 35 && audioGroups[2].changingVolume == false && corduraScript.seguro == false)
        {
            audioGroups[0].Stop();
            audioGroups[1].Stop();
            audioGroups[2].Stop();
            audioGroups[3].Play();
            Debug.Log("aumentando 3");
        }

        /*
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cordura =0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cordura = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cordura =2;
        }
       
#endif
        */
        #region Prueba de sonidos
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            probarSonido(clip[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            probarSonido(clip[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            probarSonido(clip[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            probarSonido(clip[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            probarSonido(clip[4]);
        }*/
        #endregion
       

    }

    
  




}
