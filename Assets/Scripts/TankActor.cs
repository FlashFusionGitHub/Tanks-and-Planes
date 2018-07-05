using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankActor : MonoBehaviour {

    public bool isCommander;

    private NavMeshAgent agent;

    public GameObject turret;

    public bool team1;
    public bool team2;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        if(!isCommander)
            agent.stoppingDistance = 5.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void SetCommander(bool state)
    {
        isCommander = state;
    }

    public void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void FollowLeader(TankActor commander)
    {
        agent.SetDestination(commander.transform.position);
    }
}
