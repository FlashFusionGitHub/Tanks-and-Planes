using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour {

    public List<TankActor> m_squad;
    public TankActor m_general;

    public int numSquadMembers = 4;

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

        if (numSquadMembers > 0)
        {
            DestoryDeadGrunts();

            foreach (TankActor tank in m_squad)
            {
                if (!tank.m_isGeneral && tank.m_health > 0)
                {
                    tank.m_agent.stoppingDistance = 5.0f;

                    tank.FollowLeader(m_general);
                }
            }

            PromoteToGeneral();
        }
        else
        {
            Destroy(this);
        }
	}

    void DestoryDeadGrunts()
    {
        foreach(TankActor tank in m_squad)
        {
            if(!tank.m_isGeneral && tank.m_health <= 0)
            {
                numSquadMembers -= 1;

                m_squad.Remove(tank);

                Destroy(tank.gameObject);

                return;
            }
        }
    }

    void PromoteToGeneral()
    {
        if(m_general.m_health <= 0)
        {
            Destroy(m_general.gameObject);

            numSquadMembers -= 1;

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
