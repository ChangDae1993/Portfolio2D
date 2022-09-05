using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage3_MGr : MonoBehaviour
{
    public Transform startPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SoundMgr.Instance.PlayBGM("Stage3_BGM", 1.0f);
        player = GameObject.FindGameObjectWithTag("Player");

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