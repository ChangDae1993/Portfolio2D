using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_Mgr : MonoBehaviour
{
    public Image fadeIn;

    private bool isIn;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        fadeIn.gameObject.SetActive(true);
        isIn = true;

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        if (isIn == true)
            fadeIn.fillAmount -= Time.deltaTime * 0.5f;

        if (fadeIn.fillAmount <= 0.0f)
            fadeIn.gameObject.SetActive(false);
    }
}