using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class PaymentManager : MonoBehaviour {

    [SerializeField]
    private GameManager game_manager;

    [SerializeField]
    private TextMesh
        total_bill,
        total_paid;

    [SerializeField]
    private GameObject
        note_5,
        coin_2,
        coin_1;


	// Use this for initialization
	void Start () {
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();
        total_bill = GameObject.Find("_TotalBill").GetComponent<TextMesh>();
        total_paid = GameObject.Find("_TotalPaid").GetComponent<TextMesh>();

        total_bill.text = game_manager.current_total.ToString();
        total_paid.text = "0";
    }

    // Update is called once per frame
    void Update () {
        if(total_bill.text == "0")
        {
            total_bill.text = game_manager.current_total.ToString();
        }
    }

    public void SpawnMoney(JsonData LevelData)
    {
        JsonData money_info = LevelData["money"];
        for(int i=0; i < money_info.Count; ++i)
        {
            string money_name = money_info[i]["money_name"].ToString();
            switch (money_name)
            {
                case "Note_5":
                    for(int x=0; x < int.Parse(money_info[i]["numbers"].ToString()); ++x)
                    {
                        GameObject tmpGameObj = Instantiate(note_5, note_5.transform.position, Quaternion.identity);
                        tmpGameObj.transform.parent = this.gameObject.transform;
                        Money tmpMoneyScript = tmpGameObj.AddComponent<Money>();
                        tmpMoneyScript.name = money_name;
                        tmpMoneyScript.value = int.Parse(money_info[i]["value"].ToString());
                    }
                    break;
                case "Coin_2":
                    for(int x=0; x < int.Parse(money_info[i]["numbers"].ToString()); ++x)
                    {
                        GameObject tmpGameObj = Instantiate(coin_2, coin_2.transform.position, Quaternion.identity);
                        tmpGameObj.transform.parent = this.gameObject.transform;
                        Money tmpMoneyScript = tmpGameObj.AddComponent<Money>();
                        tmpMoneyScript.name = money_name;
                        tmpMoneyScript.value = int.Parse(money_info[i]["value"].ToString());
                    }
                    break;
                case "Coin_1":
                    for(int x=0; x < int.Parse(money_info[i]["numbers"].ToString()); ++x)
                    {
                        GameObject tmpGameObj = Instantiate(coin_1, coin_1.transform.position, Quaternion.identity);
                        tmpGameObj.transform.parent = this.gameObject.transform;
                        Money tmpMoneyScript = tmpGameObj.AddComponent<Money>();
                        SpriteRenderer tmpSpriteRenderer = tmpGameObj.GetComponent<SpriteRenderer>();
                        tmpSpriteRenderer.sortingOrder += x;
                        tmpMoneyScript.name = money_name;
                        tmpMoneyScript.value = int.Parse(money_info[i]["value"].ToString());
                    }
                    break;
            }
        }
    }
}
