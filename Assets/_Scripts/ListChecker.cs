using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListChecker : MonoBehaviour {

    public int belongs_to_scene;

    public bool
        is_banana_in_basket,
        is_apple_in_basket,
        is_watermelon_in_basket;

    [SerializeField]
    private GameObject
        banana_tick,
        apple_tick,
        banana_tick_2,
        apple_tick_2,
        watermelon_tick_2;

    [SerializeField]
    private BasketManager basket_manager;

    [SerializeField]
    private AudioManager audio_manager;

    [SerializeField]
    private GameManager game_manager;


    // Use this for initialization
    void Start () {
        banana_tick = GameObject.Find("_Banana_Tick");
        apple_tick = GameObject.Find("_Apple_Tick");
        banana_tick_2 = GameObject.Find("_Banana_Tick_2");
        apple_tick_2 = GameObject.Find("_Apple_Tick_2");
        watermelon_tick_2 = GameObject.Find("_Watermelon_Tick_2");
        basket_manager = GameObject.Find("_BasketSprite").GetComponent<BasketManager>();
        audio_manager = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();

        is_banana_in_basket = false;
        is_apple_in_basket = false;
        is_watermelon_in_basket = false;

        if(belongs_to_scene == 1)
        {
            banana_tick.SetActive(is_banana_in_basket);
            apple_tick.SetActive(is_apple_in_basket);
        }

        if(belongs_to_scene ==2)
        {
            apple_tick_2.SetActive(is_apple_in_basket);
            banana_tick_2.SetActive(is_banana_in_basket);
            watermelon_tick_2.SetActive(is_watermelon_in_basket);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void CheckList()
    {
        if(game_manager.current_level == 1)
        {
            is_banana_in_basket = false;
            is_apple_in_basket = false;
            for (int i = 0; i < basket_manager.items.Length; ++i)
            {
                if (basket_manager.items[i] == "Apple - normal" || basket_manager.items[i] == "Apple - green")
                {
                    is_apple_in_basket = true;
                    if (!apple_tick.activeSelf)
                    {
                        audio_manager.PlaySFX("tick");
                    }
                }
                if (basket_manager.items[i] == "Bananas")
                {
                    is_banana_in_basket = true;
                    if (!banana_tick.activeSelf)
                    {
                        audio_manager.PlaySFX("tick");
                    }
                }
            }
            banana_tick.SetActive(is_banana_in_basket);
            apple_tick.SetActive(is_apple_in_basket);
        }
        else if(game_manager.current_level == 2)
        {
            is_banana_in_basket = false;
            is_apple_in_basket = false;
            is_watermelon_in_basket = false;
            for (int i = 0; i < basket_manager.items.Length; ++i)
            {
                if (basket_manager.items[i] == "Apple - normal" || basket_manager.items[i] == "Apple - green" || basket_manager.items[i] == "Apple - 3 for 2")
                {
                    is_apple_in_basket = true;
                    if (!apple_tick_2.activeSelf)
                    {
                        audio_manager.PlaySFX("tick");
                    }
                }
                if (basket_manager.items[i] == "Bananas")
                {
                    is_banana_in_basket = true;
                    if (!banana_tick_2.activeSelf)
                    {
                        audio_manager.PlaySFX("tick");
                    }
                }
                if (basket_manager.items[i] == "Watermelon")
                {
                    is_watermelon_in_basket = true;
                    if (!watermelon_tick_2.activeSelf)
                    {
                        audio_manager.PlaySFX("tick");
                    }
                }
            }
            banana_tick_2.SetActive(is_banana_in_basket);
            apple_tick_2.SetActive(is_apple_in_basket);
            watermelon_tick_2.SetActive(is_watermelon_in_basket);
        }
    }
}
