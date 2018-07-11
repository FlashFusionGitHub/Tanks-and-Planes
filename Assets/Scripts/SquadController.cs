using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquadController : MonoBehaviour {

    public List<TankActor> m_squad = new List<TankActor>();

    public TankActor m_currentGeneral;

    public TankActor m_enemy;

    // Use this for initialization
    void Start () {

        m_squad = GetComponentsInChildren<TankActor>().ToList();

        SetGeneral();
    }

    // Update is called once per frame
    void Update () {

        if (m_squad.Count > 0)
        {
            foreach (TankActor tank in m_squad.ToList())
            {
                if (!tank.m_isGeneral)
                    tank.FollowGeneral(m_currentGeneral);

                if (m_enemy != null)
                {
                    tank.AttackEnemy(m_enemy);
                }

                if (m_enemy == null)
                {
                    if (!tank.m_isGeneral)
                        tank.m_agent.stoppingDistance = 5.0f;
                    else
                        tank.m_agent.stoppingDistance = 0.0f;
                }

                if (tank.GetHealth() <= 0)
                {
                    if (!tank.m_isGeneral)
                    {
                        m_squad.Remove(tank);
                        Destroy(tank.gameObject);
                    }
                    else if (tank.m_isGeneral)
                    {
                        m_squad.Remove(tank);
                        PromoteToGeneral();
                        SetGeneral();
                        Destroy(tank.gameObject);
                    }
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void setEnemy(TankActor enemy)
    {
        m_enemy = enemy;
    }

    void PromoteToGeneral()
    {
        foreach (TankActor tank in m_squad.ToList())
        {
            if(!tank.m_isGeneral)
            {
                tank.m_isGeneral = true;
                return;
            }
        }
    }

    void SetGeneral()
    {
        foreach(TankActor tank in m_squad.ToList())
        {
            if(tank.m_isGeneral)
            {
                m_currentGeneral = tank;
            }
        }
    }
}
