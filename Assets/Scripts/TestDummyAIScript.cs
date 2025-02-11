using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestDummyAIScript : MonoBehaviour
{
    public bool allowFolow;


    private NavMeshAgent agent;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowFolow)
        {
            FollowPlayer();
        }
        else
        {
            StopFollowing();
        }
    }

    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void StopFollowing()
    {
        agent.SetDestination(transform.position);
    }
}
