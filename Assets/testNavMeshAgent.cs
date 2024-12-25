using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testNavMeshAgent : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.speed = 2f;
        agent.destination = target.transform.position;
    }
}
