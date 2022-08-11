using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_Mgr : MonoBehaviour
{
    public Image Inven_Panel;
    private bool invenOn;

    private void Start() => StartFunc();

    private void StartFunc()
    {
         Inven_Panel.gameObject.SetActive(false);
        invenOn = false;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(invenOn == false)
            {
                Inven_Panel.gameObject.SetActive(true);
                invenOn = true;
            }
            else
            {
                Inven_Panel.gameObject.SetActive(false);
                invenOn = false;
            }
        }
    }
}