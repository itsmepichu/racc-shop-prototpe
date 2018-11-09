using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfContainer : MonoBehaviour {

    [SerializeField]
    private GameObject
        shelf,
        priceTag;

    private int shelfMaxSize;

    [SerializeField]
    private GameManager game_manager;

    private float
        shelfStartXpos,
        shelfStartYpos,
        itemStartXpos,
        itemStartYpos,
        itemEndXpos,
        itemEndYpos;
	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ArrangeItems(GameObject[] items, int maxShelfSize)
    {
        if (game_manager.current_level == 1)
        {
            itemStartXpos = -0.5f;
            itemStartYpos = 2.5f;
            itemEndXpos = 2.7f;
            itemEndYpos = -0.5f;
        }
        else if(game_manager.current_level == 2)
        {
            itemStartXpos = -1.5f;
            itemStartYpos = 2.5f;
            itemEndXpos = 4.5f;
            itemEndYpos = -0.5f;
        }

        shelfMaxSize = maxShelfSize;
        float nextitemXpos = itemStartXpos;
        float nextitemYpos = itemStartYpos;
        for(int i=0; i<items.Length; ++i)
        {
            GameObject item = items[i];
            Item tmpItemClass = item.GetComponent<Item>();
            item.transform.position = new Vector2(nextitemXpos, nextitemYpos);
            GameObject tmpPriceTag = (GameObject)Instantiate(priceTag, new Vector2(item.transform.position.x, item.transform.position.y-1.0f), Quaternion.identity);
            TextMesh tmpPriceTextesh = tmpPriceTag.GetComponentInChildren<TextMesh>();
            tmpPriceTextesh.text = tmpItemClass.price.ToString();
            tmpPriceTag.transform.parent = this.gameObject.transform;
            if(nextitemXpos+2.65f < itemEndXpos)
            {
                nextitemXpos += 2.65f;
            } else
            {
                nextitemXpos = itemStartXpos;
                nextitemYpos -= 2.15f;
            }
        }
    }
}
