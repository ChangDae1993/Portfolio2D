using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAtt_Ctrl : MonoBehaviour
{
    //Rigidbody2D rigid;
    //GameObject player;
    ////Player_Input pInput;

    //[SerializeField] private float fly_speed;

    //private float lifeTime;
    //Vector2 p_vec = Vector2.zero;
    //Vector2 p_move = Vector2.zero;

    //private int key;

    // Start is called before the first frame update
    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player");
        ////pInput = player.GetComponent<Player_Input>();
        //fly_speed = 15.0f;
        //lifeTime = 1.0f;
        //key = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //lifeTime -= Time.deltaTime;

        //if(lifeTime <= 0.0f)
        //{
        //    Destroy(this.gameObject);
        //    lifeTime = 1.0f;
        //}

        //if (pInput.horizontal < 0)
        //    key = -1;

        //if (0 < pInput.horizontal)
        //    key = 1;


        //Vector2 p_vector = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);
        //Vector2 p_move = p_vector * fly_speed * Time.deltaTime;
        //rigid.position += p_move;

        //if (player.transform.rotation.y < 0.0f)
        //{
        //    transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        //    p_vec = new Vector2(-1.0f, 0.0f);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        //    p_vec = new Vector2(1.0f, 0.0f);
        //}

        //Vector2 p_move = p_vec * fly_speed * Time.deltaTime;
        //rigid.position += p_move;


    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        collision.GetComponent<Enemy>().M_Hit(20.0f);
    //        Destroy(this.gameObject);
    //    }
    //}
}
