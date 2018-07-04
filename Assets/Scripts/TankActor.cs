using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankActor : MonoBehaviour {

    public bool isCommander;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetCommander(bool state)
    {
        isCommander = state;
    }
}
