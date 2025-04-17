using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestDummyAIScript : MonoBehaviour
{
    public bool allowFolow;


    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;


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

        anim.SetFloat("Speed", agent.velocity.magnitude);
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
