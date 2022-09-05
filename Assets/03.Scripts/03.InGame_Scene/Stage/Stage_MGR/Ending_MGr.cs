using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending_MGr : MonoBehaviour
{
    private GameObject player;
    private void Start() => StartFunc();

    private void StartFunc()
    {
        SoundMgr.Instance.PlayBGM("Ending_BGM", 0.5f);
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player.gameObject);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Title");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("out");
            Application.Quit();
        }
    }
}