using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Transform[] _destinations =new Transform[2];
    int destPoint = 0;
    NavMeshAgent navMeshAgent;
    public TriggerSensor sensor;
    public ParticleSystem ps;
   


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        SetDestination();
        
    }
    void Update()
    {

        var detected = sensor.GetNearest();

       
        if (detected != null)
        {
            Chase(detected);
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f )
        {
            SetDestination();
        }

    }

    private void SetDestination()
    {
        navMeshAgent.speed = 1;
        ps.startColor = Color.yellow;
        if (_destinations.Length==0)
        {
            return;
        }
        navMeshAgent.destination=_destinations[destPoint].position;
       
        destPoint = (destPoint + 1) % _destinations.Length;

        if (destPoint == _destinations.Length)
        {
            destPoint = 0;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guia"))
        {
            navMeshAgent.speed = 0f;
            StartCoroutine("Touch");
        }
        
        
    }

   private void Chase(GameObject target)
    {
        transform.LookAt(target.transform);
        ps.startColor = Color.red;

        navMeshAgent.destination = target.transform.position;
        navMeshAgent.speed = 2;

    }

   IEnumerator Touch()
    {
        yield return new WaitForSeconds(2);
        navMeshAgent.speed = 1f;
        
    }
    
   
}


