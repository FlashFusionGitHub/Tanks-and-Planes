using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour {

    private InputDevice controller;

    public CameraController m_camera;

    public List<SquadController> squads;

    private int tankIndex = 0;

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

        SelectedTank(tankIndex);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if there are still squads alive
        if(squads.Count > 0)
        {
            //if the tank index is greater or equal to zero
            if (tankIndex >= 0)
            {
                //set the selection ring the the selected general
                currentCircle.transform.position = squads[tankIndex].m_general.transform.position;

                //if if current selected squad has zero members
                if (squads[tankIndex].numSquadMembers <= 0)
                {
                    //remove that squad from the squads list
                    squads.Remove(squads[tankIndex]);
                    return;
                }

                //if the action 1 button was pressed and the tank index is greater thank zero
                if (controller.Action1.WasPressed)
                {
                    //set the selected generals destination the the makers position
                    squads[tankIndex].m_general.SetDestination(currentMarker.transform.position);
                }
            }

            //if the right D pad button was pressed
            if (controller.DPadRight.WasPressed)
            {
                //check if the index not greater than the amount of squads in the list
                if (tankIndex == squads.Count - 1)
                {
                    //set the tank index so the first element will have a selection ring
                    tankIndex = -1;
                }
                //destory the currect circle
                Destroy(currentCircle);
                //increment the tank index
                tankIndex += 1;
                //and access the selectedTank method, pasing in the tank index
                SelectedTank(tankIndex);
            }

            if (controller.DPadLeft.WasPressed)
            {
                //if the tank index is less than or equal to zero
                if (tankIndex <= 0)
                {
                    //set the tank index to the current squad size
                    tankIndex = squads.Count;
                }
                //destory the currect circle
                Destroy(currentCircle);
                //decrement the tank index
                tankIndex -= 1;
                //and access the selectedTank method, pasing in the tank index
                SelectedTank(tankIndex);
            }

            if(currentMarker.GetComponent<MarkerActor>().GetEnemyToAttack() != null && controller.Action1.WasPressed)
            {
                foreach(TankActor tank in squads[tankIndex].m_squad)
                {
                    tank.Attack(currentMarker.GetComponent<MarkerActor>().GetEnemyToAttack());
                }
            }
        }
        else
        {
            Debug.Log("No Squads Left");
            Destroy(currentCircle);
        }
    }

    //this method is used for setting the cameras position and the selection rings position
    void SelectedTank(int index)
    {
        //the selection ring will be instantiated at the selected squad memeber, the object is then stored in the currentCircle gameobject
        currentCircle = Instantiate(circle, squads[tankIndex].m_general.transform.position, Quaternion.Euler(-90, 0, 0));
        //to make finding the selected unit easier, set the cameras position so it looks at the selected unit
        m_camera.setPosition(squads[tankIndex].m_general.transform.position.x, squads[tankIndex].m_general.transform.position.z - 10);
    }
}
