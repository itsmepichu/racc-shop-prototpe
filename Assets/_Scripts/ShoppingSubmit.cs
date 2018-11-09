using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingSubmit : MonoBehaviour {

    [SerializeField]
    private GameManager game_manager;

	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        game_manager.IsShoppingDone();
    }
}
