using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_Mgr : MonoBehaviour
{
    public Image fadeIn;
    private bool isIn;
    public Image fadeOut;
    private bool isOut;

    public Button startBtn;


    private GameObject player;

    private Player_State_Ctrlr PS;

    public Image tutoBG;

    public Text walk_tuto;
    public bool checkTUto1;

    public Text jump_tuto;
    public bool checkTUto2;

    public Text roll_tuto;
    public bool checkTUto3;

    public Text att_tuto;
    public bool checkTUto4;

    public Text shield_tuto;
    public bool checkTUto5;

    public Text hook_tuto;
    public bool checkTUto6;




    private void Start() => StartFunc();

    private void StartFunc()
    {
        player = GameObject.Find("Player");

        fadeIn.gameObject.SetActive(true);
        fadeOut.gameObject.SetActive(false);

        isIn = true;
        isOut = false;

        if (startBtn != null)
            startBtn.onClick.AddListener(StartBtnFunc);

        startBtn.gameObject.SetActive(false);
        tutoBG.gameObject.SetActive(false);

        PS = player.GetComponent<Player_State_Ctrlr>();

        checkTUto1 = false;
        checkTUto2 = false;
        checkTUto3 = false;
        checkTUto4 = false;
        checkTUto5 = false;
        checkTUto6 = false;

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        if (isIn == true)
            fadeIn.fillAmount -= Time.deltaTime * 0.5f;

        if (fadeIn.fillAmount <= 0.0f)
        {
            fadeIn.gameObject.SetActive(false);
            tutoBG.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            walk_tuto.gameObject.SetActive(false);
            checkTUto1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump_tuto.gameObject.SetActive(false);
            checkTUto2 = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            roll_tuto.gameObject.SetActive(false);
            checkTUto3 = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            att_tuto.gameObject.SetActive(false);
            checkTUto4 = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            shield_tuto.gameObject.SetActive(false);
            checkTUto5 = true;
        }

        if (PS.p_Attack_state == PlayerAttackState.player_hook_aim)
        {
            hook_tuto.gameObject.SetActive(false);
            checkTUto6 = true;
        }

        if(checkTUto1 == true && checkTUto2 == true && checkTUto3 == true && checkTUto4 == true && checkTUto5 == true && checkTUto6 == true)
        {
            tutoBG.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(true);
        }

        if (isOut)
        {
            fadeOut.fillAmount += Time.deltaTime * 0.5f;

            if(fadeOut.fillAmount == 1.0f)
                SceneManager.LoadScene("Stage_1");
        }

    }

    public void StartBtnFunc()
    {
        fadeOut.gameObject.SetActive(true);
        isOut = true;
    }
}