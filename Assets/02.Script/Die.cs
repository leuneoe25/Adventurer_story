using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        button.onClick.AddListener(StartScene);
    }

    void StartScene()
    {
        SceneManager.LoadScene("InGame");
        GameSystem.instans().StartSet();
    }
}
