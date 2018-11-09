using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour {

    [SerializeField]
    private GameManager game_manager;

    public string screen_name;

	// Use this for initialization
	void Start () {
        game_manager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        int current_level = game_manager.current_level;
        switch (screen_name)
        {
            case "shopping":
                if(current_level == 1)
                {
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Food");
                    foreach (GameObject obj in objects)
                    {
                        Destroy(obj);
                    }
                    game_manager.StartGame();
                    game_manager.EnableMenu();
                }
                if(current_level == 2)
                {
                    game_manager.ReloadGame(0.5f);
                }
                break;

            case "payment":
                if (current_level == 1)
                {
                    game_manager.payment_done = 0;
                    game_manager.paymentText.text = "0";
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Money");
                    foreach(GameObject obj in objects)
                    {
                        Destroy(obj);
                    }
                    game_manager.EnableShoppingScreen();
                }
                if (current_level == 2)
                {
                    game_manager.payment_done = 0;
                    game_manager.paymentText.text = "0";
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Money");
                    foreach(GameObject obj in objects)
                    {
                        Destroy(obj);
                    }
                    game_manager.EnableScannerScreen();
                }
                break;

            default:
                StartCoroutine(game_manager.ReloadGame(0.5f));
                break;
        }
    }
}
