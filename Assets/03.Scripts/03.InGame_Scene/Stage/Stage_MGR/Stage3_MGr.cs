using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage3_MGr : MonoBehaviour
{
    public Transform startPos;
    private GameObject player;
    private Skill1 p_skill1;

    // Start is called before the first frame update
    void Start()
    {
        SoundMgr.Instance.PlayBGM("Stage3_BGM", 1.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        p_skill1 = player.GetComponent<Skill1>();
        GlobalData.hpPotionNum = 25;
        p_skill1.skill1Num.text = GlobalData.hpPotionNum.ToString();
        player.transform.position = startPos.position;
    }
}