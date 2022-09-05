using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Boss_Trigger : MonoBehaviour
{
    public GameObject fire;
    public GameObject Boss;

    [SerializeField] private bool bossAreaIn;

    private Camera Cam;
    private Camera_Ctrlr camCtrl;

    private Vector2 bossCamCenter;
    private Vector2 bossCamSize;

    public Enemy boss;
    [SerializeField] private Enemy_State_Ctrlr boss_State;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        fire.gameObject.SetActive(false);
        Boss.gameObject.SetActive(false);
        Cam = Camera.main;
        camCtrl = Cam.GetComponent<Camera_Ctrlr>();
        bossCamCenter = new Vector2(40.7f, 2.5f);
        bossCamSize = new Vector2(14.7f, 7);

        bossAreaIn = false;

        if(boss != null)
        {
            boss_State = boss.GetComponent<Enemy_State_Ctrlr>();
        }

    }

    private void Update() => UpdateFunc();
    private void UpdateFunc()
    {
        if(bossAreaIn)
        {
            Cam.orthographicSize += Time.deltaTime;
            if(Cam.orthographicSize >= 6.5f)
            {
                Cam.orthographicSize = 6.5f;
            }
        }
        else
        {
            Cam.orthographicSize -= Time.deltaTime;
            if (Cam.orthographicSize <= 6.0f)
            {
                Cam.orthographicSize = 6.0f;
            }
        }

        if(boss_State.e_State == EnemyState.enemy_Death)
        {
            bossAreaIn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
        {
            bossAreaIn = true;
            fire.gameObject.SetActive(true);
            Boss.gameObject.SetActive(true);
            camCtrl.center = bossCamCenter;
            camCtrl.mapSize = bossCamSize;
        }
    }
}