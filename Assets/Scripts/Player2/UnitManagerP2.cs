using InControl;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManagerP2 : MonoBehaviour {

    private InputDevice m_controller;
    private GameObject m_currentSelectionCircle;
    private int m_squadIndex = 0;

    public List<SquadController> m_squads = new List<SquadController>();

    public GameObject m_selectionCircle;
    public NavigationArrowActorP2 m_navigationMarker;

    public CameraControllerP2 m_camera;


    private void Awake()
    {
        m_controller = InputManager.Devices[1];
    }

    // Use this for initialization
    void Start () {

        m_squads = GetComponentsInChildren<SquadController>().ToList();

        SelectedTank(m_squadIndex);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_squads.Count > 0)
        {
            foreach (SquadController squad in m_squads.ToList())
            {
                if (squad == null)
                {
                    if (squad == m_squads[m_squadIndex])
                        m_squadIndex = 0;

                    m_squads.Remove(squad);
                }
            }

            if (m_squads[m_squadIndex].m_currentGeneral != null)
                m_currentSelectionCircle.transform.position = m_squads[m_squadIndex].m_currentGeneral.transform.position;

            if (m_controller.Action1.WasPressed)
            {
                if (m_navigationMarker.GetEnemyToAttack() != null)
                {
                    m_squads[m_squadIndex].m_enemy = m_navigationMarker.GetEnemyToAttack();
                }
                else
                {
                    m_squads[m_squadIndex].m_enemy = null;
                    m_squads[m_squadIndex].m_currentGeneral.m_agent.SetDestination(m_navigationMarker.transform.position);
                }
            }

            if (m_controller.RightStickButton.WasPressed)
            {
                m_camera.SetPosition(m_navigationMarker.transform.position.x, m_navigationMarker.transform.position.z + 10);
            }

            //if the right D pad button was pressed
            if (m_controller.DPadRight.WasPressed)
            {
                //check if the index not greater than the amount of squads in the list
                if (m_squadIndex == m_squads.Count - 1)
                {
                    //set the tank index so the first element will have a selection ring
                    m_squadIndex = -1;
                }
                //destory the currect circle
                Destroy(m_currentSelectionCircle);
                //increment the tank index
                m_squadIndex += 1;
                //and access the selectedTank method, pasing in the tank index
                SelectedTank(m_squadIndex);
            }

            if (m_controller.DPadLeft.WasPressed)
            {
                //if the tank index is less than or equal to zero
                if (m_squadIndex == 0)
                {
                    //set the tank index to the current squad size
                    m_squadIndex = m_squads.Count;
                }
                //destory the currect circle
                Destroy(m_currentSelectionCircle);
                //decrement the tank index
                m_squadIndex -= 1;
                //and access the selectedTank method, pasing in the tank index
                SelectedTank(m_squadIndex);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SelectedTank(int index)
    {
        //the selection ring will be instantiated at the selected squad memeber, the object is then stored in the currentCircle gameobject
        m_currentSelectionCircle = Instantiate(m_selectionCircle, m_squads[index].m_currentGeneral.transform.position, Quaternion.Euler(-90, 0, 0));
        //to make finding the selected unit easier, set the cameras position so it looks at the selected unit
        m_camera.SetPosition(m_squads[index].m_currentGeneral.transform.position.x, m_squads[index].m_currentGeneral.transform.position.z - 10);
    }
}
