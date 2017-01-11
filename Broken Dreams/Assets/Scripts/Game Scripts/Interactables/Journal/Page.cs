﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public Sprite emptyImage;
    protected Sprite image;
    public bool startEmpty;

    void OnDisable()
    {
        if (startEmpty)
        {
            this.delete();
        }
    }

    public Page()
    {
        image = emptyImage;
    }

    public Page(Sprite aimage)
    {
        image = aimage;
    }

    public Page(Page aPage)
    {
        image = aPage.image;
    }

    public Sprite getImage()
    {
        return (image);
    }

    public void setImage(Sprite i)
    {
        image = i;
    }

    public void setImageComponent(Sprite i)
    {
        GetComponent<Image>().sprite = i;
    }

    public bool isEmpty()
    {
        return (this.getImage() == emptyImage);
    }

    public void checkIfEmpty()
    {
        Debug.Log(isEmpty());
    }

    public void delete()
    {
        setImage(emptyImage);
        setImageComponent(emptyImage);
    }

}
