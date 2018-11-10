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
        Debug.Log("current_level: " + current_level);
        Debug.Log("screen_name: " + screen_name);
        switch (screen_name)
        {
            case "shopping":
                if(current_level == 1)
                {
                    game_manager.clearClones("Food");
                    game_manager.clearClones("PriceTag");
                    game_manager.cashRegisterSprite.color = Color.grey;
                    game_manager.StartGame();
                    game_manager.EnableMenu();
                }
                if(current_level == 2)
                {
                    //StartCoroutine(game_manager.ReloadGame(0.25f));
                    game_manager.clearClones("Food");
                    game_manager.clearClones("PriceTag");
                    game_manager.cashRegisterSprite.color = Color.grey;
                    game_manager.DisableShoppingScreen();
                    game_manager.LoadLevel2();
                }
                break;

            case "payment":
                if (current_level == 1)
                {
                    game_manager.ResetPaymentScores();
                    game_manager.clearClones("Money");
                    game_manager.EnableShoppingScreen();
                }
                if (current_level == 2)
                {
                    game_manager.ResetPaymentScores();
                    game_manager.clearClones("Money");
                    game_manager.clearClones("ScannerItem");
                    game_manager.LoadScannerScreen();
                }
                break;

            case "scanner":
                game_manager.clearClones("ScannerItem");
                game_manager.DisableScannerScreen();
                break;

            case "outro":
                StartCoroutine(game_manager.ReloadGame(0.5f));
                break;

            default:
                StartCoroutine(game_manager.ReloadGame(0.5f));
                break;
        }
    }
}
