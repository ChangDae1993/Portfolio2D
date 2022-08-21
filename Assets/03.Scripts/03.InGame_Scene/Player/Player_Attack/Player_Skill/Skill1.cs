using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill1 : MonoBehaviour
{
    public Image sk1_coolImg;
    bool isHill;
    Animator anim;

    Player_TakeDamage pDam;

    // Start is called before the first frame update
    void Start()
    {
        isHill = false;
        pDam = GetComponent<Player_TakeDamage>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && isHill == false)
        {
            sk1_coolImg.gameObject.SetActive(true);
            sk1_coolImg.fillAmount = 1.0f;
            isHill=true;
        }

        if (isHill)
        {
            sk1_coolImg.fillAmount -= Time.deltaTime * 0.5f;
            pDam.curHp += 0.05f;

            if (sk1_coolImg.fillAmount <= 0.0f)
            {
                isHill = false;
            }

            pDam.Hp_Img.fillAmount = pDam.curHp / pDam.maxHp;
            if (pDam.maxHp <= pDam.curHp)
            {
                pDam.curHp = pDam.maxHp;
                isHill = false;
            }
        }
        else
        {
            sk1_coolImg.gameObject.SetActive(false);
        }

    }
}
