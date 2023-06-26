using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlinstruccion : MonoBehaviour
{
    float timer1;
    public float time1;
   

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;

        if (timer1 > time1)
        {
          gameObject.SetActive(false);
        }

    }
}
