using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //�÷��̾� ã�Ƶα�
    //��ġ�� state ���� üũ
    protected GameObject player;

    //�÷��̾� TakeDamageȣ��
    protected Player_TakeDamage P_TakeDam;

    protected Enemy_State_Ctrlr E_State;

    //�ִϸ��̼�
    protected Animator animator;

    //ü��
    protected float MaxHp;
    protected float CurHp;

    //���ݷ�
    protected float e_Att;
    //���� ���·� �Ѿ�� �Ÿ�
    protected float e_Att_Range;
    //���� ��Ÿ�
    public float e_att_Range;
    //���� ��� üũ
    public Transform attack_Point;
    public LayerMask playerLayer;
    //private void OnDrawGizmosSelected()�� ���� ���ݿ� ���ؼ� ���� ���� ���� ����

    //���� �ӵ�
    protected float e_Att_Speed;

    //��ų
    protected float Skill1;
    protected float Skill2;
    protected float Skill3;
    protected float Skill4;

    //Patrol Ÿ�̸�
    protected float patrol_Time;
    //Patrol ����
    protected int right;

    //Chase���·� �Ѿ�� üũ�� �ϱ� ���� float (Vector2.Distance)�� ���� ���� �ʿ�
    protected float chaseDist;
    protected bool isChase;

    //�̵��ӵ�
    protected float e_move_Speed;



    //Awake���� ã�Ƶα�
    protected abstract void InitData();

    //��Ʈ��
    protected abstract void M_Patrol();

    //����
    protected abstract void M_Chase();

    //������ �ʿ��� �÷��̾�� �Ÿ� üũ �Լ�
    protected abstract void M_ChaseDist();

    //���� �ִϸ��̼�
    protected abstract void M_Attack();


    //���� ���� ����
    protected abstract void M_AttackFunc(); //���� ������ Layer üũ�ؼ� Player�� ���� ���� ������

    //����
    protected abstract void M_Retreat();

    //�ǰ�
    protected abstract void M_Hit();

    //���
    protected abstract void M_Death();

    //��Ȱ
    protected abstract void M_Resurrection();

}