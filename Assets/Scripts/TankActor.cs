using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TankActor : MonoBehaviour {

    //Tanks NavMeshAgent
    public NavMeshAgent m_agent;

    //tanks rank
    public bool m_isGeneral;

    //Available teams
    public bool m_team1unit;
    public bool m_team2unit;

    //tanks health
    public float m_starthealth = 100;
    private float m_health;

    //tanks attack time
    public float m_AttackTime;
    private float m_AttackTimer = 0;

    public GameObject m_turret;

    public Image m_healthBar;

    // Use this for initialization
    void Start () {
        m_health = m_starthealth;

        m_agent = GetComponent<NavMeshAgent>();

        if (!m_isGeneral)
            m_agent.stoppingDistance = 5.0f;
        else
            m_agent.stoppingDistance = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if(m_team1unit)
        {
            foreach (TankActor tank in FindObjectsOfType<TankActor>())
            {
                if (tank.m_team2unit)
                {
                    if (Vector3.Distance(transform.position, tank.transform.position) <= 20)
                    {
                        if (tank.m_health >= 0)
                        {
                            m_AttackTimer -= Time.deltaTime;

                            if (m_AttackTimer <= 0.0f)
                            {
                                tank.TakeDamage(20);
                                m_AttackTimer = m_AttackTime;
                            }
                        }
                    }
                }
            }
        }

        if (m_team2unit)
        {
            foreach (TankActor tank in FindObjectsOfType<TankActor>())
            {
                if (tank.m_team1unit)
                {
                    if (Vector3.Distance(transform.position, tank.transform.position) <= 20)
                    {
                        if (tank.m_health >= 0)
                        {
                            m_AttackTimer -= Time.deltaTime;

                            if (m_AttackTimer <= 0.0f)
                            {
                                tank.TakeDamage(20);
                                m_AttackTimer = m_AttackTime;
                            }
                        }
                    }
                }
            }
        }
    }

    public void FollowGeneral(TankActor general)
    {
        m_agent.SetDestination(general.transform.position);
    }

    public void TakeDamage(int damageAmount)
    {
        m_health -= damageAmount;

        Debug.Log(m_health);

        m_healthBar.fillAmount = m_health / m_starthealth;
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
                enemy.TakeDamage(20);
                m_AttackTimer = m_AttackTime;
            }
        }
    }

    public float GetHealth()
    {
        return m_health;
    }
}
