using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowS_Controller : Enemy
{
    public Image Hp_Img;

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

    }
    protected override void InitData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        P_TakeDam = player.GetComponent<Player_TakeDamage>();
        E_State = GetComponent<Enemy_State_Ctrlr>();
        animator = GetComponent<Animator>();
        patrol_Time = Random.Range(1.0f, 2.0f);

        MaxHp = 80;
        CurHp = MaxHp;
        e_move_Speed = 1.0f;
        e_Att = 4.0f;           //(임시)
        e_Att_Range = 1.3f;
        e_att_Range = 0.1f;
        right = Random.Range(0, 2);

        chaseDist = 0.0f;
        isChase = false;

        disTime = 2.0f;
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
        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        //Debug.Log(chaseDist);
        if (chaseDist <= 7.0f)
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

    public override void M_Hit(float dmg)
    {

    }


    protected override void M_Attack()
    {

    }

    protected override void M_AttackFunc()
    {

    }

    protected override void M_Chase()
    {

    }


    protected override void M_Death()
    {

    }

    protected override void M_Resurrection()
    {

    }

    protected override void M_Retreat()
    {

    }
}