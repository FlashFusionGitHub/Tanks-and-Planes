using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerP2 : MonoBehaviour {

    private InputDevice m_controller;

    public float m_MinZoom = 10.0f, m_MaxZoom = 50.0f;
    public float m_MinPanX = -50.0f, m_MaxPanX = 50.0f;
    public float m_MinPanZ = -50.0f, m_MaxPanZ = 50.0f;

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
        if (m_controller.RightTrigger.IsPressed)
        {
            transform.position += new Vector3(0, -m_controller.RightStickY, 0);

            float zoom = Mathf.Clamp(transform.position.y, m_MinZoom, m_MaxZoom);

            transform.position = new Vector3(transform.position.x, zoom, transform.position.z);
        }
        else
        {
            this.transform.position += new Vector3(-m_controller.RightStickX, 0, -m_controller.RightStickY);

            float panX = Mathf.Clamp(transform.position.x, m_MinPanX, m_MaxPanX);
            float panZ = Mathf.Clamp(transform.position.z, m_MinPanZ, m_MaxPanZ);

            transform.position = new Vector3(panX, transform.position.y, panZ);
        }
    }

    public void SetPosition(float x, float z)
    {
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
