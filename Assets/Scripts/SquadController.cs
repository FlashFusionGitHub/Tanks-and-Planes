using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour {

    public List<TankActor> m_squad;
    public TankActor m_general;

	// Use this for initialization
	void Start () {

        m_squad.Capacity = 4;

        foreach (Transform child in transform)
        {
            m_squad.Add(child.GetComponent<TankActor>()); 
        }

        FindGeneral();
	}
	
	// Update is called once per frame
	void Update () {

        foreach(TankActor tank in m_squad)
        {
            if (!tank.m_isGeneral)
            {
                tank.m_agent.stoppingDistance = 5.0f;

                tank.FollowLeader(m_general);
            }
        }

        PromoteToGeneral();
	}

    void PromoteToGeneral()
    {
        if(m_general.m_health <= 0)
        {
            Destroy(m_general);

            foreach(TankActor tank in m_squad)
            {
                if (!tank.m_isGeneral)
                {
                    tank.m_isGeneral = true;
                    m_general = tank;
                    return;
                }
            }
        }
    }

    void FindGeneral()
    {
        foreach(TankActor tank in m_squad)
        {
            if(tank.m_isGeneral)
            {
                m_general = tank;
            }
        }
    }
}
