using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_Boss_Ctrl : Enemy
{
    //public Image Hp_Img;

    private void Awake()
    {
        InitData();
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {

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
        patrol_Time = Random.Range(1.0f, 3.0f);

        MaxHp = 1000;
        CurHp = MaxHp;
        e_move_Speed = 1.0f;
        e_Att = 8.0f;           //(�ӽ�)
        e_Att_Range = 2.0f;
        e_att_Range = 0.5f;
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

                //������ �������� �����̱�?
                //�����̴� ����� this.transform.position += / -= Vector3.right * e_move_Speed * Time.deltaTime�� ���
                //�����̴� ȸ���� this.transform.localEulerAngles ���
                if (right == 1)
                {
                    //1�̴ϱ� ����
                    this.transform.position -= Vector3.right * e_move_Speed * Time.deltaTime;
                    this.transform.localEulerAngles = new Vector3(0, 0, 0);
                    //Debug.Log("Left");
                }
                else if (right == 0)
                {
                    //0�̴ϱ� ������
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

        //�����̰� �˷��� ���̵��
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
        //�����̰� �˷��� ���̵��

        //���� �ϱ�
        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 0.55f);
    }

    protected override void M_ChaseDist()
    {
        if (E_State.e_State == EnemyState.enemy_Death)
            return;

        //Debug.Log(chaseDist);
        if (chaseDist <= 8.0f)
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
                    //���ݻ�Ÿ� �ȿ� ������ ����
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
        throw new System.NotImplementedException();
    }

    protected override void M_AttackFunc()
    {
        throw new System.NotImplementedException();
    }

    public override void M_Hit(float dmg)
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Resurrection()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Death()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Retreat()
    {
        Debug.Log("���� ����");
    }

}