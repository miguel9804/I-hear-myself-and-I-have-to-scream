using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{

   public Transform pos1;
   public Transform pos2;
   public int touch=0;


   
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")  &&  touch==0)
        {
            touch = 1;
            this.transform.localPosition = pos2.localPosition;
        }
        if(other.CompareTag("Enemy") && touch == 1)
        {
            touch = 2;
            this.transform.localPosition = pos1.localPosition;
        }
        if (other.CompareTag("Enemy") && touch == 2)
        {
            touch = 0;
            this.transform.localPosition = pos2.localPosition;
        }
    }
    
}
