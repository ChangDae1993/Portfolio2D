using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMain
{
    public class CameraShake : MonoBehaviour
    {
        public float ShakeAmout;
        public float ShakeTime;
        Vector3 initPosition;

        private void Start() => StartFunc();
        private void StartFunc()
        {
            ShakeAmout = 0.03f;
            ShakeTime = 1000000000.0f;
            initPosition = this.transform.position;
            //StartCoroutine(ShakeCam());
        }

        private void Update() => UpdateFunc();

        private void UpdateFunc()
        {
            if(ShakeTime > 0.0f)
            {
                this.transform.position = Random.insideUnitSphere * ShakeAmout + initPosition;
                ShakeTime -= Time.deltaTime;
            }
            else
            {
                ShakeTime = 0.0f;
                transform.position = initPosition;
            }
        }

        //IEnumerator ShakeCam()
        //{

        //}

    }
}