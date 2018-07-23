using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZoneActor : MonoBehaviour {

    public enum Owner { none, team1, team2 };

    public float capturePercentage;

    public Owner owner;

    public float captureTimer = 0;
    public float captureTime = 10;

    TankActor team1;
    TankActor team2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(capturePercentage == 0)
        {
            owner = Owner.none;
        }

        if(team1 != null && team2 == null)
        {
            if (owner == Owner.none)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.team1;
                    }
                }
            }
            else if(owner == Owner.team2)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage -= 10;
                    captureTimer = captureTime;
                }
            }
            else
            {
                return;
            }
        }

        if(team2 != null && team1 == null)
        {
            if (owner == Owner.none)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage += 10;
                    captureTimer = captureTime;

                    if (capturePercentage >= 100)
                    {
                        owner = Owner.team2;
                    }
                }
            }
            else if (owner == Owner.team1)
            {
                captureTimer -= Time.deltaTime;

                if (captureTimer <= 0)
                {
                    capturePercentage -= 10;
                    captureTimer = captureTime;
                }
            }
            else
            {
                return;
            }
        }
	}

    private void OnTriggerEnter(Collider other) {

        if(other.GetComponent<TankActor>().m_team1unit && other.GetComponent<TankActor>().m_isGeneral)
        {
            team1 = other.GetComponent<TankActor>();
        }

        if (other.GetComponent<TankActor>().m_team2unit && other.GetComponent<TankActor>().m_isGeneral)
        {
            team2 = other.GetComponent<TankActor>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<TankActor>().m_team1unit && other.GetComponent<TankActor>().m_isGeneral)
        {
            team1 = null;
        }

        if (other.GetComponent<TankActor>().m_team2unit && other.GetComponent<TankActor>().m_isGeneral)
        {
            team2 = null;
        }
    }
}
