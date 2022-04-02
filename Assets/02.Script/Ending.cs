using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private Text text;
    void Start()
    {
        text.text = "���� Ƚ�� : " +GameSystem.instans().GetDie().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScenes");
        }
    }
}
