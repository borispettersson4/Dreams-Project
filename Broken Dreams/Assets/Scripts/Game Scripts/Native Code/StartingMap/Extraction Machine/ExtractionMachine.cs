﻿using UnityEngine;
using System.Collections;

public class ExtractionMachine : MonoBehaviour {

    public Lever lever;
    public KeyReciever gear;
    public Engine engine;
    public Basement basement;
    TipsGenerator tips;
    bool key1 = true;
    public Pilars bioPilars;
    public ExtractionDoor doorA;
    public ExtractionDoor doorB;
    public Button electricityButon;
    public GameObject electricity;

    void Awake()
    {
        tips = GameObject.Find("Tips").GetComponent<TipsGenerator>();
    }

    void Update()
    {
        if(key1 && lever.isActivated() && gear.isRecieved())
        {
            key1 = false;
            doorA.open();
            doorB.open();
        }

        if (electricityButon.active && bioPilars.isActive())
        {
            electricity.SetActive(true);
        }
    }

    public bool isOnline()
    {
        return electricity.activeSelf && lever.isActivated() && gear.isRecieved();
    }
}
