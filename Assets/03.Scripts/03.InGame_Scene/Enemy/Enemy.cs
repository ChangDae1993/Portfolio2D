using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //�÷��̾� ã�Ƶα�
    protected GameObject player;

    protected Enemy_State_Ctrlr E_State;

    //�ִϸ��̼�
    protected Animator animator;

    //ü��
    protected float MaxHp;
    protected float CurHp;

    //����
    protected float Att;
    protected float Att_Range;
    protected float Att_Speed;

    //��ų
    protected float Skill1;
    protected float Skill2;
    protected float Skill3;
    protected float Skill4;


    //Awake���� ã�Ƶα�
    protected abstract void InitData();

    //��Ʈ��
    protected abstract void M_Patrol();

    //����
    protected abstract void M_Chase();

    //����
    protected abstract void M_Attack();

    //����
    protected abstract void M_Retreat();

    //�ǰ�
    protected abstract void M_Hit();

    //���
    protected abstract void M_Death();

    //��Ȱ
    protected abstract void M_Resurrection();

}