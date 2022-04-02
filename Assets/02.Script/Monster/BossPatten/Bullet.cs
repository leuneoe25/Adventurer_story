using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Start()
    {
        Invoke("des", 3);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed);
    }
    void des()
    {
        Destroy(gameObject);
    }
}
