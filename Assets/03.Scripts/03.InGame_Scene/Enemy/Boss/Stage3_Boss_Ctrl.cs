using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_Boss_Ctrl : Enemy
{
    public Image Hp_Img;

    Vector2 targetPos = Vector2.zero;

    private float skillPer;
    private int skill;
    [SerializeField] private bool isRevive;

    private void Awake()
    {
        InitData();
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        skillPer = 40f;
        skill = Random.Range(1, 101);
        isRevive = false;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        chaseDist = Vector2.Distance(this.transform.position, player.transform.position);
        targetPos = new Vector2(player.transform.position.x + 3.0f, player.transform.position.y);
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
        e_Att = 15.0f;           //(�ӽ�)
        e_Att_Range = 20.0f;
        //e_att_Range = 0.5f;
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
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * 0.55f);
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
        //Debug.Log(skill);
        if (CurHp <= 600)
        {
            if (skill < skillPer)
            {
                if (player.GetComponent<Player_State_Ctrlr>().p_state == PlayerState.player_die)
                    animator.Play("Pd_Stage3Boss_Idle");

                Debug.Log("skill");
                E_State.e_State = EnemyState.enemy_Skill;
                animator.SetBool("IsSkill", true);
            }
            else
            {
                Debug.Log("attack");
                if (player.GetComponent<Player_State_Ctrlr>().p_state == PlayerState.player_die)
                    animator.Play("Pd_Stage3Boss_Idle");

                E_State.e_State = EnemyState.enemy_Attack;
                animator.SetBool("IsAttack", true);
            }
        }
        else
        {
            if (player.GetComponent<Player_State_Ctrlr>().p_state == PlayerState.player_die)
                animator.Play("Pd_Stage3Boss_Idle");

            E_State.e_State = EnemyState.enemy_Attack;
            animator.SetBool("IsAttack", true);
        }
    }

    private void M_SkillFunc()
    {

        skill = Random.Range(1, 101);
        animator.SetBool("IsSkill", false);
    }

    protected override void M_AttackFunc()
    {
        //�÷��̾��� TakeDamage�� �����ͼ�
        //Animation �� Add Event���ٰ� ���� �Ѵ�.
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack_Point.position, e_att_Range, playerLayer);
        skill = Random.Range(1, 101);
        foreach (Collider2D collider in hitPlayer)
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
        if (E_State.e_State == EnemyState.enemy_Skill)
            return;

        E_State.e_State = EnemyState.enemy_Hit;
        //hp�� ���
        CurHp -= dmg;
        Hp_Img.fillAmount = CurHp / MaxHp;
        //Debug.Log(CurHp);
        animator.SetTrigger("B_TakeDamage");

        if (CurHp <= 0.0f)
        {
            M_Death();
        }
    }

    protected override void M_Death()
    {
        E_State.e_State = EnemyState.enemy_Death;
        animator.SetTrigger("Boss_DieTrigger");
        this.gameObject.layer = 11;
    }
    protected override void M_Resurrection()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Retreat()
    {
        Debug.Log("���� ����");
    }

}