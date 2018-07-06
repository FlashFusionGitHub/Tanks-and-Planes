using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerActor : MonoBehaviour
{

    private InputDevice controller;

    public float MinXPos = -50.0f, MaxXPos = 50.0f;
    public float MinZPos = -50.0f, MaxZPos = 50.0f;

    public int rotationSpeed = 1;

    TankActor tank;

    private void Awake()
    {
        controller = InputManager.Devices[0];
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(controller.LeftStickX, 0, controller.LeftStickY);

        float markerXPos = Mathf.Clamp(transform.position.x, MinXPos, MaxXPos);
        float markerZPos = Mathf.Clamp(transform.position.z, MinZPos, MaxZPos);

        transform.position = new Vector3(markerXPos, transform.position.y, markerZPos);
    }

    public TankActor GetEnemyToAttack()
    {
        if (tank != null)
            return tank;
        else
            return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankActor>().m_team2)
        {
            tank = other.GetComponent<TankActor>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        tank = null;
    }
}
