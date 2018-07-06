using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankActor : MonoBehaviour {

    public bool m_isGeneral;

    public NavMeshAgent m_agent;

    public GameObject m_turret;

    public bool m_team1;

    public bool m_team2;

    public float m_health = 10;

    // Use this for initialization
    void Start ()
    {
        m_agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void SetDestination(Vector3 position)
    {
        m_agent.SetDestination(position);
    }

    public void FollowLeader(TankActor commander)
    {
        m_agent.SetDestination(commander.transform.position);
    }
}
