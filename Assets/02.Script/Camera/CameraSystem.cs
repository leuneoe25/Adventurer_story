using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public GameObject target;
    public float MoveSpeed;
    private Vector3 targetPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y+2, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }
}
