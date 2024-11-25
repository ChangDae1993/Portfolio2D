using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    private Image sk2_coolImg;
    private GameObject player;
    private Player_Input p_Input;
    private Player_State_Ctrlr p_State;
    private Player_Block p_Block;
    private Player_Walk p_Walk;
    private Skill_Cool_Ctrlr sk_Cool;
    [SerializeField] private Camera_Ctrlr cam_Ctrl;

    public bool shieldDash;
    [SerializeField] private float sDashTime;
    [SerializeField] private float sDashTimer;

    // Start is called before the first frame update
    void Start()
    {
        p_Input = GetComponentInParent<Player_Input>();
        p_State = GetComponentInParent<Player_State_Ctrlr>();
        p_Block = GetComponentInParent<Player_Block>();
        p_Walk = GetComponentInParent<Player_Walk>();

        shieldDash = false;
        sDashTime = 0.2f;
        sDashTimer = sDashTime;

        sk_Cool = GameObject.Find("Skill_Cool_CTRLR").GetComponent<Skill_Cool_Ctrlr>();
    }

    // Update is called once per frame
    void Update()
    {

        if (p_Input.skill2On && sk_Cool.skill2Able == true)
        {
            shieldDash = true;
        }

        if (sDashTimer >= 0.0f && shieldDash)
        {
            sDashTimer -= Time.deltaTime;
            ShieldDash();
        }

        if (sDashTimer < 0.0f)
        {
            p_State.p_Defece_state = PlayerDefenceState.player_onShield;
            shieldDash = false;
            sDashTimer = sDashTime;
        }

    }
    #region Shield Dash
    public void ShieldDash()
    {
        cam_Ctrl = Camera.main.GetComponent<Camera_Ctrlr>();
        SoundMgr.Instance.PlayEffSound("Shield_Dash", 0.7f);
        //Debug.Log("Shield Dash");
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && shieldDash)
        {
            if (cam_Ctrl != null)
            {
                cam_Ctrl.CamShake();
            }
            
            SoundMgr.Instance.PlayEffSound("Shield_Dash_Impact", 1.0f);

            sDashTimer = -1.0f;
            collision.gameObject.GetComponent<Enemy>().M_Stun();
            collision.gameObject.GetComponent<Enemy>().M_Hit(10.0f);
            shieldDash = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && shieldDash)
        {
            if (cam_Ctrl != null)
            {
                cam_Ctrl.CamShake();
            }

            SoundMgr.Instance.PlayEffSound("Shield_Dash_Impact", 1.0f);
            sDashTimer = -1.0f;
            collision.gameObject.GetComponent<Enemy>().M_Stun();
            collision.gameObject.GetComponent<Enemy>().M_Hit(10.0f);
            shieldDash = false;
        }
    }
}
