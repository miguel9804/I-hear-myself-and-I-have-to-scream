using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoundManagerEnemy : MonoBehaviour
{
    public AudioClip walkClip;
    public AudioClip runningClip;
    public GameObject screamerObject;
    public AudioSource runner;
    public bool created;

    NavMeshAgent nv;
    void Start()
    {
        nv = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (nv.speed == 2 && created==false)
        {
            Instantiate(screamerObject, this.transform.position, Quaternion.identity);
            created = true;
            runner.clip = runningClip;
            StartCoroutine(Waiter());
        }
       
    }
    
    IEnumerator Waiter()
    {
       yield return new WaitForSeconds(30);
        created = false;
        runner.clip = walkClip;
    }
}
