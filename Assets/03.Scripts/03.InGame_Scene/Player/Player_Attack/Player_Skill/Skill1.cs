using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill1 : MonoBehaviour
{
    public Image sk1_coolImg;
    public GameObject skill_Obj;
    bool isHill;
    public Text skill1Num;
    Animator anim;

    Player_TakeDamage pDam;

    // Start is called before the first frame update
    void Start()
    {
        isHill = false;
        pDam = GetComponent<Player_TakeDamage>();
        anim = GetComponent<Animator>();
        skill_Obj.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        skill1Num.text = GlobalData.hpPotionNum.ToString();
        if (Input.GetKeyDown(KeyCode.Q) && isHill == false)
        {
            if (0 < GlobalData.hpPotionNum && pDam.curHp < 100)
            {
                //���� ���� ���ؼ� �ش� ������ 0 �̸� return�ϵ��� �����
                //GlobalData�� ���� ���� ���� ���� �� ����ġ ��� ����??
                SoundMgr.Instance.PlayEffSound("Skill1", 1.0f);
                sk1_coolImg.gameObject.SetActive(true);
                sk1_coolImg.fillAmount = 1.0f;
                isHill = true;
                GlobalData.hpPotionNum--;
            }
            else
            {
                Debug.Log("���� ���� or ü�� ����");
            }

        }

        if (isHill)
        {
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
