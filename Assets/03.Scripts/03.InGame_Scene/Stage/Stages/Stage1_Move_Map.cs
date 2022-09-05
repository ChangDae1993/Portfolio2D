using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1_Move_Map : Move_Map_Ctrl
{
    public Image FadeOut_Img;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        FadeOut_Img.gameObject.SetActive(false);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (FadeOut_Img.fillAmount == 1.0f)
        {
            GlobalData.hpPotionNum = 12;
            GlobalData.stage_Progress++;
            SceneManager.LoadScene("Stage_2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundMgr.Instance.PlayGUISound("Scene_Move", 1.0f);
            MoveScene();
        }
    }

    protected override void MoveScene()
    {

        FadeOut_Img.gameObject.SetActive(true);
    }
}