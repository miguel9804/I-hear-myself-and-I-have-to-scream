using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [Tooltip("Añadir la Musica o Efecto de sonido que será detonado")]
    [SerializeField] AudioClip sound;

    [Tooltip("Referenciar el Objeto *Music* ubicado en: XR-Rig Advanced/Player Controller/Camera Rig/Tracking Space")]
    [SerializeField] GameObject musicGenerator;
    
    [Header("Audio Mode")]
    [Tooltip("True para cambiar Musica - False para detonar sonido")]
    [SerializeField] bool audioMode; //True para cambiar musica, False para detonar sonido

    [SerializeField] bool unique; //Si está activo, el collider se desahbilitará después de reproducir el sonido por primera vez

    AudioSource audioSource;

    Collider collider;

    void Awake()
    {
        audioSource = musicGenerator.GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioMode)
            {
                audioSource.clip = sound;
                audioSource.Play();
                if (unique)
                {
                    collider.enabled = false;
                }
            }
            else if (!audioMode)
            {
                audioSource.PlayOneShot(sound);
                if (unique)
                {
                    collider.enabled = false;
                }
            }
        }
    }
}
