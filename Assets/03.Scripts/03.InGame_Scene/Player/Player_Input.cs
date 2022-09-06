using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Input : MonoBehaviour
{

    private Player_State_Ctrlr Player_State;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    Animator animator;

    private bool configOn;
    public Image Config_Panel;

    public Button Cnxl_Btn;

    //설정 관련UI
    [Header("===Config===")]
    public Button exitBtn;
    public Text MuteOn;
    public Toggle SoundOnOff;
    public Slider SoundVolume;
    [SerializeField] private bool isMute;
    


    // 이런식으로 변수 추가해서 Input class 만들기
    public bool fire { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        var obj = FindObjectsOfType<Player_Input>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    //출처: https://wergia.tistory.com/191 [베르의 프로그래밍 노트:티스토리]
    }

    void Start()
    {
        Player_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
        configOn = false;

        if (exitBtn != null)
            exitBtn.onClick.AddListener(exitFunc);

        if (Cnxl_Btn != null)
            Cnxl_Btn.onClick.AddListener(CancelFunc);

        if (SoundOnOff != null)
            SoundOnOff.onValueChanged.AddListener(MuteFunc);

        if (SoundVolume != null)
            SoundVolume.onValueChanged.AddListener(VolumeFunc);
        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_State.p_state != PlayerState.player_die)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("ShieldOn", true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("ShieldOn", false);
            }

            //설정창
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!configOn)
                {
                    Config_Panel.gameObject.SetActive(true);
                    configOn = true;
                    Time.timeScale = 0.0f;
                }
                else
                {
                    Config_Panel.gameObject.SetActive(false);
                    configOn = false;
                    Time.timeScale = 1.0f;
                }
            }
        }
    }

    public void exitFunc()
    {
        Application.Quit();
    }

    public void CancelFunc()
    {
        Config_Panel.gameObject.SetActive(false);
        configOn = false;
        Time.timeScale = 1.0f;
    }

    private void MuteFunc(bool arg0)
    {
        if (!arg0)
        {
            isMute = true;
            SoundMgr.Instance.SoundOnOff(false);
            MuteOn.text = "Sound Off";
        }
        else
        {
            isMute = false;
            SoundMgr.Instance.SoundOnOff();
            MuteOn.text = "Sound On";
        }
    }

    public void VolumeFunc(float value)
    {
        if(SoundVolume !=  null)
        {
            SoundMgr.Instance.SoundVolume(value);
        }
    }
}
