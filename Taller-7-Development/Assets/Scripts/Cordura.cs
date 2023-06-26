using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BNG
{
    public class Cordura : MonoBehaviour
    {
        private SmoothLocomotion move;
        [SerializeField]
        private float timePerdida,timeDead;
        [SerializeField]
        private int cordura, perdida1, perdida2;
        private int state =0;
        public bool seguro, dead;

        [SerializeField]
        private Image headCordura;
        [SerializeField]
        private Image[] brainCordura;

        Feedback_Cordura feedback;
        EventsCordureCurves cordureCurves;
        public bool eventoCodura; //Se activa cuando el jugador se encuentra con algo que negativo.
        int[] perdidaBase;

        public int Cordura_ { get => cordura; }


        // Start is called before the first frame update
        void Awake()
        {
            move = GetComponent<SmoothLocomotion>();
            cordura = 100;
            feedback = GetComponent<Feedback_Cordura>();
            cordureCurves = GetComponent<EventsCordureCurves>();

            perdidaBase = new int[2];
            perdidaBase[0] = perdida1;
            perdidaBase[1] = perdida2;
        }

        // Update is called once per frame
        void Update()
        {
            headCordura.fillAmount = (float)cordura / 100f;
            CorduraUI();
            if (cordura <= 0)
            {
                dead = true;
            }

            if (!seguro)
            {
                NivelCordura();
            }
            else
            {
                cordura += 2 + (int)Time.deltaTime;
                timePerdida = 1.5f;
                if (cordura>100)
                {
                    cordura = 100;
                }
            }
            if(dead)
            {
                timeDead -= Time.deltaTime;
                if(timeDead<=0f)
                {
                    SceneManager.LoadScene("MenuPrincipal");
                    Time.timeScale = 1f;
                }
            }
        }
        private void CorduraUI ()
        {
            if(cordura>=70)
            {
                brainCordura[0].gameObject.SetActive(true);
                brainCordura[1].gameObject.SetActive(false);
                brainCordura[2].gameObject.SetActive(false);
                brainCordura[3].gameObject.SetActive(false);
            }
            else if(cordura>=40 && cordura<70)
            {
                brainCordura[0].gameObject.SetActive(false);
                brainCordura[1].gameObject.SetActive(true);
            }
            else if(cordura>=10 && cordura<40)
            {
                brainCordura[1].gameObject.SetActive(false);
                brainCordura[2].gameObject.SetActive(true);
            }
            else if(cordura<10)
            {
                brainCordura[2].gameObject.SetActive(false);
                brainCordura[3].gameObject.SetActive(true);
            }
        }

        private void NivelCordura ()
        {
            float x = Mathf.Clamp(move.movementX, -move.movementX, move.movementX);
            float z = Mathf.Clamp(move.movementZ, -move.movementZ, move.movementZ);
            if (x > 1.5f && x <= 2.25f || z > 1.5f && z <= 2.25f)
            {
                state = 1;
               
            }
            if (x > 2.25 && x <= 3 || z > 2.25 && z <= 3)
            {
                state = 2;
            }

            if (state == 1)
            {
                timePerdida -= Time.deltaTime;
                if (timePerdida <= 0f)
                {
                    cordura -= perdida1;
                    timePerdida = 1.5f;
                }
                if (move.movementZ <= 1.5f && move.movementX <= 1.5f)
                {
                    state = 0;
                    timePerdida = 1.5f;
                }
            }
            if (state == 2)
            {
               
                timePerdida -= Time.deltaTime;
                if (timePerdida <= 0f)
                {
                    cordura -= perdida2;
                    timePerdida = 1.5f;
                }
                if (move.movementZ <= 1.5f && move.movementX <= 1.5f)
                {
                    state = 0;
                    timePerdida = 1.5f;
                }
            }

            if (cordura > 70 && !eventoCodura) { feedback.NuevoNivelCordura(1); }
            else if (cordura > 40 && !eventoCodura) { feedback.NuevoNivelCordura(2); }
            else if (!eventoCodura) { feedback.NuevoNivelCordura(3); }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.tag == "SalaSegura")
            {
                seguro = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "SalaSegura")
            {
                seguro = false;
            }
        }

        public void AcelerarCordura(float aceleracion)
        {
            perdida1 = (int)cordureCurves.curvaPerdida1.Evaluate(aceleracion) * 10;
            perdida2 = (int)cordureCurves.curvaPerdida2.Evaluate(aceleracion) * 10;

            //Debug.Log("[HidroLion] Aceleracion de: " + (int)cordureCurves.curvaPerdida1.Evaluate(aceleracion));
            //Debug.Log("[HidroLion] Aceleracion 2 de: " + (int)cordureCurves.curvaPerdida2.Evaluate(aceleracion));

            eventoCodura = true;
        }

        public void EstabilizarCordura()
        {
            perdida1 = perdidaBase[0];
            perdida2 = perdidaBase[1];
            eventoCodura = false;
        }
    }
}

