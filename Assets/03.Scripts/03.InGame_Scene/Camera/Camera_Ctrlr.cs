using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrlr : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;
    private Vector3 z_keyPosition;
    public float z_move_speed;

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
            targetPosition.Set(target.transform.position.x + 2, target.transform.position.y + 3, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            z_keyPosition.Set(this.transform.position.x, this.transform.position.y - 1.0f, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, z_keyPosition, z_move_speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
