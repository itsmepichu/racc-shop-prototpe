using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ScanManager : MonoBehaviour {

    [SerializeField]
    private GameObject 
        itemPrefab,
        itemSpawnerParent;

    public ItemSlug[] itemSprites;

    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private GameManager game_manager;

    // Use this for initialization
    void Start () {
        itemSpawnerParent = GameObject.Find("_ItemsToScan");
        game_manager = FindObjectOfType<GameManager>();

        //string[] names = new string[6];
        //names[0] = "Apple - normal";
        //names[1] = "Watermelon";
        //names[2] = "Apple - normal";
        //names[3] = "Bananas";
        //names[4] = "Apple - green";
        //names[5] = "Apple - fancy";

        //spawnItems(names);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void notifyNextMoveItems()
    {
        foreach(GameObject item in items)
        {
            ScannerItem tmpScannerItem = item.GetComponent<ScannerItem>();
            Vector3 newPos = item.transform.position;
            if(!tmpScannerItem.did_touch_scanner)
            {
                newPos.x += 2.25f;
                tmpScannerItem.newPosToMove = newPos;
                tmpScannerItem.move_this_item = true;
            }
        }
    }

    public void CheckAllItemsInBasket()
    {
        foreach (GameObject item in items)
        {
            ScannerItem tmpScannerItem = item.GetComponent<ScannerItem>();
            if (!tmpScannerItem.in_basket)
            {
                return;
            }
        }
        game_manager.LoadPaymentScreen();
    }

    public void spawnItems(string[] itemNames)
    {
        itemNames = itemNames.Where(name => name != null).ToArray();    // removing null values from the passed array
        items = new GameObject[itemNames.Length];
        int i = 0;
        Vector3 initPos = itemSpawnerParent.transform.position;
        foreach(string name in itemNames)
        {
            Debug.Log(name);
            if(name != null)
            {
                GameObject tmpGameObj = (GameObject)Instantiate(itemPrefab, initPos, Quaternion.identity);
                tmpGameObj.transform.parent = itemSpawnerParent.gameObject.transform;

                SpriteRenderer tmpSpriteRender = tmpGameObj.GetComponent<SpriteRenderer>();
                Item tmpObjItem = tmpGameObj.GetComponent<Item>();

                Sprite to_load = null;
                foreach (ItemSlug slug in itemSprites)
                {
                    if (slug.item_name == name)
                    {
                        to_load = slug.item_image;
                    }
                }
                tmpSpriteRender.sprite = to_load;
                tmpGameObj.AddComponent<BoxCollider2D>();

                items[i] = tmpGameObj;
                ++i;
                initPos.x -= 2;
            }
        }
    }
}
