using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {


    private InputDevice controller;

    public List<TankActor> commanderTanks;

    public List<TankActor> party1;
    public List<TankActor> party2;
    public List<TankActor> party3;

    public int numOfCommanders = 3;

    private int tankIndex = -1;

    public GameObject marker;
    public GameObject circle;

    private GameObject currentMarker;
    private GameObject currentCircle;

    private void Awake()
    {
        controller = InputManager.Devices[0];
    }

    // Use this for initialization
    void Start()
    {
        foreach (TankActor tank in party1)
        {
            if (tank.isCommander == true)
            {
                commanderTanks.Add(tank);
            }
        }

        foreach (TankActor tank in party2)
        {
            if (tank.isCommander == true)
            {
                commanderTanks.Add(tank);
            }
        }

        foreach (TankActor tank in party3)
        {
            if (tank.isCommander == true)
            {
                commanderTanks.Add(tank);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(tankIndex >= 0)
            currentCircle.transform.position = commanderTanks[tankIndex].transform.position;

        foreach (TankActor tank in party1)
        {
            if(tank.isCommander == false)
            {
                tank.FollowLeader(commanderTanks[0]);
            }
        }

        foreach (TankActor tank in party2)
        {
            if (tank.isCommander == false)
            {
                tank.FollowLeader(commanderTanks[1]);
            }
        }

        foreach (TankActor tank in party3)
        {
            if (tank.isCommander == false)
            {
                tank.FollowLeader(commanderTanks[2]);
            }
        }

        if (controller.DPadRight.WasPressed)
        {
            if (tankIndex == commanderTanks.Count - 1)
                tankIndex = -1;

            Destroy(currentMarker);
            Destroy(currentCircle);

            tankIndex += 1;

            SelectedTank(tankIndex);
        }

        if (controller.DPadLeft.WasPressed)
        {
            if (tankIndex <= 0)
                tankIndex = commanderTanks.Count;

            Destroy(currentMarker);
            Destroy(currentCircle);

            tankIndex -= 1;

            SelectedTank(tankIndex);
        }

        if (controller.Action1.WasPressed && tankIndex >= 0)
        {
           commanderTanks[tankIndex].SetDestination(currentMarker.transform.position);
        }
    }

    void SelectedTank(int index)
    {
        currentCircle = Instantiate(circle, commanderTanks[index].transform.position, Quaternion.Euler(-90, 0, 0));
        currentMarker = Instantiate(marker, commanderTanks[index].transform.position, Quaternion.identity);
    }
}
