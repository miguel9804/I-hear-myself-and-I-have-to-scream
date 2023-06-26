using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator dor;
    public AudioSource openDoor;
    float timer;
    int move;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = 2f;
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            timer = 0;
        }
       // Debug.Log("Tiempo:" + timer.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "hands_r_gloves_mat06" && timer == 0)
        {
            timer = 2f;
            Debug.Log("Entra al condicional");

            if (!opened)
            {
                dor.SetBool("Open", true);
                openDoor.Play();
                opened = true;
            }
            else {
                dor.SetBool("Open", false);
                openDoor.Play();
                opened = false;
            }
        }
    }
    
}
