﻿using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {


    private InputDevice controller;

    public List<TankActor> tanks;



    private int TankIndex = 0;

    private void Awake()
    {
        controller = InputManager.Devices[0];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (controller.DPadRight.WasPressed)
        {
            if (TankIndex == tanks.Count - 1)
                return;

            TankIndex += 1;

            SelectedTank(TankIndex);
        }

        if (controller.DPadLeft.WasPressed)
        {
            if (TankIndex == 0)
                return;

            TankIndex -= 1;

            SelectedTank(TankIndex);
        }
    }

    void SelectedTank(int index)
    {
        Debug.Log(tanks[index].name);
    }
}
