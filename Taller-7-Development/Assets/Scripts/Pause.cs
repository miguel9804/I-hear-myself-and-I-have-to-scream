using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BNG
{
    public class Pause : MonoBehaviour
    {
        public InputActionReference InputAction = default;
        public GameObject ToggleObject = default;
        private bool pause = false;
        private SmoothLocomotion smL;
        private EcoVision_Effect vision;
        [SerializeField]
        private Transform padre, spwan;
        private Cordura cordura;
        private float moveSpeed, strafeSpeed;

        private void Awake()
        {
            smL = GetComponent<SmoothLocomotion>();
            vision = GetComponentInParent<EcoVision_Effect>();
            cordura = GetComponent<Cordura>();
            moveSpeed = smL.MovementSpeed;
            strafeSpeed = smL.StrafeSpeed;
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

            if (!pause && !cordura.dead)
            {
                
                ToggleObject.SetActive(true);
                ToggleObject.transform.SetParent(padre.gameObject.transform);
                vision.enabled = false;
                pause = true;
                smL.MovementSpeed = 0;
                smL.StrafeSpeed = 0;
                Time.timeScale = 0f;
                AudioSource[] audios = FindObjectsOfType<AudioSource>();
                foreach (AudioSource a in audios)
                {
                    a.Pause();
                }

            }
            else if (pause)
            {
                ToggleObject.SetActive(false);
                ToggleObject.transform.SetParent(spwan.transform);
                ToggleObject.transform.position = spwan.transform.position;
                ToggleObject.transform.rotation = spwan.transform.rotation;
                vision.enabled = true;
                pause = false;
                smL.MovementSpeed = moveSpeed;
                smL.StrafeSpeed = strafeSpeed;
                Time.timeScale = 1f;
                AudioSource[] audios = FindObjectsOfType<AudioSource>();
                foreach(AudioSource a in audios)
                {
                    a.Play();
                }

            }

        }

        public void Menu()
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
        public void Continue()
        {
            pause = false;
            ToggleObject.SetActive(false);
            ToggleObject.transform.SetParent(smL.gameObject.transform);
            vision.enabled = true;
            pause = false;
            smL.MovementSpeed = moveSpeed;
            smL.StrafeSpeed = strafeSpeed;
            Time.timeScale = 1f;

        }
    }

}