using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Boss_Trigger : MonoBehaviour
{
    public GameObject fire;
    public GameObject Boss;

    private Camera Cam;
    private Camera_Ctrlr camCtrl;

    private Vector2 bossCamCenter;
    private Vector2 bossCamSize;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        fire.gameObject.SetActive(false);
        Boss.gameObject.SetActive(false);
        Cam = Camera.main;
        camCtrl = Cam.GetComponent<Camera_Ctrlr>();
        bossCamCenter = new Vector2(40.7f, 2.5f);
        bossCamSize = new Vector2(14.7f, 7);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
        {
            fire.gameObject.SetActive(true);
            Boss.gameObject.SetActive(true);
            camCtrl.center = bossCamCenter;
            camCtrl.mapSize = bossCamSize;
        }
    }
}