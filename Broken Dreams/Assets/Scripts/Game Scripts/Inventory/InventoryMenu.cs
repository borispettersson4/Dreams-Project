﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class InventoryMenu : MonoBehaviour {

    public GameObject inventoryMenu;
    public static bool inventroyIsUp;
    bool canTrigger = true;

    //Base Coloroation
    Color32 colorBase;
    //Objects to fade in
    public int maxItems;
    public GameObject[] itemsToFade;

    //Extra Objects
    public Selector selector;
    public ItemShack itemShack;

    void FixedUpdate () {
        if (Input.GetKey(KeyCode.E) && inventroyIsUp && canTrigger)
        {
            canTrigger = false;
            inventroyIsUp = false;
            LockMouse.lockMouse = true;
            StartCoroutine(waitTime());
        }
        else if (Input.GetKey(KeyCode.E) && !inventroyIsUp && canTrigger)
        {
            canTrigger = false;
            inventroyIsUp = true;
            LockMouse.lockMouse = false;
            StartCoroutine(waitTime());
        }

        if (inventroyIsUp)
        {
            objectsFadeIn();
            itemShack.enabled = true;
            selector.enabled = true;
            LockMouse.lockMouse = false;
        }
        else
        {
            objectsFadeOut();
            itemShack.enabled = false;
            selector.enabled = false;
            LockMouse.lockMouse = true;
        }

    }

    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.5f);
        canTrigger = true;
        StopCoroutine(waitTime());
    }

    void objectsFadeIn()
    {
       for (int i = 0; i < maxItems; i++)
         itemsToFade[i].GetComponent<Image>().enabled = true;
    }

    void objectsFadeOut()
    {
      for (int i = 0; i < maxItems; i++)
          itemsToFade[i].GetComponent<Image>().enabled = false;
    }
}
