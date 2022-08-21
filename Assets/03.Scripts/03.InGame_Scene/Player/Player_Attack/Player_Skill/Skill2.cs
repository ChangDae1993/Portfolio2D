using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    public Image chargeImgBG;
    public Image chargeImg;

    public GameObject shotPos;

    // Start is called before the first frame update
    void Start()
    {
        chargeImgBG.gameObject.SetActive(false);
        shotPos.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            chargeImgBG.gameObject.SetActive(true);
            shotPos.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            chargeImgBG.gameObject.SetActive(false);
            shotPos.gameObject.SetActive(false);
        }
        else if(Input.GetMouseButton(0))
        {
            chargeImgBG.gameObject.SetActive(false);
            shotPos.gameObject.SetActive(false);
        }
    }
}
