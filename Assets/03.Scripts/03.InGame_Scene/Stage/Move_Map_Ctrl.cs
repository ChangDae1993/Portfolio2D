using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Move_Map_Ctrl : MonoBehaviour
{
    public Image FadeOut_Img;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut_Img.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(FadeOut_Img.fillAmount == 1.0f)
        {
            SceneManager.LoadScene("Stage_2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeOut_Img.gameObject.SetActive(true);

        }
    }
}
