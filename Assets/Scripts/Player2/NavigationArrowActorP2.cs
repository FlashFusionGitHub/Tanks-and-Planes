using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrowActorP2 : MonoBehaviour {

    private InputDevice m_controller;
    private TankActor m_tank;

    [SerializeField] private float m_MinXPos = -50.0f, m_MaxXPos = 50.0f;
    [SerializeField] private float m_MinZPos = -50.0f, m_MaxZPos = 50.0f;

    public int markerSpeed = 2;

    private void Awake()
    {
         m_controller = InputManager.Devices[1];
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-m_controller.LeftStickX, 0, -m_controller.LeftStickY) / markerSpeed;

        float markerXPos = Mathf.Clamp(transform.position.x, m_MinXPos, m_MaxXPos);
        float markerZPos = Mathf.Clamp(transform.position.z, m_MinZPos, m_MaxZPos);

        transform.position = new Vector3(markerXPos, transform.position.y, markerZPos);
    }

    public TankActor GetEnemyToAttack()
    {
        if (m_tank != null)
            return m_tank;
        else
            return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankActor>().m_team1unit)
        {
            m_tank = other.GetComponent<TankActor>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankActor>().m_team1unit)
        {
            m_tank = null;
        }
    }
}
