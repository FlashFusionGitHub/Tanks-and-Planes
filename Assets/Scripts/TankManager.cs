using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {


    private InputDevice controller;

    public List<SquadController> squads;

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
        currentMarker = Instantiate(marker);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (tankIndex >= 0)
        {
            currentCircle.transform.position = squads[tankIndex].m_general.transform.position;

            squads[tankIndex].m_general.m_turret.transform.LookAt(currentMarker.transform);
        }

        if (controller.DPadRight.WasPressed)
        {
            if (tankIndex == squads.Count - 1)
                tankIndex = -1;

            Destroy(currentCircle);

            tankIndex += 1;

            SelectedTank(tankIndex);
        }

        if (controller.DPadLeft.WasPressed)
        {
            if (tankIndex <= 0)
                tankIndex = squads.Count;

            Destroy(currentCircle);

            tankIndex -= 1;

            SelectedTank(tankIndex);
        }

        if (controller.Action1.WasPressed && tankIndex >= 0)
        {
            squads[tankIndex].m_general.SetDestination(currentMarker.transform.position);
        }
    }

    void SelectedTank(int index)
    {
        currentCircle = Instantiate(circle, squads[tankIndex].m_general.transform.position, Quaternion.Euler(-90, 0, 0));
    }
}
