using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimController : MonoBehaviour
{

    Animator anim;
    Rigidbody rb;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {

        float speed = navMeshAgent.speed;
        anim.SetFloat("Speed",speed);
    }
}
