using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;

    private float move_speed = 5.0f;
    private float crawl_speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        move_speed = 5.0f;
        crawl_speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(p_input.horizontal != 0)
        {
            P_Move_Walk();
        }
        else
        {
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            P_Move_Crawl();
        }
        else
        {
            move_speed = 5.0f;
        }
    }

    private void P_Move_Walk()
    {
        Vector2 p_vector = new Vector2(p_input.horizontal, .0f);
        Vector2 p_move = p_vector * move_speed * Time.deltaTime;
        rigid.position += p_move;
    }

    private void P_Move_Crawl()
    {
        move_speed = crawl_speed;
    }
}
