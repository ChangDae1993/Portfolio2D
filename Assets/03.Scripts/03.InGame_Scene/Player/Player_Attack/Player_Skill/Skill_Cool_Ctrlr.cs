using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Cool_Ctrlr : MonoBehaviour
{
    public Image s1_coolImg;
    public Image s2_coolImg;

    public Skill2 sk2;

    private Player_Input p_Input;

    public bool skill2Able;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        s2_coolImg.gameObject.SetActive(false);
        p_Input = GetComponentInParent<Player_Input>();
        skill2Able = true;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(p_Input.skill2On)
        {
            s2_coolImg.gameObject.SetActive(true);
            skill2Able = false;
            s2_coolImg.fillAmount -= Time.deltaTime * 0.5f;

            if(s2_coolImg.fillAmount <= 0.0f)
            {
                skill2Able = true;
                p_Input.skill2On = false;
                s2_coolImg.fillAmount = 1.0f;
                s2_coolImg.gameObject.SetActive(false);
            }
        }
    }
}