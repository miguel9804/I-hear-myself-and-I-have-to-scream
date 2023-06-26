using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
    public class EventsBridge : MonoBehaviour
    {
        [SerializeField] GameObject playerController;
        [SerializeField] AudioGroup eventAudioGroup;
        [SerializeField] AnimationCurve volumeCurve;

        [Tooltip("El nivel de peligro se mide de Nivel 1 a 10")]
        [SerializeField] float nivelDePeligro; //Nivel De Peligro: 1 - 10
        [SerializeField] bool persistence;
        [SerializeField] float eventDuration;

        bool evento; 
        bool active;
        private AudioManager audioManagerScript;
        private Cordura corduraScript;
        float timer;

        void Start()
        {
            timer = 0;
            active = true;
            StopAudio();
            corduraScript = playerController.GetComponent<Cordura>();
        }

        private void Update()
        {
            if (evento)
            {
                timer += Time.deltaTime;
                if (timer >= eventDuration)
                {
                    //Debug.Log("[HidroLion] Tiempo de Evento Terminado");
                    DetenerEvento();
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && active)
            {
                ReducirCordura();
            }
        }

        public void ReducirCordura()
        {
            evento = true;
            corduraScript.AcelerarCordura(nivelDePeligro);
            eventAudioGroup.PlayEvent();

            //Debug.Log("[HidroLion] Zona de Peligro Nivel: " + nivelDePeligro);

            if (!persistence)
                active = false;
        }

        public void StopAudio()
        {
            eventAudioGroup.Stop();
        }

        public void DetenerEvento()
        {
            timer = 0;
            corduraScript.EstabilizarCordura();
            StopAudio();
            //Debug.Log("[HidroLion] Detniendo Evento");

            evento = false;
        }
    }
}

