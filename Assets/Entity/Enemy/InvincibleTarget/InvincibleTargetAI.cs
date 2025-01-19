using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvincibleTargetAI : MonoBehaviour
{
    public GameObject target;

    private void Awake()
    {

    }

    private void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
