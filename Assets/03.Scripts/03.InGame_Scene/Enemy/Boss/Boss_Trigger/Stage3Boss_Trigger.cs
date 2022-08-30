using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Boss_Trigger : MonoBehaviour
{
    public GameObject fire;
    public GameObject Boss;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        fire.gameObject.SetActive(false);
        Boss.gameObject.SetActive(false);
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
        }
    }
}