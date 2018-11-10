using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasketManager : MonoBehaviour {

    public float 
        nextItemXpos, 
        nextItemYpos;

    public int 
        itemsInBasket,
        nextPosIndexToPass;

    public Vector2[] itemPositioning;
    public String[] items;

	// Use this for initialization
	void Start () {
        nextItemXpos = -2.0f;
        nextItemYpos = -3.5f;
        itemsInBasket = 0;
        nextPosIndexToPass = 0;
        itemPositioning = new Vector2[100];
        items = new String[100];
        itemPositioning[nextPosIndexToPass] = new Vector2(nextItemXpos, nextItemYpos);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector2 getNextPosInBasket(int index)
    {
        return itemPositioning[index];
    }

    public void addItemToBasket(string item_name, int item_index)
    {
        itemsInBasket += 1;
        nextPosIndexToPass += 1;
        nextItemXpos += 0.75f;
        itemPositioning[nextPosIndexToPass] = new Vector2(nextItemXpos, nextItemYpos);
        items[item_index] = item_name;
    }

    public void removeItemFromBasket(int item_index)
    {
        itemsInBasket -= 1;
        items[item_index] = null;
        //String[] tmp = new String[100] ;
        //int start = 0;
        //int end = items.Length-1;
        //for(int i = 0; i < items.Length; i++)
        //{
        //    if (items[i] != null)
        //    {
        //        tmp[start] = items[i];
        //        start++;
        //    }
        //    else
        //    {
        //        tmp[end] = items[i];
        //        end--;
        //    }
        //}

        //for (int i = 0; i < items.Length; i++)
        //{
        //    items[i] = tmp[i];
        //}
    }
}
