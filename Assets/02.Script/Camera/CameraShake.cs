using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    public GameObject Cm;
    float ShakeTime;
    Vector3 inirialPosition;
    bool isShake = false;
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        inirialPosition = transform.position;
        isShake = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isShake)
        {
            if (ShakeTime > 0)
            {
                Cm.SetActive(false);
                transform.position = Random.insideUnitSphere * ShakeAmount + inirialPosition;
                ShakeTime -= Time.deltaTime;
            }
            else
            {
                ShakeTime = 0f;
                transform.position = inirialPosition;
                isShake = false;
                Cm.SetActive(true);
            }
        }
       
    }
}
