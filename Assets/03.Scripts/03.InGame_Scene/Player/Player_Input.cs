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
    public Button exitBtn;


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
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Input.GetMouseButton(1))
        {
            animator.SetBool("ShieldOn", true);
        }

        if(Input.GetMouseButtonUp(1))
        {
            animator.SetBool("ShieldOn", false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
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

    public void exitFunc()
    {
        Application.Quit();
    }
}
