using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin_Controller : Enemy
{
    public Image Hp_Img;

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
            //Debug.Log("Stun");
            animator.SetBool("IsStun", true);
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
        patrol_Time = Random.Range(1.0f, 3.0f);
        pS = player.GetComponent<Player_State_Ctrlr>();

        MaxHp = 100;
        CurHp = MaxHp;
        e_move_Speed = 2.0f;
        e_Att = 4.0f;           //(�ӽ�)
        e_Att_Range = 1.3f;
        e_att_Range = 0.1f;
        right = Random.Range(0, 2);

        chaseDist = 0.0f;
        isChase = false;

        disTime = 2.0f;

        isStun = false;
        stunTimer = 3.0f;
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
        if (E_State.e_StunState == EnemyStunState.enemy_Stun)
            return;

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

    //�÷��̾�� �Ÿ� üũ�ϴ� �Լ�
    protected override void M_ChaseDist()
    {
        if (E_State.e_StunState == EnemyStunState.enemy_Stun)
            return;

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
        else if(7.0f < chaseDist && isChase == true)
        {
            isChase = false;
            E_State.e_State = EnemyState.enemy_Idle;
            animator.SetBool("IsChase", false);
        }
    }


    protected override void M_Attack()
    {
        if (pS.p_state == PlayerState.player_die)
            animator.Play("Pd_Goblin_Idle");

        E_State.e_State = EnemyState.enemy_Attack;
        animator.SetBool("IsAttack", true);
        //Debug.Log("Attack");
        //Debug.Log(player.GetComponent<Player_State_Ctrlr>().p_state);
    }

    protected override void M_AttackFunc()
    {
        //�÷��̾��� TakeDamage�� �����ͼ�
        //Animation �� Add Event���ٰ� ���� �Ѵ�.
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack_Point.position, e_att_Range, playerLayer);

        foreach(Collider2D collider in hitPlayer)
        {
            P_TakeDam.P_TakeDmage(3.0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attack_Point == null)
            return;

        Gizmos.DrawWireSphere(attack_Point.position, e_att_Range);
    }

    public override void M_Hit(float dmg)
    {
        E_State.e_State = EnemyState.enemy_Hit;
        //hp�� ���
        CurHp -= dmg;
        Hp_Img.fillAmount = CurHp / MaxHp;
        //Debug.Log(CurHp);
        animator.SetTrigger("E_TakeDamage");

        if (CurHp <= 0.0f)
        {
            M_Death();
        }

    }
    protected override void M_Retreat()
    {
        //����� ���� ����
        throw new System.NotImplementedException();
    }

    protected override void M_Death()
    {
        E_State.e_State = EnemyState.enemy_Death;
        animator.SetTrigger("DieTrigger");
        this.gameObject.layer = 11;
    }

    protected override void M_Resurrection()
    {
        throw new System.NotImplementedException();
    }

    public override void M_Stun()
    {
        isStun = true;

    }
}
