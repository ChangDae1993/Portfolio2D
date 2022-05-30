using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{

    private Player_State_Ctrlr Player_State;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    Animator animator;


    // 이런식으로 변수 추가해서 Input class 만들기
    public bool fire { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Player_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Sword_Attack_start");
        }

        if(Input.GetMouseButton(1))
        {
            animator.SetBool("ShieldOn", true);
        }

        if(Input.GetMouseButtonUp(1))
        {
            animator.SetBool("ShieldOn", false);
        }
    }
}
