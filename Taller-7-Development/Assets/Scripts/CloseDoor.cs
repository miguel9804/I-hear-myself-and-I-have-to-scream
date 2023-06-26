using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator dor;
    public AudioSource openDoor;
    int move;
    bool opened = true;
    // Start is called before the first frame update
    void Start()
    {
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "hands_r_gloves_mat06")
        {
            if (!opened)
            {
                dor.SetBool("Open", false);
                openDoor.Play();
                opened = false;
            }
            else
            {
                dor.SetBool("Open", true);
                openDoor.Play();
                opened = true;
            }

        }
    }
}
