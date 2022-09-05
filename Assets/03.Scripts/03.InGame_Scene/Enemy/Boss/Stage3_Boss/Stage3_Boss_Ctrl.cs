using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3_Boss_Ctrl : Enemy
{
    public Image Hp_Img;
    public Image Hp2_Img;
    public Text Hp_Txt;

    private bool isDead;
    public Image ending_FadeOut;
    [SerializeField] private float endingAlpha;

    Vector2 targetPos = Vector2.zero;

    private float skill1Per;
    private float skill2Per;
    private int skill;
    [SerializeField] private bool isRevive;
    [SerializeField] private int skill1ActiveHp;
    [SerializeField] private int skill2ActiveHp;
    [SerializeField] private bool isSkill2;

    private void Awake()
    {
        InitData();
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        skill1Per = 40f;
        skill2Per = 5f;
        skill = Random.Range(1, 101);
        isRevive = false;
        skill1ActiveHp = 350;
        skill2ActiveHp = 600;
        isSkill2 = false;
        ending_FadeOut.color = new Color(0, 0, 0, 0);
        endingAlpha = 0.0f;
        isDead = false;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        Hp_Txt.text = CurHp.ToString() + "/" + MaxHp.ToString();
        if(!isRevive)
        {
            Hp_Img.fillAmount = CurHp / MaxHp;
        }
        else
        {
            Hp2_Img.fillAmount = CurHp / MaxHp;
        }
        chaseDist = Vector2.Distance(this.transform.position, player.transform.position);
        targetPos = new Vector2(player.transform.position.x + 3.0f, player.transform.position.y);
        M_ChaseDist();
        M_Patrol();
        //Debug.Log(patrol_Time);

        if (E_State.e_State == EnemyState.enemy_Death)
        {
            disTime -= Time.deltaTime;

            if (disTime <= 0.0f)
            {
                isDead = true;
            }
        }

        if(isDead)
        {
            endingAlpha += Time.deltaTime * 0.3f;
            ending_FadeOut.color = new Color(0, 0, 0, endingAlpha);

        }

        if(ending_FadeOut.color.a >= 1.0f)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    protected override void InitData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        P_TakeDam = player.GetComponent<Player_TakeDamage>();
        E_State = GetComponent<Enemy_State_Ctrlr>();
        animator = GetComponent<Animator>();
        patrol_Time = Random.Range(1.0f, 3.0f);
        pS = player.GetComponent<Player_State_Ctrlr>();

        MaxHp = 600;
        CurHp = MaxHp;
        e_move_Speed = 1.0f;
        e_Att = 15.0f;           //(임시)
        e_Att_Range = 20.0f;
        e_att_Range = 3.0f;
        right = Random.Range(0, 2);

        chaseDist = 0.0f;
        isChase = false;

        disTime = 3.0f;
    }

    protected override void M_Patrol()
    {
        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        if (E_State.e_State == EnemyState.enemy_Idle)
        {
            patrol_Time -= Time.deltaTime;
            if (patrol_Time <= 0.0f)
            {
                E_State.e_State = EnemyState.enemy_Patrol;
                patrol_Time = Random.Range(2.0f, 4.0f);
                right = Random.Range(0, 2);
            }
        }
        else if (E_State.e_State == EnemyState.enemy_Patrol)
        {
            if (patrol_Time >= 0.0f)
            {
                patrol_Time -= Time.deltaTime;
                animator.SetBool("IsPatrol", true);

                //랜덤한 방향으로 움직이기?
                //움직이는 방식은 this.transform.position += / -= Vector3.right * e_move_Speed * Time.deltaTime을 사용
                //움직이는 회전은 this.transform.localEulerAngles 사용
                if (right == 1)
                {
                    //1이니까 왼쪽
                    this.transform.position -= Vector3.right * e_move_Speed * Time.deltaTime;
                    this.transform.localEulerAngles = new Vector3(0, 0, 0);
                    //Debug.Log("Left");
                }
                else if (right == 0)
                {
                    //0이니까 오른쪽
                    this.transform.position += Vector3.right * e_move_Speed * Time.deltaTime;
                    this.transform.localEulerAngles = new Vector3(0, 180, 0);
                    //Debug.Log("Right");
                }

                if (patrol_Time < 0.0f)
                {
                    E_State.e_State = EnemyState.enemy_Idle;
                    animator.SetBool("IsPatrol", false);
                    patrol_Time = Random.Range(1.0f, 3.0f);
                    right = Random.Range(0, 3);
                }
            }
        }
    }

    protected override void M_Chase()
    {
        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        //은범이가 알려준 아이디어
        Vector2 playervec = player.transform.position - this.transform.position;
        //Debug.Log(playervec.x);
        if (playervec.x < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //은범이가 알려준 아이디어

        //추적 하기
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * 0.55f);
    }

    protected override void M_ChaseDist()
    {
        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        //Debug.Log(chaseDist);
        if (chaseDist <= 30.0f)
        {
            E_State.e_State = EnemyState.enemy_Chase;

            if (chaseDist <= e_Att_Range)
            {
                if (player.gameObject.layer == LayerMask.NameToLayer("Player_Die"))
                {
                    animator.SetBool("IsAttack", false);
                }
                else
                {
                    //공격사거리 안에 들어오면 공격
                    M_Attack();
                }
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
            animator.SetBool("IsChase", true);
            M_Chase();
            isChase = true;
        }
        else if (7.0f < chaseDist && isChase == true)
        {
            isChase = false;
            E_State.e_State = EnemyState.enemy_Idle;
            animator.SetBool("IsChase", false);
        }
    }


    protected override void M_Attack()
    {
        if (E_State.e_State == EnemyState.enemy_Resurrection || E_State.e_State == EnemyState.enemy_Death)
            return;

        if (CurHp <= 0.0f)
            return;
        if(!isRevive)
        {
            //Debug.Log(skill);
            if (CurHp <= skill1ActiveHp)
            {
                if (skill < skill1Per)
                {
                    if (pS.p_state == PlayerState.player_die)
                        animator.Play("Pd_Stage3Boss_Idle");
                    E_State.e_State = EnemyState.enemy_Skill;
                    animator.SetBool("IsSkill", true);
                }
                else
                {
                    //Debug.Log("attack");
                    if (pS.p_state == PlayerState.player_die)
                        animator.Play("Pd_Stage3Boss_Idle");

                    E_State.e_State = EnemyState.enemy_Attack;
                    animator.SetBool("IsAttack", true);
                }
            }
            else
            {
                if (pS.p_state == PlayerState.player_die)
                    animator.Play("Pd_Stage3Boss_Idle");

                E_State.e_State = EnemyState.enemy_Attack;
                animator.SetBool("IsAttack", true);
            }
        }
        else
        {
            if (CurHp <= skill2ActiveHp)
            {
                if (skill < skill2Per )
                {
                    M_Skill2Func();
                    //Resources.Load해서 고블린만 생성하기 한번에 한마리씩만
                    //animator.SetBool("IsSKill2", true);
                }
                else
                {
                    if (skill < skill1Per && skill2Per < skill)
                    {
                        if (pS.p_state == PlayerState.player_die)
                            animator.Play("Pd_Stage3Boss_Idle");

                        if (pS.p_state != PlayerState.player_die)
                        {
                            E_State.e_State = EnemyState.enemy_Skill;
                            animator.SetBool("IsSkill", true);
                        }
                    }
                    else
                    {
                        //Debug.Log("attack");
                        if (pS.p_state == PlayerState.player_die)
                            animator.Play("Pd_Stage3Boss_Idle");

                        E_State.e_State = EnemyState.enemy_Attack;
                        animator.SetBool("IsAttack", true);
                    }
                }
            }
            else
            {
                if (skill < skill1Per)
                {
                    if (pS.p_state == PlayerState.player_die)
                        animator.Play("Pd_Stage3Boss_Idle");
                    E_State.e_State = EnemyState.enemy_Skill;
                    animator.SetBool("IsSkill", true);
                }
                else
                {
                    //Debug.Log("attack");
                    if (pS.p_state == PlayerState.player_die)
                        animator.Play("Pd_Stage3Boss_Idle");

                    E_State.e_State = EnemyState.enemy_Attack;
                    animator.SetBool("IsAttack", true);
                }
            }
        }

    }

    public void M_Skill1Func()
    {
        //Debug.Log("skill");
        SoundMgr.Instance.PlayEffSound("Boss_Skill1", 0.7f);
        GameObject skill1 = (GameObject)Instantiate(Resources.Load("Prefab/Stage3_Boss_Skill1")) as GameObject;
        skill1.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
        animator.SetBool("IsSkill", false);
        skill = Random.Range(1, 101);
    }

    public void M_Skill2Func()
    {
        isSkill2 = true;
        if (isSkill2)
        {
            SoundMgr.Instance.PlayEffSound("Boss_Skill2", 0.7f);
            Debug.Log("Skill2");
            GameObject skill2 = (GameObject)Instantiate(Resources.Load("Prefab/Goblin")) as GameObject;
            skill2.transform.position = new Vector3(this.transform.position.x + 1.0f, this.transform.position.y, this.transform.position.z);

            GameObject skill2_1 = (GameObject)Instantiate(Resources.Load("Prefab/Goblin")) as GameObject;
            skill2_1.transform.position = new Vector3(this.transform.position.x - 3.0f, this.transform.position.y, this.transform.position.z);
            isSkill2 = false;
            skill = Random.Range(1, 101);
        }

    }

    protected override void M_AttackFunc()
    {
        //플레이어의 TakeDamage를 가져와서
        //Animation 상에 Add Event에다가 구현 한다.
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack_Point.position, e_att_Range, playerLayer);
        skill = Random.Range(1, 101);
        foreach (Collider2D collider in hitPlayer)
        {
            P_TakeDam.P_TakeDmage(7.0f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attack_Point == null)
            return;

        Gizmos.DrawCube(attack_Point.position, new Vector3(3.0f, 1.0f, 0.0f)/*e_Att_Range*/); ;
    }

    public override void M_Hit(float dmg)
    {
        if (E_State.e_State == EnemyState.enemy_Skill)
            return;

        E_State.e_State = EnemyState.enemy_Hit;
        //hp값 깎기
        CurHp -= dmg;

        //Debug.Log(CurHp);
        animator.SetTrigger("B_TakeDamage");

        if (CurHp <= 0.0f)
        {
            if(!isRevive)
            {
                M_Resurrection();
            }
            else
            {
                M_Death();
            }
        }
    }


    protected override void M_Resurrection()
    {
        SoundMgr.Instance.PlayEffSound("Boss_Revive", 0.7f);
        E_State.e_State = EnemyState.enemy_Resurrection;
        Debug.Log("부활");
        animator.SetBool("IsRevive", true);
        this.gameObject.layer = 13;

    }

    public void Revive()
    {
        SoundMgr.Instance.PlayBGM("Boss_Revive_BGM", 0.4f);
        E_State.e_State = EnemyState.enemy_Idle;
        animator.SetBool("IsRevive", false);
        MaxHp = 1000;
        CurHp = MaxHp;
        isRevive = true;
        this.gameObject.layer = 6;
    }
    protected override void M_Death()
    {
        SoundMgr.Instance.PlayBGM("Boss_Revive_BGM", 0.1f);
        SoundMgr.Instance.PlayEffSound("Boss_Death", 1.0f);
        E_State.e_State = EnemyState.enemy_Death;
        animator.SetTrigger("Boss_DieTrigger");
        this.gameObject.layer = 11;
    }

    protected override void M_Retreat()
    {
        Debug.Log("구현 안함");
    }

}