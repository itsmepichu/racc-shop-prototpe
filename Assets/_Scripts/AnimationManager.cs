using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public GameObject baby_raccon_sprite;
    public bool is_anim_playing;

    public Animator 
        list_animator,
        budget_animator;

    [SerializeField]
    private GameManager game_manager;
	
    // Use this for initialization
	void Start () {
        is_anim_playing = false;
        game_manager = GameObject.Find("_GameManager").GetComponent<GameManager>();  
        budget_animator = GameObject.Find("_BudgetSprite").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBabyRacconAnimation()
    {
        if(!is_anim_playing)
        {
            GameObject tmpGameObj = (GameObject)Instantiate(baby_raccon_sprite, baby_raccon_sprite.transform.position, Quaternion.identity);
            is_anim_playing = true;
            StartCoroutine(DestroyTmpObj(tmpGameObj));
        }
    }

    private IEnumerator DestroyTmpObj(GameObject obj)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(obj);
        is_anim_playing = false;
    }

    public void PlayListShakeAnimation()
    {
        if (game_manager.current_level == 1)
        {
            list_animator = GameObject.Find("_ListMenu").GetComponent<Animator>();
        }
        else if (game_manager.current_level == 2)
        {
            list_animator = GameObject.Find("_ListMenu_2").GetComponent<Animator>();
        }
        list_animator.Play("sprite_shake_list", -1, 0.0f);
    }

    public void PlayBudgetShakeAnimation()
    {
        budget_animator.Play("sprite_shake_budget", -1, 0.0f);
    }

}
