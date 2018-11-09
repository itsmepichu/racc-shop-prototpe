using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerItem : MonoBehaviour
{

    public Vector3 
        initPos, 
        newPosToMove;
    public float
        scaleUpRatio = 0.2f,
        scaleDownRatio = 0.085f;

    public bool
        is_item_selected,
        did_touch_scanner,
        in_basket,
        move_this_item;

    [SerializeField]
    private AudioManager audio_manager;

    [SerializeField]
    private ScanManager scan_manager;

    [SerializeField]
    private SpriteRenderer sprite_renderer;

    // Use this for initialization
    void Start()
    {
        initPos = this.gameObject.transform.position;

        is_item_selected = false;
        did_touch_scanner = false;
        move_this_item = false;
        in_basket = false;

        sprite_renderer = this.gameObject.GetComponent<SpriteRenderer>();
        audio_manager = GameObject.Find("_AudioManager").GetComponent<AudioManager>();
        scan_manager = FindObjectOfType<ScanManager>();
    }

    // Update is called once per frame
    void Update()
    {
        itemMoveController();
    }

    private void OnMouseDown()
    {
        is_item_selected = true;
        audio_manager.PlaySFX("tap");
    }

    private void OnMouseUp()
    {
        is_item_selected = false;
    }

    private void OnMouseDrag()
    {
        if (is_item_selected)
        {
            sprite_renderer.sortingOrder = 10; // just giving some random high number
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
        }
    }

    private void itemMoveController()
    {
        if (move_this_item)
        {
            float step = 10.0f * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPosToMove, step);

            if (this.transform.position.x >= (newPosToMove.x - 0.25f))
            {
                move_this_item = false;
            }

            initPos = this.transform.position;
            return;
        }
        if (!did_touch_scanner)
        {
            if (is_item_selected && this.transform.localScale.x <= 1.6f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + scaleUpRatio, this.transform.localScale.y + scaleUpRatio, this.transform.localScale.z);
            }
            if (!is_item_selected && this.transform.localScale.x >= 1.0f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - scaleDownRatio, this.transform.localScale.y - scaleDownRatio, this.transform.localScale.z);
            }
            if (!is_item_selected)
            {
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, initPos, step);
            }
        } else
        {
            if (this.transform.localScale.x >= 1.0f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - scaleDownRatio, this.transform.localScale.y - scaleDownRatio, this.transform.localScale.z);
            }
            if (this.transform.position.x <= 6.5f)
            {
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(6.5f, 0.0f, 0.0f), step);
            }
            if(this.transform.position.x >= 6.0f)
            {
                sprite_renderer.sortingOrder = -1;
                float step = 10.0f * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(6.7f, -4.5f, 0.0f), step);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Scanner" && is_item_selected)
        {
            Debug.Log("Touched Scanner");
            audio_manager.PlaySFX("tick");
            did_touch_scanner = true;
            is_item_selected = false;
            scan_manager.notifyNextMoveItems();
        }

        if(collision.gameObject.name == "BasketCover")
        {
            in_basket = true;
            scan_manager.CheckAllItemsInBasket();
        }
    }
}
