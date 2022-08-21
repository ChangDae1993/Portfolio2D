using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShieldState
{
    shieldOn,
    shieldOff,
    shieldBreak,
    shieldRecharge
}

public class Player_Block : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Player_State_Ctrlr Player_State;
    public GameObject def_area;
    Animator animator;

    //쉴드 상태일때 scale 값 받아두기
    public float Shield_scale;
    public ShieldState shieldState;

    public Image shield_Dur;
    public Image shield_Recharge;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrlr>();
        Player_State.p_state = PlayerState.player_idle;
        Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
        shield_Dur.gameObject.SetActive(false);
        shield_Recharge.gameObject.SetActive(false);
        shieldState = ShieldState.shieldOff;
    }

    // Update is called once per frame
    void Update()
    {
        if(shield_Dur.fillAmount <= 0.0f)
        {
            shield_Dur.gameObject.SetActive(false);
            shield_Dur.fillAmount = 1.0f;
            shieldState = ShieldState.shieldBreak;
        }

        if (shieldState == ShieldState.shieldBreak)
        {
            SoundMgr.Instance.PlayEffSound("Shield_Break", 1.0f);
            shieldState = ShieldState.shieldRecharge;

        }
        else if(shieldState == ShieldState.shieldRecharge)
        {
            shield_Recharge.gameObject.SetActive(true);
            shield_Recharge.fillAmount += Time.deltaTime * 0.3f;
            def_area.gameObject.SetActive(false);
            Player_State.p_Defece_state = PlayerDefenceState.player_noShield;

            if(shield_Recharge.fillAmount >= 1.0f)
            {
                shieldState = ShieldState.shieldOff;
                shield_Recharge.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(1) && shieldState != ShieldState.shieldRecharge)
        {
            shield_Recharge.fillAmount = 0.0f;
            shieldState = ShieldState.shieldOn;
            shield_Dur.gameObject.SetActive(true);
            Player_State.p_state = PlayerState.player_Shield;
            Player_State.p_Defece_state = PlayerDefenceState.player_onShield;
            def_area.gameObject.SetActive(true);
            SoundMgr.Instance.PlayEffSound("shield_on", 0.5f);
            Shield_scale = this.transform.localScale.x;
        }

        if (Input.GetMouseButtonUp(1) && shieldState != ShieldState.shieldRecharge)
        {
            shieldState = ShieldState.shieldOff;
            shield_Dur.gameObject.SetActive(false);
            Player_State.p_state = PlayerState.player_idle;
            Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
            SoundMgr.Instance.PlayEffSound("shield_off", 0.4f);
            def_area.gameObject.SetActive(false);
        }
    }
}
