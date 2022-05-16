using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;

    private float dash_speed = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        dash_speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            P_Move_Dash();
        }
    }

    private void P_Move_Dash()
    {
        Vector2 p_vector = new Vector2(p_input.horizontal, 0);
        Vector2 p_dash = p_vector * dash_speed * Time.deltaTime;
        rigid.position += p_dash;
    }
}
