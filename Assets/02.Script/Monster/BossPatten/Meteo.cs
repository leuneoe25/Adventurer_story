using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Start()
    {
        Invoke("DestroyObject", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed);
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
