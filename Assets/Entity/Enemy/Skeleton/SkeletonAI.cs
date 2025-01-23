using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats), typeof(EnemySpawn), typeof(Rigidbody))]
public class SkeletonAI : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Walls;
    public EnemyBehaviour behaviour;
    public int associatedSpawner;

    private NavMeshAgent agent;
    private float agentSpeed;
    private GameObject target;
    private Vector3 targetPos;
    private float wallDistance;
    private Animator animator;

    private int behaviourStage;
    private int currentWall;
    private double searchNodeTime = 0;
    private double searchWallTime = 0;
    private float currentAgentSpeed = 0.5f;

    private void Start()
    {
        Invoke("Spawn", 5f);
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = GetComponent<EnemyStats>().speed;
        behaviourStage = 0;
    }

    void Update()
    {
        Behaviour();
    }

    private void Spawn()
    {
        GetComponent<EnemySpawn>().Spawn(new Vector2(0, 5f));
    }

    private void Behaviour()
    {
        switch (behaviourStage)
        {
            case 0: //spawning

                GetComponent<EnemySpawn>().Spawn(associatedSpawner);
                behaviourStage++;
                break;

            case 1: //roaming

                if (SearchNode(out targetPos) && searchNodeTime >= behaviour.searchNodeInterval)
                {
                    searchNodeTime = 0;

                    currentAgentSpeed = UnityEngine.Random.Range(0.4f, 0.7f);
                    agent.SetDestination(targetPos);
                }

                if (Vector3.Distance(this.transform.position, Player.transform.position) <= behaviour.playerAggroDistance)
                {
                    behaviourStage++;
                }

                agent.speed = currentAgentSpeed * agentSpeed;
                animator.SetFloat("Speed", currentAgentSpeed);

                break;

            case 2: //following and attacking player
                agent.SetDestination(Player.transform.position);
                if (Vector3.Distance(this.transform.position, Player.transform.position) <= behaviour.playerAttackDistance)
                {
                    //Attack animation and damage TODO
                }
                if (Vector3.Distance(this.transform.position, Player.transform.position) > behaviour.playerAttackDistance * 1.5)
                {
                    Invoke("behaviourStage--", 4f);
                }

                break;

            case 3: //following and attacking walls
                if (SearchClosestWall(out target) && searchWallTime >= behaviour.searchNodeInterval)
                {
                    searchWallTime = 0;

                    currentAgentSpeed = 0.6f;
                    agent.SetDestination(target.transform.position);
                }
                if (Vector3.Distance(this.transform.position, target.transform.position) <= behaviour.wallAttackDistance)
                {
                    //Attack animation and damage TODO
                }

                agent.speed = currentAgentSpeed * agentSpeed;
                animator.SetFloat("Speed", currentAgentSpeed);

                break;

        }
        searchNodeTime += Time.deltaTime;
        Debug.Log(searchNodeTime);
        searchWallTime += Time.deltaTime;

    }

    private bool SearchNode(out Vector3 newPos)
    {
        Vector3 pos = this.transform.position;
        float nd = behaviour.searchNodeDistance;

        newPos = new Vector3(0f, 300f, 0f);
        newPos.x = UnityEngine.Random.Range(pos.x - nd, pos.x + nd);
        newPos.z = UnityEngine.Random.Range(pos.z - nd, pos.z + nd);
        if (Physics.Raycast(newPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            newPos = hit.point;
            return true;
        }
        return false;
    }
    private bool SearchClosestWall(out GameObject _wall)
    {
        wallDistance = 99999999f;
        _wall = null;
        foreach (GameObject wall in Walls)
        {
            if (Vector3.Distance(this.transform.position, wall.transform.position) < wallDistance)
            {
                wallDistance = Vector3.Distance(this.transform.position, wall.transform.position);
                _wall = wall;
            }
        }
        if (_wall != null)
        {
            return true;
        }
        return false;
    }
}

[Serializable]
public struct EnemyBehaviour
{
    public float searchNodeDistance;
    public float searchNodeInterval;

    public float playerAggroDistance;
    public float playerAggroResetTime;
    public float playerAttackDistance;

    public float wallAttackDistance;
}