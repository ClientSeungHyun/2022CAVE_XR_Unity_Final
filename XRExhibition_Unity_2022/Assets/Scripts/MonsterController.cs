using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    enum STATE { Idle, Chase };

    private GameObject player;
    private int animState;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animState = 0;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        chasePlayer();
        animator.SetInteger("MonsterState", animState);
    }

    bool chasePlayer()
    {
        float distance = (transform.position - player.transform.position).magnitude;
        print(distance);
        if (distance <= 30.0f)
        {
            animState = 1;
            agent.speed = 8.0f;
            agent.SetDestination(player.transform.position);
            return true;
        }
        else
        {
            agent.speed = 3.5f;
            animState = 0;
            return false;
        }
    }
}
