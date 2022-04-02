using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("InGame");
        PlayerPrefs.SetInt("ESkill", 0);
        PlayerPrefs.SetInt("QSkill", 0);
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("Level", 1);
        if(GameSystem.instans()!=null)
        {
            GameSystem.instans().SetisFirst(true);
        }
    }
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
