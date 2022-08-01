using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Mgr : MonoBehaviour
{
    public Button Start_Btn;
    public Image fadeOut;

    private bool isStart;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        isStart = false;

        if (Start_Btn != null)
            Start_Btn.onClick.AddListener(StartBtnFunc);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (isStart == true)
            fadeOut.fillAmount += Time.deltaTime * 0.5f;

        if (fadeOut.fillAmount >= 1.0f)
            SceneManager.LoadScene("Lobby");
    }

    void StartBtnFunc()
    {
        isStart = true;
    }
}