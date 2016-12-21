﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    public string keyTag;
    public bool isLocked;
    Rigidbody doorPhysics;
    HingeJoint hinge;

    //Tools
    bool doorIsOpen = false;
    bool doorActivation;

    void Update()
    {
        lockHandeler();

        stateHandeler();
    }

    public void toggle()
    {
        if (doorIsOpen || !doorIsOpen)
            doorIsOpen = !doorIsOpen;
        doorActivation = true;
        Debug.Log(doorIsOpen);
    }

    void Awake()
    {
        doorPhysics = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
    }
    public void unlockDoor()
    {
        isLocked = false;
    }

    void lockHandeler()
    {
        if (isLocked)
            doorPhysics.isKinematic = true;
        else
            doorPhysics.isKinematic = false;

    }

    void stateHandeler()
    {
        if(!isLocked)
        if (doorIsOpen)
        {
            if (hinge.angle != 90 && doorActivation)
            {
                JointMotor motor = hinge.motor;
                motor.force = 100;
                motor.targetVelocity = 75;
                motor.freeSpin = false;
                hinge.motor = motor;
                hinge.useMotor = true;
            }
            else if (hinge.useMotor && hinge.angle == 90)
            {
                hinge.useMotor = false;
                doorActivation = false;
            }
        }
        else if (hinge.angle != 0 && doorActivation)
        {
            if (hinge.angle != 0 && doorActivation)
            {
                JointMotor motor = hinge.motor;
                motor.force = 100;
                motor.targetVelocity = -75;
                motor.freeSpin = false;
                hinge.motor = motor;
                hinge.useMotor = true;
            }
            else if (hinge.useMotor && hinge.angle == 0)
            {
                hinge.useMotor = false;
                doorActivation = false;
            }
        }
    }
}
