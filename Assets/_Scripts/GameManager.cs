using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System;

public class GameManager : MonoBehaviour {

    // Maintaining GameObjects here
    [SerializeField]
    private GameObject 
        mainMenuCanvas,
        introScreen,
        shoppingScreen,
        scannerScreen,
        paymentScreen,
        outroScreen,
        foodMeter,
        budget,
        totalSpent;

    public GameObject[] menu_buttons;

    public TextMesh 
        budgetText,
        paymentText,
        totalText;

    public SpriteRenderer cashRegisterSprite;

    [SerializeField]
    private SpriteSpawnManager sprite_spawn_manager;

    [SerializeField]
    private JsonData
        wholeLevelData,
        currentLevelData;

    // Maintining Score here
    public int
        current_level,
        total_budget,
        percentage_threshold,
        current_total,
        payment_done,
        current_percentage;

    public ItemSlug[] itemSprites;

    [SerializeField]
    private ListChecker 
        list_checker,
        list_checker_2;

    [SerializeField]
    private PaymentManager payment_manager;

    [SerializeField]
    private AudioManager audio_manager;

    [SerializeField]
    private ScanManager scan_manager;

    [SerializeField]
    private BasketManager basket_manager;

    [SerializeField]
    private AnimationManager animation_manager;

    private void Awake()
    {
        string jsonDataAsString = "{\r\n  \"Level1\": {\r\n    \"items\": [\r\n      {\r\n        \"item_name\": \"Apple - normal\",\r\n        \"item_price\": 1,\r\n        \"percentage\": 10\r\n      },\r\n      {\r\n        \"item_name\": \"Apple - fancy\",\r\n        \"item_price\": 3,\r\n        \"percentage\": 10\r\n      },\r\n      {\r\n        \"item_name\": \"Apple - green\",\r\n        \"item_price\": 1,\r\n        \"percentage\": 20\r\n      },\r\n      {\r\n        \"item_name\": \"Bananas\",\r\n        \"item_price\": 2,\r\n        \"percentage\": 20\r\n      }\r\n    ],\r\n\t\"money\": [\r\n\t  {\r\n\t    \"money_name\": \"Note_5\",\r\n\t\t\"value\": 5,\r\n\t\t\"numbers\": 1\r\n\t  },\r\n\t  {\r\n\t    \"money_name\": \"Coin_2\",\r\n\t\t\"value\": 2,\r\n\t\t\"numbers\": 1\r\n\t  },\r\n\t  {\r\n\t    \"money_name\": \"Coin_1\",\r\n\t\t\"value\": 1,\r\n\t\t\"numbers\": 3\r\n\t  }\r\n\t],\r\n\t\"budget\": 10,\r\n\t\"percentage_threshold\": 100\r\n  },\r\n  \"Level2\": {\r\n    \"items\": [\r\n      {\r\n        \"item_name\": \"Apple - normal\",\r\n        \"item_price\": 1,\r\n        \"percentage\": 5\r\n      },\r\n      {\r\n        \"item_name\": \"Apple - fancy\",\r\n        \"item_price\": 3,\r\n        \"percentage\": 5\r\n      },\r\n      {\r\n        \"item_name\": \"Apple - 3 for 2\",\r\n        \"item_price\": 2,\r\n        \"percentage\": 15\r\n      },\r\n      {\r\n        \"item_name\": \"Bananas\",\r\n        \"item_price\": 2,\r\n        \"percentage\": 20\r\n      },\r\n      {\r\n        \"item_name\": \"Cake\",\r\n        \"item_price\": 7,\r\n        \"percentage\": 5\r\n      },\r\n      {\r\n        \"item_name\": \"Watermelon\",\r\n        \"item_price\": 5,\r\n        \"percentage\": 30\r\n      }\r\n    ],\r\n\t\"money\": [\r\n\t  {\r\n\t    \"money_name\": \"Note_5\",\r\n\t\t\"value\": 5,\r\n\t\t\"numbers\": 2\r\n\t  },\r\n\t  {\r\n\t    \"money_name\": \"Coin_2\",\r\n\t\t\"value\": 2,\r\n\t\t\"numbers\": 1\r\n\t  },\r\n\t  {\r\n\t    \"money_name\": \"Coin_1\",\r\n\t\t\"value\": 1,\r\n\t\t\"numbers\": 3\r\n\t  }\r\n\t],\r\n\t\"budget\": 15,\r\n\t\"percentage_threshold\": 100\r\n  }\r\n}";
        wholeLevelData = JsonMapper.ToObject(jsonDataAsString);
        //string
        //    levelDataFileName = "level_data.json",
        //    levelDataFilePath = "";

        //switch (Application.platform)
        //{
        //    case RuntimePlatform.Android:
        //        StartCoroutine("LoadJsonOnAndroid", levelDataFileName);
        //        break;
        //    case RuntimePlatform.IPhonePlayer:
        //        levelDataFilePath = Application.dataPath + "/Raw/" + levelDataFileName;
        //        if (File.Exists(levelDataFilePath))
        //        {
        //            string jsonString = File.ReadAllText(levelDataFilePath);
        //            wholeLevelData = JsonMapper.ToObject(jsonString);
        //        }
        //        else
        //        {
        //            Debug.LogError("Level Data JSON File Not Found!");
        //        }
        //        break;
        //    default:
        //        levelDataFilePath = Application.dataPath + "/StreamingAssets/" + levelDataFileName;
        //        if (File.Exists(levelDataFilePath))
        //        {
        //            string jsonString = File.ReadAllText(levelDataFilePath);
        //            wholeLevelData = JsonMapper.ToObject(jsonString);
        //        }
        //        else
        //        {
        //            Debug.LogError("Level Data JSON File Not Found!");
        //        }
        //        break;
        //}
        //levelDataFilePath = Path.Combine(Application.streamingAssetsPath, levelDataFileName);
        //Debug.Log(Application.streamingAssetsPath);
        //Debug.Log(levelDataFilePath);
        //Debug.Log(File.Exists(levelDataFilePath));
    }

    public IEnumerator LoadJsonOnAndroid(string fileName)
    {
        string filePath;
        filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
        string dataAsJson;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            yield return www.Send();
            dataAsJson = www.downloadHandler.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
            wholeLevelData = JsonMapper.ToObject(dataAsJson);
        }
    }

    // Use this for initialization
    void Start () {
        mainMenuCanvas = GameObject.Find("_UI");
        Debug.Log("1111111111111111111111111111111111111111111111111111");
        introScreen = GameObject.Find("_Intro_Screen");
        shoppingScreen = GameObject.Find("_Shopping_Screen");
        scannerScreen = GameObject.Find("_Scanner_Screen");
        paymentScreen = GameObject.Find("_Payment_Screen");
        outroScreen = GameObject.Find("_Outro_Screen");
        budget = GameObject.Find("_BudgetSprite");
        totalSpent = GameObject.Find("_TotalAmoutSprite");
        menu_buttons = GameObject.FindGameObjectsWithTag("MenuButton");
        foodMeter = GameObject.FindGameObjectWithTag("FoodMeter");
        Debug.Log("3333333333333333333333333333333333333333333333333333");


        sprite_spawn_manager = shoppingScreen.GetComponent<SpriteSpawnManager>();
        audio_manager = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        animation_manager = GameObject.Find("_AnimationPlayer").GetComponent<AnimationManager>();
        cashRegisterSprite = GameObject.Find("_CashRegisterSprite").GetComponent<SpriteRenderer>();
        scan_manager = scannerScreen.GetComponent<ScanManager>();
        basket_manager = FindObjectOfType<BasketManager>();
        budgetText = budget.GetComponentInChildren<TextMesh>();
        totalText = totalSpent.GetComponentInChildren<TextMesh>();
        paymentText = GameObject.Find("_TotalPaid").GetComponent<TextMesh>();
        payment_manager = paymentScreen.GetComponent<PaymentManager>();
        list_checker = GameObject.Find("_ListMenu").GetComponent<ListChecker>();
        list_checker_2 = GameObject.Find("_ListMenu_2").GetComponent<ListChecker>();
        Debug.Log("2222222222222222222222222222222222222222222222222222");


        itemSprites = scan_manager.itemSprites;
        StartGame();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartGame()
    {
        Debug.Log("StartGameCalled............");
        mainMenuCanvas.SetActive(true);
        foodMeter.SetActive(false);
        shoppingScreen.SetActive(false);
        scannerScreen.SetActive(false);
        paymentScreen.SetActive(false);
        introScreen.SetActive(false);
        outroScreen.SetActive(false);
        Debug.Log(foodMeter.activeInHierarchy);
    }

    public void DisableMenu()
    {
        //mainMenuCanvas.SetActive(false);
        foodMeter.SetActive(false);
        foreach(GameObject button in menu_buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void EnableMenu()
    {
        //mainMenuCanvas.SetActive(false);
        foreach (GameObject button in menu_buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
    public void LoadLevel1 ()
    {
        DisableMenu();
        LoadLevelData(1);
    }

    public void LoadLevel2 ()
    {
        DisableMenu();
        LoadLevelData(2);
    }

    public void EnableIntroScene()
    {
        introScreen.SetActive(true);
        audio_manager.PlayDialouge("intro");
        StartCoroutine(LoadLevel_2_ShoppingScreen());
    }

    public void clearClones(string Tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(Tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    private IEnumerator LoadLevel_2_ShoppingScreen()
    {
        yield return new WaitForSeconds(5.5f);
        list_checker_2.gameObject.SetActive(true);
        EnableShoppingScreen();
    }

    public void DisableShoppingScreen()
    {
        shoppingScreen.SetActive(false);
    }

    public void EnableShoppingScreen()
    {
        introScreen.SetActive(false);
        paymentScreen.SetActive(false);
        shoppingScreen.SetActive(true);
        if(current_level == 2)
        {
            foodMeter.SetActive(true);
        }
    }

    public void EnablePaymentScreen()
    {
        shoppingScreen.SetActive(false);
        scannerScreen.SetActive(false);
        paymentScreen.SetActive(true);
    }

    public void EnableScannerScreen()
    {
        foodMeter.SetActive(false);
        shoppingScreen.SetActive(false);
        paymentScreen.SetActive(false);
        scannerScreen.SetActive(true);
    }

    public void DisableScannerScreen()
    {
        foodMeter.SetActive(true);
        shoppingScreen.SetActive(true);
        paymentScreen.SetActive(false);
        scannerScreen.SetActive(false);
    }

    public void DisableoutroScreen()
    {
        outroScreen.SetActive(false);
    }

    public void EnableOutroScreen()
    {
        paymentScreen.SetActive(false);
        outroScreen.SetActive(true);
    }

    public void LoadScannerScreen()
    {
        scan_manager.spawnItems(basket_manager.items);
        EnableScannerScreen();
    }

    public void LoadPaymentScreen()
    {
        Debug.Log("All conditions met. Loading Payment Screen");
        // First initiatilizing all sprites before showing the screen
        payment_manager.SpawnMoney(currentLevelData);
        EnablePaymentScreen();
    }

    public void ResetPaymentScores()
    {
        payment_done = 0;
        paymentText.text = "0";
    }

    public void LoadLevelData(int level)
    {
        Debug.Log("Current Level: " + level);
        current_level = level;
        currentLevelData = wholeLevelData["Level" + level];
        sprite_spawn_manager.spawnItems(currentLevelData, itemSprites);
        budgetText.text = currentLevelData["budget"].ToString();
        total_budget = int.Parse(currentLevelData["budget"].ToString());
        percentage_threshold = int.Parse(currentLevelData["percentage_threshold"].ToString());
        current_total = 0;
        current_percentage = 0;
        if(level == 1)
        {
            list_checker_2.gameObject.SetActive(false);
            EnableShoppingScreen();
        }
        else if(level == 2)
        {
            list_checker.gameObject.SetActive(false);
            EnableIntroScene();
            //EnableShoppingScreen();
        }
    }

    public void setCurrentScore(int price, int percentage, string methodType)
    {
        switch (methodType)
        {
            case "add":
                current_total += price;
                current_percentage += percentage;
                totalText.text = current_total.ToString();
                break;

            case "remove":
                current_total -= price;
                current_percentage -= percentage;
                totalText.text = current_total.ToString();
                break;
        }
        this.updateSlider();
    }

    public void updatePaymentBill(int money, string methodType)
    {
        switch (methodType)
        {
            case "add":
                payment_done += money;
                paymentText.text = payment_done.ToString();
                Debug.Log("Current Total: " + current_total + " -> Payment Done: " + payment_done);
                break;

            case "remove":
                payment_done -= money;
                paymentText.text = payment_done.ToString();
                break;
        }
        if (current_total == payment_done)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver!!!!!");
        if(current_level == 1)
        {
            audio_manager.PlaySFX("game_over");
            StartCoroutine(ReloadGame(2.0f));
        } 
        else if(current_level == 2)
        {
            StartCoroutine(HandleEndScreen());
            //StartCoroutine(ReloadGame(5.5f));
        }
    }

    public IEnumerator ReloadGame(float delay=2.0f)
    {
        Debug.Log("Reload Game Called!!!");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("game");
    }

    public IEnumerator HandleEndScreen()
    {
        yield return new WaitForSeconds(0.25f);
        audio_manager.PlaySFX("game_over");

        yield return new WaitForSeconds(1.0f);
        EnableOutroScreen();

        yield return new WaitForSeconds(0.25f);
        audio_manager.PlayDialouge("outro");
    }

    public void IsShoppingDone()
    {
        Debug.Log("Shopping Is Done!!!");

        // Checking if game win conditions are met
        //no need to check here because we are handling this in the class Item.cs
        //if(current_total > total_budget)  //no need to check here because we are handling this in the class Item.cs
        //{
        //    Debug.Log("Over budget!!!");
        //    return;
        //}

        //if (current_percentage > (percentage_threshold + 10))
        //{
        //    Debug.Log("This is too much for me to eat!");
        //    return;
        //}
        
        if(current_percentage < percentage_threshold)
        {
            animation_manager.PlayBabyRacconAnimation();
            audio_manager.PlayDialouge("not_enough_food");
            return;
        }

        if(current_level == 1)
        {
            if (!list_checker.is_apple_in_basket || !list_checker.is_banana_in_basket)
            {
                animation_manager.PlayListShakeAnimation();
                audio_manager.PlaySFX("over_budget");
                Debug.Log("Item missing from list!!!");
                return;
            }
        }
        else if(current_level == 2)
        {
            if (!list_checker_2.is_apple_in_basket || !list_checker_2.is_banana_in_basket || !list_checker_2.is_watermelon_in_basket)
            {
                animation_manager.PlayListShakeAnimation();
                audio_manager.PlaySFX("over_budget");
                Debug.Log("Item missing from list!!!");
                return;
            }
        }

        if(current_level == 1)
        {
            LoadPaymentScreen();
        } 
        else if(current_level == 2)
        {
            LoadScannerScreen();
        }
    }

    public void updateSlider()
    {
        Slider food_slider = foodMeter.GetComponent<Slider>();
        food_slider.value = current_percentage;
        if(current_percentage >= percentage_threshold)
        {
            cashRegisterSprite.color = Color.white;
        } else
        {
            cashRegisterSprite.color = Color.grey;
        }
    }

    public JsonData GetLevelInfo()
    {
        return currentLevelData;
    }
}
