using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Boss_Skill_Ctrl : MonoBehaviour
{
    //�÷��̾� ã�Ƶα�
    //��ġ�� state ���� üũ
    protected GameObject player;
    
    //�÷��̾� TakeDamageȣ��
    protected Player_TakeDamage P_TakeDam;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        P_TakeDam = player.GetComponent<Player_TakeDamage>();
    }

    //private void Update() => UpdateFunc();

    //private void UpdateFunc()
    //{
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
        {
            P_TakeDam.P_TakeDmage(10.0f);
        }
    }

    private void SkillDestroy()
    {
        Destroy(this.gameObject);
    }
}