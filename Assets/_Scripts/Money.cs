using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    public string name;
    public int value;

    [SerializeField]
    private SpriteRenderer sprite_renderer;

    [SerializeField]
    private Vector2 initPos;

    [SerializeField]
    private Transform handSpriteTransform;

    [SerializeField]
    private GameManager game_manager;

    [SerializeField]
    private AudioManager audio_manager;

    public bool
        isMoneySelected,
        isMoneyInHand,
        isMoneyInWallet;

    public float
        scaleUpRatio = 0.2f,
        scaleDownRatio = 0.085f;

    // Use this for initialization
    void Start () {
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        initPos = this.transform.position;
        isMoneyInWallet = true;
        isMoneyInHand = false;
        isMoneySelected = false;
        handSpriteTransform = GameObject.Find("_Hand").GetComponent<Transform>();
        audio_manager = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update () {
        MoneyMovementController();
	}

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown......");
        isMoneySelected = true;
        audio_manager.PlaySFX("tap");
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp......");
        isMoneySelected = false;
    }

    private void OnMouseDrag()
    {
        if(isMoneySelected)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter: " + collision.gameObject.name);
        if (isMoneySelected)
        {
            if (collision.gameObject.name == "_Hand")
            {
                isMoneyInHand = true;
                isMoneyInWallet = false;
                game_manager.updatePaymentBill(this.value, "add");
            }
            if (collision.gameObject.name == "Wallet")
            {
                isMoneyInWallet = true;
                isMoneyInHand = false;
                game_manager.updatePaymentBill(this.value, "remove");
            }
        }
        isMoneySelected = false;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("OnTriggerExit: " + collision.gameObject.name);
    //    if(isMoneySelected)
    //    {
    //        if (collision.gameObject.name == "_Hand")
    //        {
    //            isMoneyInHand = false;
    //            isMoneyInWallet = true;
    //            game_manager.updatePaymentBill(this.value, "remove");
    //        }
    //        if (collision.gameObject.name == "Wallet")
    //        {
    //            isMoneyInWallet = false;
    //            isMoneyInHand = true;
    //            game_manager.updatePaymentBill(this.value, "add");
    //        }
    //    }
    //    isMoneySelected = false;
    //}

    private void MoneyMovementController()
    {
        // make Money bigger on mouse down
        if (isMoneySelected && this.transform.localScale.x <= 1.4f)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + scaleUpRatio, this.transform.localScale.y + scaleUpRatio, this.transform.localScale.z);
        }

        // make Money smaller on mouse up or movement between hand and wallet
        if (!isMoneySelected && this.transform.localScale.x >= 1.0f)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x - scaleDownRatio, this.transform.localScale.y - scaleDownRatio, this.transform.localScale.z);
        }

        if(!isMoneySelected)
        {
            if (isMoneyInWallet)
            {
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, initPos, step);
            }

            if (isMoneyInHand)
            {
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, handSpriteTransform.position, step);
            }
        }
    }
}
