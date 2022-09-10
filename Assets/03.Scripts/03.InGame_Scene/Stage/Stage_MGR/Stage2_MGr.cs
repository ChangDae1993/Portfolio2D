using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2_MGr : MonoBehaviour
{
    public Transform startPos;
    private GameObject player;
    private Skill1 p_skill1;

    // Start is called before the first frame update
    void Start()
    {
        SoundMgr.Instance.PlayBGM("Stage2_BGM", 1.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        p_skill1 = player.GetComponent<Skill1>();
        GlobalData.hpPotionNum = 12;
        p_skill1.skill1Num.text = GlobalData.hpPotionNum.ToString();
        player.transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}