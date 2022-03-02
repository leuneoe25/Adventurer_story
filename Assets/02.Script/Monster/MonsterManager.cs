using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject Potarl;
    [SerializeField] private int Numder;
    void Start()
    {
        Potarl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount == Numder)
        {
            Potarl.SetActive(true);
        }
    }
}
