using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankActor : MonoBehaviour {

    //Tanks NavMeshAgent
    public NavMeshAgent m_agent;

    //tanks rank
    public bool m_isGeneral;

    //Available teams
    public bool m_team1unit;
    public bool m_team2unit;

    //tanks health
    public int m_health;

    //tanks attack time
    public float m_AttackTime;
    private float m_AttackTimer = 0;

    public GameObject m_turret;

    // Use this for initialization
    void Start () {
        m_agent = GetComponent<NavMeshAgent>();

        if (!m_isGeneral)
            m_agent.stoppingDistance = 5.0f;
        else
            m_agent.stoppingDistance = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void FollowGeneral(TankActor general)
    {
        m_agent.SetDestination(general.transform.position);
    }

    public void TakeDamage(int damageAmount)
    {
        m_health -= damageAmount;
    }

    public void AttackEnemy(TankActor enemy)
    {
        m_turret.transform.LookAt(enemy.transform);

        m_agent.SetDestination(enemy.transform.position);

        if (Vector3.Distance(transform.position, enemy.transform.position) <= 20.0f)
        {
            m_agent.stoppingDistance = 15.0f;

            m_AttackTimer -= Time.deltaTime;

            if(m_AttackTimer <= 0.0f)
            {
                enemy.TakeDamage(2);
                m_AttackTimer = m_AttackTime;
            }
        }
    }
}
