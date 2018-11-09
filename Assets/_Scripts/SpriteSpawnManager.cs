using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SpriteSpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject 
        item,
        listMenu,
        shelves;

    [SerializeField]
    private ShelfContainer shelfContainer;

    [SerializeField]
    private Sprite[] sprites;

    // Use this for initialization
    void Start () {
        shelves = GameObject.Find("_Shelves");
        shelfContainer = shelves.GetComponent<ShelfContainer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnItems(JsonData levelInfo, ItemSlug[] itemSprites)
    {
        JsonData itemInfo = levelInfo["items"];
        Debug.Log(itemInfo.Count);
        GameObject[] items;
        items = new GameObject[itemInfo.Count];

        for (int i=0; i<itemInfo.Count; ++i)
        {
            GameObject tmpGameObj = (GameObject)Instantiate(item, Vector3.zero, Quaternion.identity);
            tmpGameObj.transform.parent = this.gameObject.transform;

            SpriteRenderer tmpSpriteRender = tmpGameObj.GetComponent<SpriteRenderer>();
            Item tmpObjItem = tmpGameObj.GetComponent<Item>();
            Sprite to_load = null;
            foreach (ItemSlug slug in itemSprites)
            {
                if (slug.item_name == itemInfo[i]["item_name"].ToString())
                {
                    to_load = slug.item_image;
                }
            }
            tmpSpriteRender.sprite = to_load;
            tmpGameObj.AddComponent<BoxCollider2D>();

            tmpObjItem.itemName = itemInfo[i]["item_name"].ToString();
            tmpObjItem.price = int.Parse(itemInfo[i]["item_price"].ToString());
            tmpObjItem.percentage = int.Parse(itemInfo[i]["percentage"].ToString());

            items[i] = tmpGameObj;
        }
        shelfContainer.ArrangeItems(items, 2);
    }
}
