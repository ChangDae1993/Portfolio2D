using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Controller : Enemy
{
    protected override void InitData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        E_State = GetComponent<Enemy_State_Ctrlr>();
        animator = GetComponent<Animator>();
        patrol_Time = Random.Range(1.0f, 3.0f);

        MaxHp = 100;
        CurHp = MaxHp;
        e_move_Speed = 1.0f;
        e_Att = 4.0f;           //(임시)
        right = Random.Range(0, 2);

        chaseDist = 0.0f;
    }

    private void Awake()
    {
        InitData();
    }

    // Start is called before the first frame update
    void Start()
    {
        E_State.e_State = EnemyState.enemy_Idle;
    }

    // Update is called once per frame
    void Update()
    {
        M_ChaseDist();
        M_Patrol();
        //Debug.Log(patrol_Time);
    }

    protected override void M_Patrol()
    {
        //m_Patrol_Time = Random.Range(0.0f, 5.0f);
        //Debug.Log(m_Patrol_Time);
        //if(E_State.e_State == EnemyState.enemy_StopChase)
        //{
        //    //patrol_Time = Random.Range(1.0f, 3.0f);
        //    patrol_Time -= Time.deltaTime;
        //    if (patrol_Time <= 0.0f)
        //    {
        //        E_State.e_State = EnemyState.enemy_Idle;
        //    }
        //}

        if (E_State.e_State == EnemyState.enemy_Idle)
        {
            patrol_Time -= Time.deltaTime;
            if (patrol_Time <= 0.0f)
            {
                E_State.e_State = EnemyState.enemy_Patrol;
                patrol_Time = Random.Range(1.0f, 3.0f);
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

    //플레이어와 거리 체크하는 함수
    protected override void M_ChaseDist()
    {
        chaseDist = Vector2.Distance(this.transform.position , player.transform.position);
        if (chaseDist <= 7.0f)
        {
            //E_State.e_State = EnemyState.enemy_Chase;
            //상태값을 바꾸면 고장난다ㅠㅠ

            animator.SetBool("IsChase", true);
            M_Chase();
        }
        else
        {
            //Chase는 연출만 하고
            //기능은 따로 구현해야겠다ㅠㅠ

            animator.SetBool("IsChase", false);
        }
    }

    protected override void M_Chase()
    {
        Debug.Log("Chase!!");
        //Chase 함수로 넘어오는거 확인
        //이제 Chase 기능 구현 필요

        //Vector3.magnitude 사용?


    }

    protected override void M_Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Hit()
    {
        throw new System.NotImplementedException();
    }
    protected override void M_Retreat()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Death()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Resurrection()
    {
        throw new System.NotImplementedException();
    }

}
