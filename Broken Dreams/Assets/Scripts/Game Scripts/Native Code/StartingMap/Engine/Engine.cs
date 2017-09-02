﻿using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

    public bool waterActive = false;
    public bool heatActive = false;
    public bool engineActive = false;
    bool online = false;

    public Animator spinner;
    public Light pointlight;
    public Button[] hyperDoorButtons;
    public TipsGenerator tips;

    public Valve waterValve;
    public Valve heatValve;         
    public KeyReciever CoolantReciever;

    //Visuals 
    public Material[] floorLights;
    


    //Tools

    bool[] key = { true , true , true };

    void Awake()
    {
        for(int i = 0; i < floorLights.Length;i++)
        floorLights[i].SetColor("_EmissionColor", new Color(0, 0, 0, 0));
        pointlight.enabled = false;
        spinner.Play("Idle");
    }


    void Update()
    {
        waterActive = waterValve.isActive();
        heatActive = heatValve.isActive();

        if(waterValve.col != null)
        waterValve.col.enabled = engineActive;
        if (heatValve.col != null)
            heatValve.col.enabled = engineActive;

        if (CoolantReciever.isRecieved())
        {
            StartCoroutine(flickerLight(0.5f));
        }

        if (engineActive && !heatActive)
        {
            if (key[0])
            {
                key[0] = false;
                spinner.Play("Slow");
                Debug.Log("FUEL READY");
            }
            StartCoroutine(flickerLight(0.1f));
        }
        else if (engineActive && heatActive && !waterActive)
        {
            Debug.Log("TURN THE WHEEEL");
            StartCoroutine(flickerLight(0.5f));
        }
        else if (engineActive && !heatActive && waterActive)
        {
            Debug.Log("TURN THE WHEEEL");
            StartCoroutine(flickerLight(0.5f));
        }
        else if (engineActive && heatActive && waterActive && CoolantReciever.isRecieved() && key[1])
        {
            Debug.Log("IT IS OOOOOOOOOOOONNNNNNNNNN");
            key[1] = false;
            pointlight.enabled = true;
            for (int i = 0; i < floorLights.Length; i++)
                floorLights[i].SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            tips.Show("Engine is online");
            online = true;
            for(int i = 0; i < hyperDoorButtons.Length; i++)
            {
                hyperDoorButtons[i].hasPower = true;
            }

            if (key[2])
            {
                key[2] = false;
                spinner.Play("Fast");
            }
        }
    }

    IEnumerator flickerLight(float x)
    {
        pointlight.enabled = false;
        for (int i = 0; i < floorLights.Length; i++)
            floorLights[i].SetColor("_EmissionColor", new Color(0, 0, 0, 0));
        yield return new WaitForSeconds(x);
        for (int i = 0; i < floorLights.Length; i++)
            floorLights[i].SetColor("_EmissionColor", new Color(1, 1, 1, 1));
        pointlight.enabled = true;
        yield return new WaitForSeconds(x);
        StopCoroutine(flickerLight(x));
    }

    public bool isOnline()
    {
        return online;
    }

}
