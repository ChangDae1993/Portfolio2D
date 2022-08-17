using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_Ctrl : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float fly_speed;

    private float destroyTimer;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        rigid = GetComponent<Rigidbody2D>();
        fly_speed = -1.0f;
        destroyTimer = 1.0f;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        destroyTimer -= Time.deltaTime;

        if(destroyTimer <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        rigid.AddForce(transform.right * fly_speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
        {
            collision.GetComponent<Player_TakeDamage>().P_TakeDmage(4.0f);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("PLATFORM"))
        {
            Destroy(this.gameObject);
        }
    }
}