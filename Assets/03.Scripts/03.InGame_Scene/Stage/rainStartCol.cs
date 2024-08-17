using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainStartCol : MonoBehaviour
{
    public BaseRainScript rain2D;

    public float rainVolume;    // 0~1f 사이 값
    public float changeSpeed;   // 0~1f 사이 값


    Coroutine rainCo;
    // Start is called before the first frame update
    void Start()
    {
        InitSetting();
    }

    void InitSetting()
    {
        rain2D.RainIntensity = 0f;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(rainCo != null)
            {
                StopCoroutine(rainCo);
            }

            rainCo = StartCoroutine(rainVolumeChange(rainVolume, changeSpeed));
            //rain2D.RainIntensity = rainVolume;
        }
    }

    public IEnumerator rainVolumeChange(float volume, float chngSpeed)
    {
        if(rain2D.RainIntensity > volume)
        {
            while(rain2D.RainIntensity > volume)
            {
                rain2D.RainIntensity -= chngSpeed;
                yield return null;
            }
            rain2D.RainIntensity = volume;
            Debug.Log("volime down");
            yield break;
        }
        else if(rain2D.RainIntensity < volume)
        {
            Debug.Log("volume up");
            while (rain2D.RainIntensity < volume)
            {
                rain2D.RainIntensity += chngSpeed;
                yield return null;
            }
            rain2D.RainIntensity = volume;
            yield break;
        }
        else
        {
            rain2D.RainIntensity = volume;
            Debug.Log("volume change X");
            yield break;
        }

    }
}
