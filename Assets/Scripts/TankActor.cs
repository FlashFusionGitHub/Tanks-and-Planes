using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankActor : MonoBehaviour {

    public bool isCommander;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
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
}
