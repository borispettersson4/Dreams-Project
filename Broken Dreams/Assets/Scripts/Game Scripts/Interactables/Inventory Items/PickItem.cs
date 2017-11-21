﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickItem : MonoBehaviour {

   public  Sprite itemImage;
   public string itemTag;
   public  string itemName;
   public  string itemDesc;
   public ItemShack itemShack;
   GameObject tipsGen;
   TipsGenerator tips;
   public string messageOnPickUp;
   public bool autoFindItemShack = true;
   public bool autoFindWeaponShack = false;
   public bool destroyOnPickUp = true;

    void Awake()
    {
        tipsGen = GameObject.Find("ItemTips");
        tips = tipsGen.GetComponent<TipsGenerator>();

    }

    void Update()
    {
        if (autoFindItemShack)
        {
            itemShack = GameObject.Find("ItemShack").GetComponent<ItemShack>();
            autoFindWeaponShack = false;
        }
        else if (autoFindWeaponShack)
        {
            itemShack = GameObject.Find("Weapon Wheel").GetComponent<ItemShack>();
            autoFindItemShack = false;
        }
    }

    public void pickUpItem()
    {
        itemShack.add(new Item(itemImage, itemTag, itemName, itemDesc));
        //tips.Show(messageOnPickUp);
        if (destroyOnPickUp)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
