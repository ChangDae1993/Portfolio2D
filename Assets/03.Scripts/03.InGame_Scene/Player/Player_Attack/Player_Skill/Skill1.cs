using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill1 : MonoBehaviour
{
    private Image sk1_coolImg;
    public GameObject skill_Obj;
    bool isHill;
    public Text skill1Num;
    Animator anim;

    Player_TakeDamage pDam;

    // Start is called before the first frame update
    void Start()
    {
        sk1_coolImg = GameObject.Find("Skill_Cool_CTRLR").GetComponent<Skill_Cool_Ctrlr>().s1_coolImg;
        isHill = false;
        pDam = GetComponent<Player_TakeDamage>();
        anim = GetComponent<Animator>();
        skill_Obj.gameObject.SetActive(false);
        skill1Num.text = GlobalData.hpPotionNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && isHill == false)
        {

            if (0 < GlobalData.hpPotionNum && pDam.curHp < 100)
            {
                //물약 개수 정해서 해당 개수가 0 이면 return하도록 만들기
                //GlobalData를 만들어서 물약 개수 관리 및 경험치 계수 관리??
                SoundMgr.Instance.PlayEffSound("Skill1", 1.0f);
                sk1_coolImg.gameObject.SetActive(true);
                sk1_coolImg.fillAmount = 1.0f;
                isHill = true;
                GlobalData.hpPotionNum--;
            }

        }

        if (isHill)
        {
            skill1Num.text = GlobalData.hpPotionNum.ToString();
            skill_Obj.gameObject.SetActive(true);
            sk1_coolImg.fillAmount -= Time.deltaTime * 0.5f;
            pDam.curHp += 0.1f;

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
            skill_Obj.gameObject.SetActive(false);
            sk1_coolImg.gameObject.SetActive(false);
        }

    }
}
