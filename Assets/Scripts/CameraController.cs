using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    private InputDevice controller;

    public float MinZoom = 10.0f, MaxZoom = 50.0f;
    public float MinPanX = -50.0f, MaxPanX = 50.0f;
    public float MinPanZ = -50.0f, MaxPanZ = 50.0f;

    private void Awake()
    {
        controller = InputManager.Devices[0];
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controller.RightTrigger.IsPressed)
        {
            transform.position += new Vector3(0, -controller.RightStickY, 0);

            float zoom = Mathf.Clamp(transform.position.y, 10.0f, 50.0f);

            transform.position = new Vector3(transform.position.x, zoom, transform.position.z);
        }
        else
        {
            this.transform.position += new Vector3(controller.RightStickX, 0, controller.RightStickY);

            float panX = Mathf.Clamp(transform.position.x, -50.0f, 50.0f);
            float panZ = Mathf.Clamp(transform.position.z, -50.0f, 50.0f);

            transform.position = new Vector3(panX, transform.position.y, panZ);
        }
    }
}
