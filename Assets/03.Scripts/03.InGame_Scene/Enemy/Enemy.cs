using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //플레이어 찾아두기
    protected GameObject player;

    protected float MaxHp;
    protected float CurHp;

    protected float Att;
    protected float Att_Range;
    protected float Att_Speed;

    protected float Skill1;
    protected float Skill2;
    protected float Skill3;
    protected float Skill4;

    //값 할당용 변수
    protected abstract void InitData();

    //패트롤
    protected abstract void M_Patrol();

    //추적
    protected abstract void M_Chase();

    //공격
    protected abstract void M_Attack();

    //후퇴
    protected abstract void M_Retreat();

    //피격
    protected abstract void M_Hit();

    //사망
    protected abstract void M_Death();

    //부활
    protected abstract void M_Resurrection();

}