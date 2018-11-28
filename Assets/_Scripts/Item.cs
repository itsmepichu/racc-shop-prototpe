using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string itemName;
    public int 
        price, 
        percentage,
        thisItemNo;    // an item's index to keep track of its position when placed inside the basket
    public bool 
        isItemSelected,
        didTouchBasket,
        canRemoveFromBasket;
    public float 
        scaleUpRatio = 0.2f,
        scaleDownRatio = 0.085f;

    [SerializeField]
    private SpriteRenderer sprite_renderer;

    [SerializeField]
    private Vector3 initalPos;

    [SerializeField]
    private GameObject placeHoldingItem;

    [SerializeField]
    private GameManager game_manager;

    [SerializeField]
    private BasketManager basket_manager;

    [SerializeField]
    private ListChecker list_checker, list_checker_2;

    [SerializeField]
    private AudioManager audio_manager;

    [SerializeField]
    private AnimationManager animation_manager;

    // Use this for initialization
    void Start () {
        sprite_renderer = GetComponent<SpriteRenderer>();
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();
        basket_manager = GameObject.Find("_BasketSprite").GetComponent<BasketManager>();
        audio_manager = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        animation_manager = GameObject.Find("_AnimationPlayer").GetComponent<AnimationManager>();

        isItemSelected = false;
        didTouchBasket = false;
        canRemoveFromBasket = false;
        initalPos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        ItemMovementController_V2();
    }

    private void OnMouseDown()
    {
        if (!placeHoldingItem)
        {
            // Implementing something like infinite fruits in shelf
            placeHoldingItem = (GameObject)Instantiate(this.transform.gameObject, initalPos, Quaternion.identity);
            placeHoldingItem.transform.parent = this.transform.parent;
        }
        if(!didTouchBasket)
        {
            this.thisItemNo = basket_manager.itemsInBasket;
        }
        isItemSelected = true;
        audio_manager.PlaySFX("tap");
    }

    private void OnMouseDrag()
    {
        if((!didTouchBasket || canRemoveFromBasket) && isItemSelected)
        {
            sprite_renderer.sortingOrder = 10; // just giving some random high number
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
        }
    }

    private void OnMouseUp()
    {
        isItemSelected = false;
        if (!didTouchBasket)    // place the fruit back into the shelf 
        {
            sprite_renderer.sortingOrder = 1;
        }
    }

    IEnumerator DestroyPlaceHolders()
    {
        yield return new WaitForSeconds(5);
        Destroy(placeHoldingItem);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ADDING THE ITEM TO BASKET
        if (collision.gameObject.name == "_BasketSprite" && !didTouchBasket)
        {
            if(game_manager.current_total + this.price <= game_manager.total_budget)    // Doing A Budget Check
            {
                if(game_manager.current_percentage + this.percentage <= game_manager.percentage_threshold)  // Doing A Food Needed Percentage Check
                {
                    didTouchBasket = true;
                    audio_manager.PlaySFX("basket_value");
                    basket_manager.addItemToBasket(this.itemName, this.thisItemNo);
                    game_manager.setCurrentScore(this.price, this.percentage, "add");
                    isItemSelected = false;
                    canRemoveFromBasket = true;
                    if(game_manager.current_level == 1)
                    {
                        list_checker = GameObject.Find("_ListMenu").GetComponent<ListChecker>();
                        list_checker.CheckList();
                    }
                    else if(game_manager.current_level == 2)
                    {
                        list_checker_2 = GameObject.Find("_ListMenu_2").GetComponent<ListChecker>();
                        list_checker_2.CheckList();
                    }
                } else
                {
                    animation_manager.PlayBabyRacconAnimation();
                    isItemSelected = false;
                    audio_manager.PlayDialouge("too_much_food");
                }
            } else
            {
                animation_manager.PlayBudgetShakeAnimation();
                isItemSelected = false;
                audio_manager.PlaySFX("over_budget");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // REMOVING THE ITEM FROM BASKET
        if (collision.gameObject.name == "_BasketSprite" && didTouchBasket)
        {
            didTouchBasket = false;
            canRemoveFromBasket = false;
            basket_manager.removeItemFromBasket(thisItemNo, itemName);
            game_manager.setCurrentScore(this.price, this.percentage, "remove");
            if (game_manager.current_level == 1)
            {
                list_checker = GameObject.Find("_ListMenu").GetComponent<ListChecker>();
                list_checker.CheckList();
            }
            else if (game_manager.current_level == 2)
            {
                list_checker_2 = GameObject.Find("_ListMenu_2").GetComponent<ListChecker>();
                list_checker_2.CheckList();
            }
        }
    }

    private void ItemMovementController_V2()
    {
        if(!didTouchBasket) // actions that are happening to the fruit outside the basket
        {
            // make fruit bigger on mouse down
            if (isItemSelected && this.transform.localScale.x <= 1.4f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + scaleUpRatio, this.transform.localScale.y + scaleUpRatio, this.transform.localScale.z);
            }

            // make fruit smaller on mouse up
            if (!didTouchBasket && !isItemSelected && this.transform.localScale.x >= 1.0f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - scaleDownRatio, this.transform.localScale.y - scaleDownRatio, this.transform.localScale.z);
            }

            // move fruit back to shelf on mouse up
            if (!isItemSelected)
            {
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, initalPos, step);
            }
        }
        else // actions that are happening when fruit is inside the basket
        {
            float step = 10.0f * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, basket_manager.getNextPosInBasket(thisItemNo), step);
        }
    }
}
