using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowS_Controller : Enemy
{
    public Image Hp_Img;

    //화살용 OBJ
    private GameObject arrowObj;

    //화살 발사 위치
    public Transform firePos;

    private void Awake()
    {
        InitData();
    }

    private void Start() => StartFunc();
    private void StartFunc()
    {
        E_State.e_State = EnemyState.enemy_Idle;
    }

    private void Update() => UpdateFunc();
    private void UpdateFunc()
    {
        chaseDist = Vector2.Distance(this.transform.position, player.transform.position);
        M_ChaseDist();
        M_Patrol();
        //Debug.Log(patrol_Time);

        if (E_State.e_State == EnemyState.enemy_Death)
        {
            disTime -= Time.deltaTime;

            if (disTime <= 0.0f)
                Destroy(this.gameObject);
        }

        if (isStun)
        {
            E_State.e_StunState = EnemyStunState.enemy_Stun;
            stunTimer -= Time.deltaTime;
        }
        else
        {
            if (CurHp <= 0.0f)
            {
                M_Death();
            }
            stunTimer = 3.0f;
            animator.SetBool("IsStun", false);
        }

        if (stunTimer <= 0.0f)
        {
            E_State.e_StunState = EnemyStunState.enemy_noStun;
            isStun = false;
        }
    }

    protected override void InitData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        P_TakeDam = player.GetComponent<Player_TakeDamage>();
        E_State = GetComponent<Enemy_State_Ctrlr>();
        animator = GetComponent<Animator>();
        patrol_Time = Random.Range(1.0f, 2.0f);
        pS = player.GetComponent<Player_State_Ctrlr>();

        MaxHp = 80;
        CurHp = MaxHp;
        e_move_Speed = 1.0f;
        e_Att = 4.0f;           //(임시)
        e_Att_Range = 10.0f;
        e_att_Range = 0.1f;
        right = Random.Range(0, 2);

        chaseDist = 0.0f;
        isChase = false;

        disTime = 2.0f;

        isRetreat = false;
        retreatTimer = -1.0f;

        isStun = false;
        stunTimer = 5.0f;
    }

    protected override void M_Patrol()
    {
        if (E_State.e_StunState == EnemyStunState.enemy_Stun)
            return;

        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        if (E_State.e_State == EnemyState.enemy_Idle)
        {
            patrol_Time -= Time.deltaTime;
            if (patrol_Time <= 0.0f)
            {
                E_State.e_State = EnemyState.enemy_Patrol;
                patrol_Time = Random.Range(1.0f, 2.0f);
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
                    patrol_Time = Random.Range(1.0f, 2.0f);
                    right = Random.Range(0, 3);
                }
            }
        }
    }
    protected override void M_ChaseDist()
    {
        if (E_State.e_StunState == EnemyStunState.enemy_Stun)
            return;

        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        //Debug.Log(chaseDist);
        if (chaseDist <= 15.0f)
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
            M_Chase();
            isChase = true;
        }
        else if (7.0f < chaseDist && isChase == true)
        {
            isChase = false;
            E_State.e_State = EnemyState.enemy_Idle;
            animator.SetBool("IsAlert", false);
        }
    }
    protected override void M_Chase()
    {
        if (E_State.e_StunState == EnemyStunState.enemy_Stun)
            return;

        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        animator.SetBool("IsAlert", true);

        //은범이가 알려준 아이디어
        Vector2 playervec = player.transform.position - this.transform.position;
        //Debug.Log(playervec.x);
        if (playervec.x < 0.0f)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //은범이가 알려준 아이디어

        //1번 후퇴하기 기능
        if (retreatTimer >= 0.0f)
        {
            retreatTimer -= Time.deltaTime;
            if(player.transform.rotation.y < 0.0f)
            {
                Vector2 targetPos = new Vector2(player.transform.position.x - 8.0f, player.transform.position.y);
                //추적 하기
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * 0.65f);
            }
            else
            {
                Vector2 targetPos = new Vector2(player.transform.position.x + 8.0f, player.transform.position.y);
                //추적 하기
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * 0.65f);
            }

            if (playervec.x < 0.0f)
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
            else
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
            if (retreatTimer <= 0.0f)
            {
                isRetreat = true;
            }
        }
        //1번 후퇴하기 기능
    }

    protected override void M_Attack()
    {
        if (pS.p_state == PlayerState.player_die)
            animator.Play("Pd_Bow_Idle");
        E_State.e_State = EnemyState.enemy_Attack;
        animator.SetBool("IsAttack", true);
        //Debug.Log("Shoot");
    }

//화살 생성하기
    
    private void arrowRelease()
    {
        arrowObj = Resources.Load("Prefab/E_Arrow") as GameObject;
        //타이머 맞춰서 화살 생성하기
        GameObject Arrow = Instantiate(arrowObj, transform.position, transform.rotation) as GameObject;
        Arrow.transform.position = firePos.position;
        //타이머 맞춰서 화살 생성하기
    }

    //화살 생성하기

    public override void M_Hit(float dmg)
    {
        E_State.e_State = EnemyState.enemy_Hit;
        //hp값 깎기
        CurHp -= dmg;
        Hp_Img.fillAmount = CurHp / MaxHp;
        //Debug.Log(CurHp);
        animator.SetTrigger("B_TakeDamage");

        if (CurHp <= MaxHp * 0.5f && isRetreat == false)
        {
            retreatTimer = 1.0f;
        }

        if (CurHp <= 0.0f)
        {
            M_Death();
        }
    }

    protected override void M_AttackFunc()
    {

    }

    public override void M_Stun()
    {
        isStun = true;
        if (isStun)
        {
            Debug.Log("Stun");
            animator.SetBool("IsStun", true);
        }

    }

    protected override void M_Death()
    {
        E_State.e_State = EnemyState.enemy_Death;
        animator.SetTrigger("DieTrigger");
        this.gameObject.layer = 11;
    }

    protected override void M_Resurrection()
    {

    }

    protected override void M_Retreat()
    {

    }
}