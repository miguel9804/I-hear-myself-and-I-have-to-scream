using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BNG
{
    public class Timer : MonoBehaviour
    {
        public InputActionReference InputAction = default;
        public float timeRemaining = 10;
        public bool timerIsRunning = false;
        private bool lookHand = false;
        public Text timeText;
        public GameObject loseScreen;
        public GameObject miniTimer;
        private SmoothLocomotion smL;
        private EcoVision_Effect vision;
        private Cordura cordura;

        private void Start()
        {
            timerIsRunning = true;
            smL = GetComponent<SmoothLocomotion>();
            vision = GetComponentInParent<EcoVision_Effect>();
            cordura = GetComponent<Cordura>();
        }
        private void OnEnable()
        {
            InputAction.action.performed += ToggleActive;
        }

        private void OnDisable()
        {
            InputAction.action.performed -= ToggleActive;
        }

        public void ToggleActive(InputAction.CallbackContext context)
        {

            if (!lookHand)
            {

                miniTimer.SetActive(true);
                lookHand = true;

            }
            else if (lookHand)
            {
                miniTimer.SetActive(false);
                lookHand = false;
            }

        }

        void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                    
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                    GameEnd();
                }
            }
            if(cordura.dead)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameEnd();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GameEnd();
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        void GameEnd()
        {
           // loseScreen.transform.SetParent(vision.gameObject.transform);
            vision.enabled = false;
            smL.MovementSpeed = 0;
            smL.StrafeSpeed = 0;
            loseScreen.SetActive(true);
            cordura.dead = true;
            //Time.timeScale = 0f;
        }

        /*public void restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void mainMenu()
        {
            SceneManager.LoadScene("MenuPrincipal");
            Time.timeScale = 1f;
        }*/
        //Estos no se necesitan con el nuevo modo del Menu de perdida
       
    }
}
