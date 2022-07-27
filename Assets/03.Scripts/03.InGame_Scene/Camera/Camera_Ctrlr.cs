using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrlr : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {      
    }
    
    // Update is called once per frame
    //void Update()
    //{
    //}

    private void LateUpdate()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x + 3, target.transform.position.y + 3, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
