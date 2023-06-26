using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGroup : MonoBehaviour
{
    //Cada Audio group tiene sus propios audio source 

    //arreglo de audioclips
    [SerializeField] AudioClip[] clips, clipsVoice;
    [SerializeField] AudioSource[] sources;
    public bool changingVolume; 
    private bool stopTimers;
    int i = 0;
    int j = 0;
    private float timeWait;
    float time1,time2;
    float timer1, timer2;//which has running seconds 


    //método de play con el que comeinza a sonar con lerp
    private void Start()
    {
        
        changingVolume = false;
        sources[0].clip = clips[0];
        sources[1].clip = clips[1];

        if(clipsVoice.Length > 0)
        sources[2].clip = clipsVoice[0];
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 0f;     //cada soruce en inicail volumen de 0
                       
            sources[i].Play();
        }
        if (gameObject.name == "Default")
            timeWait = 60;
        if (gameObject.name == "Cordura 1")
            timeWait = 40;
        if (gameObject.name == "Cordura 2")
            timeWait = 35;
        if (gameObject.name == "Cordura 3")
            timeWait = 30;

    }

    
    void Update()
    {
        time1 = sources[1].clip.length + timeWait;
        if (clipsVoice.Length > 0)
        {
            time2 = sources[2].clip.length + timeWait;
            timer2 += Time.deltaTime;
        }
            
        timer1 += Time.deltaTime;
        
        //Debug.Log("waitTime = "+ timer1 + "| time = "+ time1);
        //para los efectos aleatorios
        if (stopTimers)
        {timer1 = 0; timer2 = 0;}
        if (timer1 > time1)
        {
            //float wait = Random.Range(40f, 125f);
            int clipSelect = Random.Range(1, clips.Length);
            
            if (clipSelect == i)
                clipSelect = Random.Range(1, clips.Length);
            i= clipSelect;

            sources[1].clip = clips[i];
            sources[1].Play();

            timer1 = 0;
            //Debug.Log("timepo source " + sources[1].clip.length);
        }
        //Para las voces
      
        if (timer2 > time2 && clipsVoice.Length > 0)
        {
            //float wait = Random.Range(40f, 125f);
            int clipSelect = Random.Range(0, clipsVoice.Length);

            if (clipSelect == j)
                clipSelect = Random.Range(0, clipsVoice.Length);
            j = clipSelect;

            sources[2].clip = clipsVoice[j];
            sources[2].Play();

            timer2 = 0;
           // Debug.Log("timepo source " + sources[1].clip.length);
        }


    }

public void Play()
    {
        //ciclo en el que accede a cada soruce y aumenta su voluen de forma progresiva
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 1;                
        }
        changingVolume = true;       
    }

    public void Stop()
    {
        //ciclo en el que accede a cada soruce y se pone los clips correspondiendtes
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 0f;          
        }
        changingVolume = false;
        
    }

    public void PlayEvent()
    {
        sources[0].Play();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 1;
        }
        changingVolume = true;

        sources[0].PlayOneShot(clips[0]);
        sources[0].PlayOneShot(clips[2]);

        int clipSelect = Random.Range(1, clipsVoice.Length);
        sources[0].PlayOneShot(clipsVoice[clipSelect]);
    }

    public void StopEvent()
    {
        sources[0].volume = 0;
    }
}
